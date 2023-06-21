using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Dashboard
{
   public interface IDashboardManager: Abp.Domain.Services.IDomainService
    {
        Task<Dictionary<string, object>> GetStudentGenderDistribution();
        Task<Dictionary<string, object>> GetStudentGenderDistribution(Guid classId);
        Task<Dictionary<string, object>> GetStaffGenderDistribution();
        //payments by academic year=>(default:current academic year)
        Task<IEnumerable<object>> GetPayments();
        Task<IEnumerable<object>> GetPaymentsByClass(Guid classId);
        Task<IEnumerable<object>> GetMonthlyPayments();
        Task<IEnumerable<object>> GetMonthlyPayments(Guid classId);
        Task<IEnumerable<object>> GetPaymentsTillDate();
    }
}
