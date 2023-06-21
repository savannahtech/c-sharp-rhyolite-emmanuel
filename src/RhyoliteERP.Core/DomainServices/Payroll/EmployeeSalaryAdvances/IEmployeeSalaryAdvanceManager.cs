using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeSalaryAdvances
{
   public interface IEmployeeSalaryAdvanceManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> ListAll(Guid employeeId);
        Task Create(EmployeeSalaryAdvance input);
        Task Update(EmployeeSalaryAdvance input);
        Task Delete(Guid Id);
    }
}
