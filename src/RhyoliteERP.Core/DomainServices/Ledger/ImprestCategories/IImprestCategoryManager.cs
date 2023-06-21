using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.ImprestCategories
{
   public interface IImprestCategoryManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(ImprestCategory input);
        Task Update(ImprestCategory input);
        Task Delete(Guid Id);
    }
}
