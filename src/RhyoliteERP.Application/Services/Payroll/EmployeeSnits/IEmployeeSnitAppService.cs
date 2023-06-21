using RhyoliteERP.Services.Payroll.EmployeeSnits.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeSnits
{
   public interface IEmployeeSnitAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateEmployeeSnitInput input);
        Task Import(List<ImportEmployeeSnitInput> inputList);
        Task<object> GetAsync(Guid Id);
        Task Update(UpdateEmployeeSnitInput input);
        Task Delete(Guid Id);
    }
}
