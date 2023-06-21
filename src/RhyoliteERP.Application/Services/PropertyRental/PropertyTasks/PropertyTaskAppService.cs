using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.PropertyTasks;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.PropertyTasks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyTasks
{
    public class PropertyTaskAppService: RhyoliteERPAppServiceBase, IPropertyTaskAppService
    {
        private readonly IPropertyTaskManager _propertyTaskManager;
        private readonly IMapper _mapper;

        public PropertyTaskAppService(IPropertyTaskManager propertyTaskManager, IMapper mapper)
        {
            _propertyTaskManager = propertyTaskManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _propertyTaskManager.ListAll();
        }

        public async Task Create(CreatePropertyTaskInput input)
        {
            var obj = _mapper.Map<PropertyTask>(input);
            await _propertyTaskManager.Create(obj);
        }

        public async Task Update(UpdatePropertyTaskInput input)
        {
            var obj = _mapper.Map<PropertyTask>(input);
            await _propertyTaskManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _propertyTaskManager.Delete(Id);

        }
    }
}
