using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeLoans
{
   public interface IEmployeeLoanManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll(); 
        Task<object> ListOutStandingLoans();
        Task<object> ListPastLoans();
        Task<object> ListPendingLoans();
        Task Approve(List<Guid> ids, string approvalType);
        Task<object> ListAll(Guid employeeId);
        Task Create(EmployeeLoan input);
        Task Update(EmployeeLoan input);
        Task Delete(Guid Id);
    }
}
