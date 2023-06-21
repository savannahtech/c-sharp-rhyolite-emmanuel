using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.CancelledPayments
{
   public interface ICancelledPaymentManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId);
        Task CreateBatch(List<CancelledPayment> payments);
    }
}
