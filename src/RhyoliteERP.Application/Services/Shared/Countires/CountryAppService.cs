using AutoMapper;
using RhyoliteERP.DomainServices.Shared.Countries;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.Countires.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Countires
{
    public class CountryAppService : RhyoliteERPAppServiceBase, ICountryAppService
    {

        private readonly ICountryManager _countryManager;
        private readonly IMapper _mapper;

        public CountryAppService(ICountryManager countryManager, IMapper mapper)
        {
            _countryManager = countryManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _countryManager.ListAll();
        }

        public async Task<object> Create(CreateCountryInput input)
        {
            var obj = _mapper.Map<Country>(input);
            return await _countryManager.Create(obj);
        }

        public async Task Update(UpdateCountryInput input)
        {
            var obj = _mapper.Map<Country>(input);
            await _countryManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _countryManager.Delete(Id);
        }

    }
}
