using RhyoliteERP.DomainServices.School.PromotionHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.PromotionHistories
{
   public class PromotionHistoryAppService:RhyoliteERPAppServiceBase, IPromotionHistoryAppService
    {
        private readonly IPromotionHistoryManager _promotionHistoryManager;

        public PromotionHistoryAppService(IPromotionHistoryManager promotionHistoryManager)
        {
            _promotionHistoryManager = promotionHistoryManager;
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId)
        {
            return await _promotionHistoryManager.ListAll(academicYearId);
        }
    }
}
