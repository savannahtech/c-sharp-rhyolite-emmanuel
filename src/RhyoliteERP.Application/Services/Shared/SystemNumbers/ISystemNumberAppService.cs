using RhyoliteERP.Services.Shared.SystemNumbers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.SystemNumbers
{
   public interface ISystemNumberAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateSystemNumberInput input);
        Task Update(UpdateSystemNumberInput input);
        Task Delete(Guid Id);
    }
}
