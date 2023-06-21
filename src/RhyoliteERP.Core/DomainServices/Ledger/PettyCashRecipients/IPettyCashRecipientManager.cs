using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.PettyCashRecipients
{
   public interface IPettyCashRecipientManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(PettyCashRecipient input);
        Task Update(PettyCashRecipient input);
        Task Delete(Guid Id);
    }
}
