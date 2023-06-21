using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.Journals
{
   public interface IJournalManager : Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> GetAsync(Guid id);
        Task Create(Journal input);
        Task BatchDelete(List<Guid> Ids);
    }
}
