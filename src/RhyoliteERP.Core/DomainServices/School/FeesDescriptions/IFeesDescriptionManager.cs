using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.FeesDescriptions
{
   public interface IFeesDescriptionManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<IEnumerable<object>> ListAll(Guid billTypeId);
        Task<object> Create(FeesDescription input);
        Task Update(FeesDescription input);
        Task Delete(Guid Id);
    }

}
