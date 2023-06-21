using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.PropertyUnits;
using RhyoliteERP.DomainServices.Shared.SystemNumbers;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.PropertyUnits.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyUnits
{
    public class PropertyUnitAppService: RhyoliteERPAppServiceBase, IPropertyUnitAppService
    {

        private readonly IPropertyUnitManager _propertyUnitManager;
        private readonly ISystemNumberManager _systemNumberManager;
        private readonly IMapper _mapper;

        public PropertyUnitAppService(IPropertyUnitManager propertyUnitManager, IMapper mapper, ISystemNumberManager systemNumberManager)
        {
            _propertyUnitManager = propertyUnitManager;
            _mapper = mapper;
            _systemNumberManager = systemNumberManager;
        }

        public async Task<object> ListAll()
        {
            return await _propertyUnitManager.ListAll();
        }

        public async Task<object> ListAll(bool isRented = false)
        {
            return await _propertyUnitManager.ListAll(isRented);
        }

        public async Task<object> ListAll(Guid propertyId, bool isRented = false)
        {
            return await _propertyUnitManager.ListAll(propertyId, isRented);
        }
         
        public async Task<object> Create(CreatePropertyUnitInput input)
        {
            var resultList = new List<object>();

            if (input.AutoCreateUnits)
            {
                var systemNumberInfo = await _systemNumberManager.GetByItemName("PropertyUnitNo");

                int systemNumberLastNo = 1;

                if (systemNumberInfo != null)
                {
                    systemNumberLastNo = systemNumberInfo.LastNo;
                }

                for (int i = 0; i < input.NoOfUnitsPerProperty; i++)
                {
                    input.UnitNo = $"{systemNumberInfo.Prefix}{systemNumberLastNo}{systemNumberInfo.Suffix}";

                    var propertyUnit = _mapper.Map<PropertyUnit>(input);

                    var result = await _propertyUnitManager.Create(propertyUnit);

                    resultList.Add(result);

                    systemNumberLastNo += 1;
                }

                systemNumberInfo.LastNo= systemNumberLastNo;
                await _systemNumberManager.Update(systemNumberInfo);
                return resultList;
            }
            else
            {
                var obj = _mapper.Map<PropertyUnit>(input);

                return await _propertyUnitManager.Create(obj);
            }

           
        }

        public async Task Update(UpdatePropertyUnitInput input)
        {
            var obj = _mapper.Map<PropertyUnit>(input);
            await _propertyUnitManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _propertyUnitManager.Delete(Id);

        }

    }
}
