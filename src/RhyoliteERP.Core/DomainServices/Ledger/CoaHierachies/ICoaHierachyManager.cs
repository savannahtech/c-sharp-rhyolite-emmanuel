using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.CoaHierachies
{
   public interface ICoaHierachyManager : Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CoaHierachy input);
        Task Update(CoaHierachy input);
        Task Delete(Guid Id);
    }
}
