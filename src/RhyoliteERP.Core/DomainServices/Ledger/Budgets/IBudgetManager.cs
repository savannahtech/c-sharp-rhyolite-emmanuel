using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.Budgets
{
   public interface IBudgetManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(Budget input);
        Task Update(Budget input);
        Task Delete(Guid Id);
    }
}
