using RhyoliteERP.Services.Payroll.EmployeeSalaryAdvances.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeSalaryAdvances
{
   public interface IEmployeeSalaryAdvanceAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListAll(Guid employeeId);
        Task Create(CreateEmployeeSalaryAdvanceInput input);
        Task Delete(Guid Id);
    }
}
