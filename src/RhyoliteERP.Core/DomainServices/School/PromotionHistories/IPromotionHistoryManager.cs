using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.PromotionHistories
{
   public interface IPromotionHistoryManager: Abp.Domain.Services.IDomainService
    {
        Task Create(PromotionHistory Input);
        Task<IEnumerable<object>> ListAll(Guid academicYearId);
    }
}
