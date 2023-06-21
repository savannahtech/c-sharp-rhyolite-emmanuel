using AutoMapper;
using RhyoliteERP.DomainServices.Shared.CustomerGroups;
using RhyoliteERP.DomainServices.Shared.Customers;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.CustomerGroups.Dto;
using RhyoliteERP.Services.Shared.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Customers
{
    public class CustomerAppService: RhyoliteERPAppServiceBase, ICustomerAppService
    {

        private readonly ICustomerManager _customerManager;
        private readonly IMapper _mapper;

        public CustomerAppService(ICustomerManager customerManager, IMapper mapper)
        {
            _customerManager = customerManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _customerManager.ListAll();
        }


        public async Task<object> ListAllByGroup(Guid groupId)
        {
            return await _customerManager.ListAllByGroup(groupId);
        }

        public async Task<object> Create(CreateCustomerInput input)
        {
            var obj = _mapper.Map<Customer>(input);
            return await _customerManager.Create(obj);
        }

        public async Task Update(UpdateCustomerInput input)
        {
            var obj = _mapper.Map<Customer>(input);
            await _customerManager.Update(obj);
        }


        public async Task Delete(Guid id)
        {
            await _customerManager.Delete(id);

        }

    }
}
