using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Dashboard
{
   public interface ISchDashboardAppService: Abp.Application.Services.IApplicationService
    {
        Task<Dictionary<string, object>> GetStudentGenderDistribution();
        Task<Dictionary<string, object>> GetStudentGenderDistribution(Guid classId);
        Task<Dictionary<string, object>> GetStaffGenderDistribution();
        Task<IEnumerable<object>> GetPayments();
        Task<IEnumerable<object>> GetPaymentsByClass(Guid classId);
        Task<IEnumerable<object>> GetMonthlyPayments();
        Task<IEnumerable<object>> GetMonthlyPayments(Guid classId);
        Task<IEnumerable<object>> GetPaymentsTillDate();
    }
}
