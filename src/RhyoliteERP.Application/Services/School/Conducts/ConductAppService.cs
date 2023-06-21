using AutoMapper;
using RhyoliteERP.DomainServices.School.Conducts;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.Conducts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Conducts
{
   public class ConductAppService: RhyoliteERPAppServiceBase, IConductAppService
    {

        private readonly IConductManager _attitudeManager;
        private readonly IMapper _mapper;

        public ConductAppService(IConductManager attitudeManager, IMapper mapper)
        {
            _attitudeManager = attitudeManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _attitudeManager.ListAll();
        }

        public async Task Create(CreateConductInput input)
        {
            var obj = _mapper.Map<Conduct>(input);
            await _attitudeManager.Create(obj);
        }

        public async Task Update(UpdateConductInput input)
        {
            var obj = _mapper.Map<Conduct>(input);
            await _attitudeManager.Create(obj);
        }

        public async Task Delete(Guid id)
        {
            await _attitudeManager.Delete(id);
        }
    }
}
