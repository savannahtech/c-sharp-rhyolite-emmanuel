using Abp.Application.Services;
using RhyoliteERP.Services.School.BillPayments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.BillPayments
{
   public interface IBillPaymentAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid studentId);
        Task<IEnumerable<object>> ListAllUnPosted(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListAllPosted(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListAllCreditMemos(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListDailyPayments(DateTime paymentDate);
        Task<object> Create(CreateBillPaymentInput input);
        Task CancelBatch(List<CancelBillPaymentInput> paymentList);
        Task<object> CreateCreditMemo(CreateBillPaymentInput input);
        Task Delete(Guid Id);
        Task PostPayments(IEnumerable<Guid> ids);


    }
}
