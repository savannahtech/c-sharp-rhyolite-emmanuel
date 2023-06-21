using AutoMapper;
using RhyoliteERP.DomainServices.Shared.SystemNumbers;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.SystemNumbers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.SystemNumbers
{
   public class SystemNumberAppService: RhyoliteERPAppServiceBase, ISystemNumberAppService
    {
        private readonly ISystemNumberManager _systemNumberManager;
        private readonly IMapper _mapper;

        public SystemNumberAppService(ISystemNumberManager systemNumberManager, IMapper mapper)
        {
            _systemNumberManager = systemNumberManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _systemNumberManager.ListAll();
        }

        public async Task<object> Create(CreateSystemNumberInput input)
        {
            var obj = _mapper.Map<SystemNumber>(input);
            return await _systemNumberManager.Create(obj);
        }

        public async Task Update(UpdateSystemNumberInput input)
        {
            var obj = _mapper.Map<SystemNumber>(input);
            await _systemNumberManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _systemNumberManager.Delete(Id);
        }

    }
}
