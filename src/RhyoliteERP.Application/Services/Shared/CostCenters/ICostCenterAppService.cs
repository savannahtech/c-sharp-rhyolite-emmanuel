using RhyoliteERP.Services.Shared.Banks.Dto;
using RhyoliteERP.Services.Shared.CostCenters.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.CostCenters
{
    public interface ICostCenterAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateCostCenterInput input);
        Task Update(UpdateCostCenterInput input);
        Task Delete(Guid Id);
    }
}
