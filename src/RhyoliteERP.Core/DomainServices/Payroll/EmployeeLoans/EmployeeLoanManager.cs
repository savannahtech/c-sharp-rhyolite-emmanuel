using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeLoans
{
   public class EmployeeLoanManager: Abp.Domain.Services.DomainService, IEmployeeLoanManager
    {
        private readonly IRepository<EmployeeLoan, Guid> _repositoryEmployeeLoan;
        private readonly IRepository<EmployeeBioData, Guid> _repositoryEmployeeBioData;

        private readonly IRepository<EmployeeLoanRepaymentSchedule, Guid> _repositoryEmployeeLoanRepaymentSchedule;
        private decimal totalBalance = 0;
        private DateTime currentDate = DateTime.Today;
        public EmployeeLoanManager(IRepository<EmployeeLoan, Guid> repositoryEmployeeLoan, IRepository<EmployeeLoanRepaymentSchedule, Guid> repositoryEmployeeLoanRepaymentSchedule, IRepository<EmployeeBioData, Guid> repositoryEmployeeBioData)
        {
            _repositoryEmployeeLoan = repositoryEmployeeLoan;
            _repositoryEmployeeLoanRepaymentSchedule = repositoryEmployeeLoanRepaymentSchedule;
            _repositoryEmployeeBioData = repositoryEmployeeBioData;
        }

        public async Task Create(EmployeeLoan entity)
        {
            var employeeBioData = await _repositoryEmployeeBioData.FirstOrDefaultAsync(entity.EmployeeId);

            var employeeLoanId = Guid.NewGuid();

            entity.Id = employeeLoanId;
            entity.EmployeeCategory = employeeBioData.CategoryName;
            entity.EmployeeDepartment = employeeBioData.DepartmentName;
            entity.Status = "Pending";
            await _repositoryEmployeeLoan.InsertAsync(entity);

            await CreateRepaySchedule(entity, employeeLoanId);
        }

        public async Task Update(EmployeeLoan entity)
        {
            await _repositoryEmployeeLoan.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryEmployeeLoan.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryEmployeeLoan.GetAllListAsync();
        }

        public async Task<object> ListOutStandingLoans()
        {
            return await _repositoryEmployeeLoan.GetAllListAsync(x=> x.CurrentBalance > 0 && x.IsApproved);
        }

        public async Task<object> ListPendingLoans()
        {
            return await _repositoryEmployeeLoan.GetAllListAsync(x => !x.IsApproved);
        }
        public async Task<object> ListPastLoans()
        {
            return await _repositoryEmployeeLoan.GetAllListAsync(x =>  x.IsApproved);
        }
        public async Task<object> ListAll(Guid employeeId)
        {
            return await _repositoryEmployeeLoan.GetAllListAsync(x=> x.EmployeeId == employeeId);
        }

        public async Task Delete(Guid id)
        {
            await _repositoryEmployeeLoan.DeleteAsync(id);

        }

        public async Task Approve(List<Guid> ids, string approvalType)
        {
            foreach (var id in ids)
            {

                var data = await _repositoryEmployeeLoan.FirstOrDefaultAsync(id);
                if (data != null)
                {

                    switch (approvalType)
                    {
                        case "approve":
                            data.IsApproved = true;
                            data.Status = "Approved";
                            break;

                        case "reject":
                            data.IsApproved = false;
                            data.Status = "Rejected";
                            break;

                        default:
                            break;
                    }

                    await _repositoryEmployeeLoan.UpdateAsync(data);

                }


            }

        }

        private async Task CreateRepaySchedule(EmployeeLoan input, Guid employeeLoanId)
        {
            totalBalance = input.CurrentBalance;
            currentDate = input.DeductionStarts;

            //calculate loan repayment schedule... 
            //test for simple interest only
            for (int i = 0; i < input.Duration; i++)
            {
                var loanRepaymentSchedule = new EmployeeLoanRepaymentSchedule
                {
                    EmployeeId = input.EmployeeId,
                    EmployeeIdentifier = input.EmployeeIdentifier,
                    EmployeeName  = input.EmployeeName,
                    EmployeeLoanName = input.LoanTypeName,
                    EmployeeLoanId = employeeLoanId,
                    ScheduleDate = GetCurDate(currentDate) //obtain last day of the month based on deduction start date ...
                };
                if (totalBalance <= input.MonthlyDeduction)
                {
                    input.MonthlyDeduction = totalBalance;
                    loanRepaymentSchedule.InterestPlusPrincipalBalance = GetCurBalance(totalBalance, input.MonthlyDeduction);
                    loanRepaymentSchedule.MonthlyPayment = totalBalance;

                }
                else
                {
                    loanRepaymentSchedule.InterestPlusPrincipalBalance = GetCurBalance(input.MonthlyDeduction, totalBalance);

                }

                loanRepaymentSchedule.MonthlyPayment = input.MonthlyDeduction;
                loanRepaymentSchedule.PrincipalPayment = input.Amount / input.Duration;
                loanRepaymentSchedule.InterestPayment = input.InterestCharges / input.Duration;
                loanRepaymentSchedule.PrincipalBalance = input.Amount - (input.Amount / input.Duration);

                //loanRepaymentSchedule.InterestPlusPrincipalBalance =GetCurBalance(param.MonthlyDeduction, totalBalance);
                loanRepaymentSchedule.Period = i + 1;

                await _repositoryEmployeeLoanRepaymentSchedule.InsertAsync(loanRepaymentSchedule);

            }



        }

        //helpers...
        private decimal GetCurBalance(decimal monthlypayment, decimal currentBalance)
        {

            if (currentBalance <= monthlypayment)
            {
                currentBalance = monthlypayment;
                var balance = currentBalance - monthlypayment;

                return balance;
            }
            else
            {
                var balance = currentBalance - monthlypayment;

                totalBalance = balance;
                return balance;
            }

        }

        private DateTime GetCurDate(DateTime dateTime)
        {
            int numberofDays = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            DateTime temp = new DateTime(dateTime.Year, dateTime.Month, numberofDays);
            currentDate = temp.AddMonths(1);
            return temp;
        }
    }
}
