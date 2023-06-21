using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.SchClasses
{
   public interface ISchClassManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid levelId);
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(SchClass input);
        Task<object> GetAsync(Guid id);
        Task Update(SchClass input);
        Task Delete(Guid Id);
    }
}
