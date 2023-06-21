using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.SpecialDuties
{
   public interface ISpecialDutyManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(SpecialDuty input);
        Task Update(SpecialDuty input);
        Task Delete(Guid id);
    }
}
