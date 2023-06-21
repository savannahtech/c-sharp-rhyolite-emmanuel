using RhyoliteERP.Services.Ledger.CoaDetails.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Ledger.CoaDetails
{
   public interface ICoaDetailAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListActiveAccounts();
        Task<object> Create(CreateCoaDetailInput input);
        Task Update(UpdateCoaDetailInput input);
        Task Delete(Guid Id);
    }
}
