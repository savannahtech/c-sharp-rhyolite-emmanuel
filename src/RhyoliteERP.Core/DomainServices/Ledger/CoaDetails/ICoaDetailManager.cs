using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.CoaDetails
{
   public interface ICoaDetailManager : Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<IEnumerable<object>> ListActiveAccounts();
        Task<object> Create(CoaDetail entity);
        Task Update(CoaDetail entity);
        Task Delete(Guid Id);
    }
}
