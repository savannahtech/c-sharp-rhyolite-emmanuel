using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.MeterTypes;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.MeterTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.MeterTypes
{
    public class MeterTypeAppService: RhyoliteERPAppServiceBase, IMeterTypeAppService
    {

        private readonly IMeterTypeManager _meterTypeManager;
        private readonly IMapper _mapper;

        public MeterTypeAppService(IMeterTypeManager meterTypeManager, IMapper mapper)
        {
            _meterTypeManager = meterTypeManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _meterTypeManager.ListAll();
        }

        public async Task<object> Create(CreateMeterTypeInput input)
        {
            var obj = _mapper.Map<MeterType>(input);
            return await _meterTypeManager.Create(obj);
        }

        public async Task Update(UpdateMeterTypeInput input)
        {
            var obj = _mapper.Map<MeterType>(input);
            await _meterTypeManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _meterTypeManager.Delete(Id);

        }
    }
}
