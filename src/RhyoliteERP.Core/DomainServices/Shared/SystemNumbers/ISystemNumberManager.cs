using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.SystemNumbers
{
   public interface ISystemNumberManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<SystemNumber> GetByItemName(string itemName);
        Task<object> Create(SystemNumber input);
        Task Update(SystemNumber input);
        Task Delete(Guid Id);
    }
}
