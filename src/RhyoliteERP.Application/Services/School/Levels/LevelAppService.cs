using AutoMapper;
using RhyoliteERP.DomainServices.School.Levels;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.Levels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Levels
{
   public class LevelAppService: RhyoliteERPAppServiceBase, ILevelAppService
    {
        private readonly ILevelManager _levelManager;
        private readonly IMapper _mapper;

        public LevelAppService(ILevelManager billTypeManager, IMapper mapper)
        {
            _levelManager = billTypeManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _levelManager.ListAll();
        }

        public async Task<object> Create(CreateLevelInput input)
        {
            var obj = _mapper.Map<Level>(input);
            return await _levelManager.Create(obj);
        }

        public async Task Update(UpdateLevelInput input)
        {
            var obj = _mapper.Map<Level>(input);
            await _levelManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _levelManager.Delete(Id);

        }
    }
}
