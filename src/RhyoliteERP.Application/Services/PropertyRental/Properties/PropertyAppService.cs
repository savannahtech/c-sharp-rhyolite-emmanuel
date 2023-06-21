using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.Properties;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.Properties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Properties
{
    public class PropertyAppService: RhyoliteERPAppServiceBase, IPropertyAppService
    {
        private readonly IPropertyManager _propertyManager;
        private readonly IMapper _mapper;

        public PropertyAppService(IPropertyManager propertyManager, IMapper mapper)
        {
            _propertyManager = propertyManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _propertyManager.ListAll();
        }

        public async Task<object> ListAll(bool isRented)
        {
            return await _propertyManager.ListAll(isRented);
        }

        public async Task<object> Create(CreatePropertyInput input)
        {
            var obj = _mapper.Map<Property>(input);
            return await _propertyManager.Create(obj);
        }

        public async Task Update(UpdatePropertyInput input)
        {
            var obj = _mapper.Map<Property>(input);
            await _propertyManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _propertyManager.Delete(Id);

        }

    }
}
