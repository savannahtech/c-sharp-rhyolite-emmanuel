using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeSalaryAdvances
{
   public class EmployeeSalaryAdvanceManager: Abp.Domain.Services.DomainService, IEmployeeSalaryAdvanceManager
    {
        private readonly IRepository<EmployeeSalaryAdvance, Guid> _repositoryEmployeeSalaryAdvance;

        public EmployeeSalaryAdvanceManager(IRepository<EmployeeSalaryAdvance, Guid> repositoryEmployeeSalaryAdvance)
        {
            _repositoryEmployeeSalaryAdvance = repositoryEmployeeSalaryAdvance;
        }

        public async Task Create(EmployeeSalaryAdvance entity)
        {
            await _repositoryEmployeeSalaryAdvance.InsertAsync(entity);

        }

        public async Task Update(EmployeeSalaryAdvance entity)
        {
            await _repositoryEmployeeSalaryAdvance.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryEmployeeSalaryAdvance.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryEmployeeSalaryAdvance.GetAllListAsync();
        }

        public async Task<object> ListAll(Guid employeeId)
        {
            return await _repositoryEmployeeSalaryAdvance.GetAllListAsync(x=> x.EmployeeId == employeeId);
        }

        public async Task Delete(Guid id)
        {
            await _repositoryEmployeeSalaryAdvance.DeleteAsync(id);
        }
    }
}
