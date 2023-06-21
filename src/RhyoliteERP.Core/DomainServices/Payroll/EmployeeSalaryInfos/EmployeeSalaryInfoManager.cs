using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Payroll.EmployeeSalaryInfos.Dto;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeSalaryInfos
{
   public class EmployeeSalaryInfoManager: Abp.Domain.Services.DomainService, IEmployeeSalaryInfoManager
    {
        private readonly IRepository<EmployeeSalaryInfo, Guid> _repositoryEmployeeSalaryInfo;
        private readonly IRepository<Bank, Guid> _repositoryBank;
        private readonly IRepository<Currency, Guid> _repositoryCurrency;
        private readonly IRepository<Models.Payroll.SalaryIncrementHistory, Guid> _repositorySalaryIncrementHistory;

        public EmployeeSalaryInfoManager(IRepository<EmployeeSalaryInfo, Guid> repositoryEmployeeSalaryInfo, IRepository<Models.Payroll.SalaryIncrementHistory, Guid> repositorySalaryIncrementHistory, IRepository<Bank, Guid> repositoryBank, IRepository<Currency, Guid> repositoryCurrency)
        {
            _repositoryEmployeeSalaryInfo = repositoryEmployeeSalaryInfo;
            _repositorySalaryIncrementHistory = repositorySalaryIncrementHistory;
            _repositoryBank = repositoryBank;
            _repositoryCurrency = repositoryCurrency;
        }

        public async Task Create(EmployeeSalaryInfo entity)
        {
            var datta = await _repositoryEmployeeSalaryInfo.FirstOrDefaultAsync(x => x.EmployeeId == entity.EmployeeId);

            if (datta == null)
            {
                var bankInfo = await _repositoryBank.FirstOrDefaultAsync(entity.BankId);
                if (bankInfo != null)
                {
                    entity.BankName = bankInfo.Name;
                    var branchInfo = bankInfo.BankBranches.FirstOrDefault(x=>x.Id == entity.BankBranchId);

                    if (branchInfo != null)
                    {
                        entity.BankBranchName = branchInfo.Name;
                    }
                }

                await _repositoryEmployeeSalaryInfo.InsertAsync(entity);
            }
            else
            {
                await _repositoryEmployeeSalaryInfo.UpdateAsync(entity);
            }
        }

        public async Task Update(EmployeeSalaryInfo entity)
        {
            await _repositoryEmployeeSalaryInfo.UpdateAsync(entity);
        }

        public async Task<object> GetAllBySalaryType(string salaryType)
        {

           return await _repositoryEmployeeSalaryInfo.GetAllListAsync(x=>x.SalaryType == salaryType);

        }

        public async Task<object> GetAllBySalaryType(string salaryType, Guid categoryId)
        {
            return await _repositoryEmployeeSalaryInfo.GetAllListAsync(x => x.SalaryType == salaryType && x.EmployeeCategoryId == categoryId);

        }

        public async Task<object> GetAllBySalaryGrade(string salaryType, Guid salaryGradeId, Guid salaryNotchId)
        {
            return await _repositoryEmployeeSalaryInfo.GetAllListAsync(x => x.SalaryType == salaryType && x.EmployeeSalaryGradeId == salaryGradeId && x.EmployeeSalaryNotchId == salaryNotchId);

        }

        public async Task<object> GetAsync(Guid employeeId)
        {
            return await _repositoryEmployeeSalaryInfo.FirstOrDefaultAsync(x=>x.EmployeeId == employeeId);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryEmployeeSalaryInfo.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryEmployeeSalaryInfo.DeleteAsync(id);

        }

        public async Task ProcessSalaryIncrement(List<SalaryIncrement> salaryDataList)
        {
            foreach (var salaryData in salaryDataList)
            {
                var employeeSalaryInfo = await _repositoryEmployeeSalaryInfo.FirstOrDefaultAsync(x=>x.EmployeeId == salaryData.EmployeeId);

                employeeSalaryInfo.MonthlySalary = salaryData.MonthlySalary;
                employeeSalaryInfo.PreviousSalary= salaryData.PreviousSalary;

                await _repositoryEmployeeSalaryInfo.UpdateAsync(employeeSalaryInfo);

                var salaryIncrementHistory = new Models.Payroll.SalaryIncrementHistory
                {
                    EmployeeId = salaryData.EmployeeId,
                    EmployeeIdentifier= employeeSalaryInfo.EmployeeIdentifier,
                    EmployeeName= employeeSalaryInfo.EmployeeName,
                    CurrentSalary = salaryData.MonthlySalary,
                    PreviousSalary = salaryData.PreviousSalary,
                    IncrementAmount = salaryData.IncrementAmount,
                };

                await _repositorySalaryIncrementHistory.InsertAsync(salaryIncrementHistory);


            }

        }
    }
}
