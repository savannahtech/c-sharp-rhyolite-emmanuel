using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.ProcessPay
{
    public interface IProcessPayAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ProcessPayroll(Guid id);
        Task PostPayroll();
        Task<object> GetPayrollResults();
        Task<object> GetPayrollResults(Guid employeeId);

    }
}
