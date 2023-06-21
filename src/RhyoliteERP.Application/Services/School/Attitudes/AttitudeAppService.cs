using AutoMapper;
using RhyoliteERP.DomainServices.School.Attitudes;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.Attitudes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Attitudes
{
   public class AttitudeAppService: RhyoliteERPAppServiceBase, IAttitudeAppService
    {
        private readonly IAttitudeManager _attitudeManager;
        private readonly IMapper _mapper;

        public AttitudeAppService(IAttitudeManager attitudeManager, IMapper mapper)
        {
            _attitudeManager = attitudeManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _attitudeManager.ListAll();
        }

        public async Task Create(CreateAttitudeInput input)
        {
            var obj = _mapper.Map<Attitude>(input);
            await _attitudeManager.Create(obj);
        }

        public async Task Update(UpdateAttitudeInput input)
        {
            var obj = _mapper.Map<Attitude>(input);
            await _attitudeManager.Create(obj);
        }

        public async Task Delete(Guid id)
        {
            await _attitudeManager.Delete(id);
        }
    }
}
