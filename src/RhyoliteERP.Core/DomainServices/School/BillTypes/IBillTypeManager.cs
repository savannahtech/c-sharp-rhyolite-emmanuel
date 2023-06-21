using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.BillTypes
{
   public interface IBillTypeManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(BillType input);
        Task Update(BillType input);
        Task Delete(Guid Id);
    }
}
