using RhyoliteERP.DomainServices.School.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Dashboard
{
   public class SchDashboardAppService: RhyoliteERPAppServiceBase, ISchDashboardAppService
    {
        private readonly IDashboardManager _dashboardManager;

        public SchDashboardAppService(IDashboardManager dashboardManager)
        {
            _dashboardManager = dashboardManager;
        }

        public async Task<Dictionary<string, object>> GetStudentGenderDistribution()
        {
            return  await _dashboardManager.GetStudentGenderDistribution();
        }

        public async Task<Dictionary<string, object>> GetStudentGenderDistribution(Guid classId)
        {
            return await _dashboardManager.GetStudentGenderDistribution(classId);
        }

        public async Task<Dictionary<string, object>> GetStaffGenderDistribution()
        {
            return await _dashboardManager.GetStaffGenderDistribution();

        }

        public async Task<IEnumerable<object>> GetPayments()
        {
            return await _dashboardManager.GetPayments();

        }

        public async Task<IEnumerable<object>> GetPaymentsByClass(Guid classId)
        {
            return await _dashboardManager.GetPaymentsByClass(classId);
        }

        public async Task<IEnumerable<object>> GetMonthlyPayments()
        {
            return await _dashboardManager.GetMonthlyPayments();
        }


        public async Task<IEnumerable<object>> GetMonthlyPayments(Guid classId)
        {
            return await _dashboardManager.GetMonthlyPayments(classId);
        }

        public async Task<IEnumerable<object>> GetPaymentsTillDate()
        {
            return await _dashboardManager.GetPaymentsTillDate();
        }
    }
}
