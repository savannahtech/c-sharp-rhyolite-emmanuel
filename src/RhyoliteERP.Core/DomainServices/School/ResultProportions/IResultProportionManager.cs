using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.ResultProportions
{
   public interface IResultProportionManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid levelId);
        Task<IEnumerable<object>> ListAll();
        Task Create(ResultProportion input);
        Task Update(ResultProportion input);
        Task Delete(Guid Id);

    }
}
