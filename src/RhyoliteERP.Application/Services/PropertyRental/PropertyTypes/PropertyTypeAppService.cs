using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.PropertyTypes;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.PropertyTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyTypes
{
    public class PropertyTypeAppService: RhyoliteERPAppServiceBase, IPropertyTypeAppService
    {
        private readonly IPropertyTypeManager _propertyTypeManager;
        private readonly IMapper _mapper;

        public PropertyTypeAppService(IPropertyTypeManager propertyTypeManager, IMapper mapper)
        {
            _propertyTypeManager = propertyTypeManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _propertyTypeManager.ListAll();
        }

        public async Task<object> Create(CreatePropertyTypeInput input)
        {
            var obj = _mapper.Map<PropertyType>(input);
            return await _propertyTypeManager.Create(obj);
        }

        public async Task Update(UpdatePropertyTypeInput input)
        {
            var obj = _mapper.Map<PropertyType>(input);
            await _propertyTypeManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _propertyTypeManager.Delete(Id);

        }
    }
}
