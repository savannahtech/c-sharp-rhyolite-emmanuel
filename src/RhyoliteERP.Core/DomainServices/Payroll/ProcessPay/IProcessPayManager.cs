using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.ProcessPay
{
    public interface IProcessPayManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ProcessPayroll(Guid id);
        Task PostPayroll();
        Task<object> GetPayrollResults();
        Task<object> GetPayrollResults(Guid employeeId);
    }
}
