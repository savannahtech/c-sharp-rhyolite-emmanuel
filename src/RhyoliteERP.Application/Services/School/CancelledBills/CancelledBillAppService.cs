using RhyoliteERP.DomainServices.School.CancelledBills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.CancelledBills
{
   public class CancelledBillAppService: RhyoliteERPAppServiceBase, ICancelledBillAppService
    {
        private readonly ICancelledBillManager _cancelledBillManager;

        public CancelledBillAppService(ICancelledBillManager cancelledBillManager)
        {
            _cancelledBillManager = cancelledBillManager;
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _cancelledBillManager.ListAll(academicYearId, termId, classId, billTypeId);
        }


    }
}
