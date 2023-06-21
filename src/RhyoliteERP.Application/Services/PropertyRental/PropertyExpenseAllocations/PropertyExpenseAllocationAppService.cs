using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.PropertyExpenseAllocations;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.PropertyExpenseAllocations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyExpenseAllocations
{
    public class PropertyExpenseAllocationAppService: RhyoliteERPAppServiceBase, IPropertyExpenseAllocationAppService
    {

        private readonly IPropertyExpenseAllocationManager _propertyExpenseAllocationManager;
        private readonly IMapper _mapper;

        public PropertyExpenseAllocationAppService(IPropertyExpenseAllocationManager propertyExpenseAllocationManager, IMapper mapper)
        {
            _propertyExpenseAllocationManager = propertyExpenseAllocationManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _propertyExpenseAllocationManager.ListAll();
        }

        public async Task Create(CreatePropertyExpenseAllocationInput input)
        {
            var obj = _mapper.Map<PropertyExpenseAllocation>(input);
            await _propertyExpenseAllocationManager.Create(obj);
        }

        public async Task Update(UpdatePropertyExpenseAllocationInput input)
        {
            var obj = _mapper.Map<PropertyExpenseAllocation>(input);
            await _propertyExpenseAllocationManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _propertyExpenseAllocationManager.Delete(Id);

        }

    }
}
