using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.CancelledBills
{
   public interface ICancelledBillManager: Abp.Domain.Services.IDomainService
    {
        Task CreateBatch(List<CancelledBill> bills);
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
    }
}
