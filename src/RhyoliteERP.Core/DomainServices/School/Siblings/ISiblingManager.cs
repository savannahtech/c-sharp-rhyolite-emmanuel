using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Siblings
{
   public interface ISiblingManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid studentId);
        Task Create(Sibling input);
        Task Update(Sibling input);
        Task Delete(Guid Id);
    }
}
