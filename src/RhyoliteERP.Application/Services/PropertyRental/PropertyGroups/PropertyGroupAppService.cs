using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.MeterTypes;
using RhyoliteERP.DomainServices.PropertyRental.PropertyGroups;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.PropertyGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyGroups
{
    public class PropertyGroupAppService: RhyoliteERPAppServiceBase, IPropertyGroupAppService
    {

        private readonly IPropertyGroupManager _propertyGroupManager;
        private readonly IMapper _mapper;

        public PropertyGroupAppService(IPropertyGroupManager propertyGroupManager, IMapper mapper)
        {
            _propertyGroupManager = propertyGroupManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _propertyGroupManager.ListAll();
        }

        public async Task<object> Create(CreatePropertyGroupInput input)
        {
            var obj = _mapper.Map<PropertyGroup>(input);
            return await _propertyGroupManager.Create(obj);
        }

        public async Task Update(UpdatePropertyGroupInput input)
        {
            var obj = _mapper.Map<PropertyGroup>(input);
            await _propertyGroupManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _propertyGroupManager.Delete(Id);

        }

    }
}
