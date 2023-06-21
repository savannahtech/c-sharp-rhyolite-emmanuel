using AutoMapper;
using RhyoliteERP.DomainServices.School.FeesDescriptions;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.FeesDescriptions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.FeesDescriptions
{
   public class FeesDescriptionAppService: RhyoliteERPAppServiceBase, IFeesDescriptionAppService
    {
        private readonly IFeesDescriptionManager _feesDescriptionManager;
        private readonly IMapper _mapper;

        public FeesDescriptionAppService(IFeesDescriptionManager feesDescriptionManager, IMapper mapper)
        {
            _feesDescriptionManager = feesDescriptionManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _feesDescriptionManager.ListAll();
        }

        public async Task<IEnumerable<object>> ListAll(Guid billTypeId)
        {
            return await _feesDescriptionManager.ListAll(billTypeId);
        }


        public async Task<object> Create(CreateFeesDescriptionInput input)
        {
            var obj = _mapper.Map<FeesDescription>(input);
            return await _feesDescriptionManager.Create(obj);
        }

        public async Task Update(UpdateFeesDescriptionInput input)
        {
            var obj = _mapper.Map<FeesDescription>(input);
            await _feesDescriptionManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _feesDescriptionManager.Delete(Id);

        }
    }
}
