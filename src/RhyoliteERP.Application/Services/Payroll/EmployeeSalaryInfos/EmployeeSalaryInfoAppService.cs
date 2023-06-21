using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeBioDatas;
using RhyoliteERP.DomainServices.Payroll.EmployeeSalaryInfos;
using RhyoliteERP.DomainServices.Payroll.EmployeeSalaryInfos.Dto;
using RhyoliteERP.DomainServices.Shared.Banks;
using RhyoliteERP.DomainServices.Shared.Currencies;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeSalaryInfos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeSalaryInfos
{
   public class EmployeeSalaryInfoAppService: RhyoliteERPAppServiceBase, IEmployeeSalaryInfoAppService
    {
        private readonly IEmployeeSalaryInfoManager _employeeSalaryInfoManager;
        private readonly IEmployeeBioDataManager _employeeBioDataManager;
        private readonly IBankManager _bankManager;
        private readonly ICurrencyManager _currencyManager;
        private readonly IMapper _mapper;

        public EmployeeSalaryInfoAppService(IEmployeeSalaryInfoManager employeeSalaryInfoManager, IMapper mapper, IEmployeeBioDataManager employeeBioDataManager, IBankManager bankManager, ICurrencyManager currencyManager)
        {
            _employeeSalaryInfoManager = employeeSalaryInfoManager;
            _mapper = mapper;
            _employeeBioDataManager = employeeBioDataManager;
            _bankManager = bankManager;
            _currencyManager = currencyManager;
        }

        public async Task<object> ListAll()
        {
            return await _employeeSalaryInfoManager.ListAll();
        }

        public async Task<object> GetAllBySalaryType(string salaryType)
        {
            return await _employeeSalaryInfoManager.GetAllBySalaryType(salaryType);
        }

        public async Task<object> GetAllBySalaryType(string salaryType, Guid categoryId)
        {
            return await _employeeSalaryInfoManager.GetAllBySalaryType(salaryType, categoryId);
        }

        public async Task<object> GetAllBySalaryGrade(string salaryType, Guid salaryGradeId, Guid salaryNotchId)
        {
            return await _employeeSalaryInfoManager.GetAllBySalaryGrade(salaryType, salaryGradeId, salaryNotchId);

        }
        public async Task<object> GetAsync(Guid employeeId)
        {
            return await _employeeSalaryInfoManager.GetAsync(employeeId);
        }

        public async Task Create(CreateEmployeeSalaryInfoInput input)
        {
            var obj = _mapper.Map<EmployeeSalaryInfo>(input);
            await _employeeSalaryInfoManager.Create(obj);
        }


        public async Task Import(List<ImportEmployeeSalaryInfoInput> inputList)
        {

            foreach (var input in inputList)
            {
                var employee = await _employeeBioDataManager.GetAsync(input.EmployeeIdentifier);
                var bankInfo = await _bankManager.GetAsync(input.BankName);
                var currencyInfo = await _currencyManager.GetAsync(input.CurrencyName);


                if (employee != null) 
                {
                    var employeeSalaryInfo = new CreateEmployeeSalaryInfoInput
                    {
                        EmployeeIdentifier = employee.EmployeeIdentifier,
                        EmployeeName = string.IsNullOrEmpty(employee.OtherName) ? $"{employee.LastName} {employee.FirstName}" : $"{employee.LastName} {employee.FirstName} {employee.OtherName}",
                        SalaryType = input.SalaryType,
                        EmployeeCategoryId = employee.CategoryId,
                        EmployeeId = employee.Id,
                        AccountNumber = input.AccountNumber,
                        BankBranchId = Guid.Empty,
                        PayType = input.PayType,
                        MonthlySalary = input.MonthlySalary,
                        PreviousSalary = 0,
                        BankId = bankInfo != null ? bankInfo.Id : Guid.Empty,
                        CurrencyId = currencyInfo != null ? currencyInfo.Id : Guid.Empty,
                        CurrencyName = currencyInfo != null ? currencyInfo.CurrencyName : string.Empty,
                        CurrentHourlyRate = 0,
                        DailyHours = 8,
                        EmployeePhoto = string.Empty,
                        EmployeeSalaryGradeId = Guid.Empty,
                        EmployeeSalaryNotchId = Guid.Empty,
                        VacationDaysPaid = 20,

                    };


                    var obj = _mapper.Map<EmployeeSalaryInfo>(employeeSalaryInfo);
                    await _employeeSalaryInfoManager.Create(obj);
                }
               


            }
        }
        public async Task ProcessSalaryIncrement(List<SalaryIncrement> salaryDataList)
        {
             await _employeeSalaryInfoManager.ProcessSalaryIncrement(salaryDataList);
        }

        public async Task Update(UpdateEmployeeSalaryInfoInput input)
        {
            var obj = _mapper.Map<EmployeeSalaryInfo>(input);
            await _employeeSalaryInfoManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeSalaryInfoManager.Delete(Id);

        }
    }
}
