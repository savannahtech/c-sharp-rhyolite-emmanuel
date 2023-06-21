using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhyoliteERP.Services.Shared.BusinessProfile.Dto;

namespace RhyoliteERP.Services.Shared.BusinessProfile
{
    public interface IBusinessProfileAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> GetProfile();
        Task Create(CreateBusinessProfileInput input);
        Task Update(UpdateBusinessProfileInput input);
    }
}
