using RhyoliteERP.DomainServices.Payroll.EmployeeLoanRepaymentSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeLoanRepaymentSchedules
{
    public class EmployeeLoanRepaymentScheduleAppService: RhyoliteERPAppServiceBase, IEmployeeLoanRepaymentScheduleAppService
    {
        private readonly IEmployeeLoanRepaymentScheduleManager _employeeLoanRepaymentScheduleManager;

        public EmployeeLoanRepaymentScheduleAppService(IEmployeeLoanRepaymentScheduleManager employeeLoanRepaymentScheduleManager)
        {
            _employeeLoanRepaymentScheduleManager = employeeLoanRepaymentScheduleManager;
        }

        public async Task<object> ListAll()
        {
            return await _employeeLoanRepaymentScheduleManager.ListAll();
        }
    }
}
