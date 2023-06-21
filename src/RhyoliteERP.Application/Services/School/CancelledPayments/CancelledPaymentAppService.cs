using RhyoliteERP.DomainServices.School.CancelledPayments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.CancelledPayments
{
   public class CancelledPaymentAppService: RhyoliteERPAppServiceBase, ICancelledPaymentAppService
    {

        private readonly ICancelledPaymentManager _cancelledPaymentManager;

        public CancelledPaymentAppService(ICancelledPaymentManager cancelledPaymentManager)
        {
            _cancelledPaymentManager = cancelledPaymentManager;
        }


        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _cancelledPaymentManager.ListAll(academicYearId, termId, classId);
        }
    }
}
