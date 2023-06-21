using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.BillPayments
{
   public interface IBillPaymentManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid studentId);
        Task<IEnumerable<object>> ListAllUnPosted(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListAllPosted(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListAllCreditMemos(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListDailyPayments(DateTime paymentDate);
        Task<object> Create(BillPayment input);
        Task CancelPayment(BillPayment payment);
        Task<object> CreateCreditMemo(BillPayment input);
        Task Delete(Guid Id);
        Task PostPayments(IEnumerable<Guid> ids);
    }
}
