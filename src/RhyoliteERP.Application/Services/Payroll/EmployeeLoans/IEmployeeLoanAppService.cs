using RhyoliteERP.Services.Payroll.EmployeeLoans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeLoans
{
   public interface IEmployeeLoanAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateEmployeeLoanInput input);
        Task Update(UpdateEmployeeLoanInput input);
        Task Delete(Guid Id); 
        Task Approve(List<Guid> ids, string approvalType);
        Task<object> ListPendingLoans();
        Task<object> ListOutStandingLoans();
        Task<object> ListPastLoans();
    }
}
