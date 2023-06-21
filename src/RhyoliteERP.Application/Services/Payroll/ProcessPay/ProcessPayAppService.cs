using RhyoliteERP.DomainServices.Payroll.ProcessPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.ProcessPay
{
    public class ProcessPayAppService: RhyoliteERPAppServiceBase, IProcessPayAppService
    {

        private readonly IProcessPayManager _processPayManager;

        public ProcessPayAppService(IProcessPayManager processPayManager)
        {
            _processPayManager = processPayManager;
        }

        public async Task PostPayroll()
        {
             await _processPayManager.PostPayroll();
        }

        public async Task<object> ProcessPayroll(Guid id)
        {
           return await _processPayManager.ProcessPayroll(id);
        }


        public async Task<object> GetPayrollResults()
        {
            return await _processPayManager.GetPayrollResults();
        }

        public async Task<object> GetPayrollResults(Guid employeeId)
        {
            return await _processPayManager.GetPayrollResults(employeeId);
        }

    }
}
