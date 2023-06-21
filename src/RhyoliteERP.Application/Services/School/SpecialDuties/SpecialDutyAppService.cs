using AutoMapper;
using RhyoliteERP.DomainServices.School.SpecialDuties;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.SpecialDuties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SpecialDuties
{
   public class SpecialDutyAppService: RhyoliteERPAppServiceBase, ISpecialDutyAppService
    {
        private readonly ISpecialDutyManager _specialDutyManager;
        private readonly IMapper _mapper;

        public SpecialDutyAppService(ISpecialDutyManager specialDutyManager, IMapper mapper)
        {
            _specialDutyManager = specialDutyManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _specialDutyManager.ListAll();
        }

        public async Task<object> Create(CreateSpecialDutyInput input)
        {
            var obj = _mapper.Map<SpecialDuty>(input);
            return await _specialDutyManager.Create(obj);
        }

        public async Task Update(UpdateSpecialDutyInput input)
        {
            var obj = _mapper.Map<SpecialDuty>(input);
            await _specialDutyManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _specialDutyManager.Delete(Id);

        }
    }
}
