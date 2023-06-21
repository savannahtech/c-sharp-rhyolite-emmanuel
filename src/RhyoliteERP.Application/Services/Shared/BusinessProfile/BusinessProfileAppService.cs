using AutoMapper;
using RhyoliteERP.DomainServices.Shared.BusinessProfile;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.BusinessProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.BusinessProfile
{
    public class BusinessProfileAppService:RhyoliteERPAppServiceBase, IBusinessProfileAppService
    {
        private readonly IBusinessProfileManager _businessProfileManager;
        private readonly IMapper _mapper;

        public BusinessProfileAppService(IBusinessProfileManager businessProfileManager, IMapper mapper)
        {
            _businessProfileManager = businessProfileManager;
            _mapper = mapper;
        }


        public async Task<object> GetProfile()
        {
             return await _businessProfileManager.GetProfile();
        }


        public async Task Create(CreateBusinessProfileInput input)
        {
            var obj = _mapper.Map<CompanyProfile>(input);
            await _businessProfileManager.Create(obj);
        }


        public async Task Update(UpdateBusinessProfileInput input)
        {
            var obj = _mapper.Map<CompanyProfile>(input);
            await _businessProfileManager.Update(obj);
        }
    }
}
