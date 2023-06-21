using AutoMapper;
using RhyoliteERP.DomainServices.Shared.CustomerGroups;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.CustomerGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.CustomerGroups
{
    public class CustomerGroupAppService: RhyoliteERPAppServiceBase, ICustomerGroupAppService
    {
        private readonly ICustomerGroupManager _customerGroupManager;
        private readonly IMapper _mapper;

        public CustomerGroupAppService(ICustomerGroupManager customerGroupManager, IMapper mapper)
        {
            _customerGroupManager = customerGroupManager;
            _mapper = mapper;
        }


        public async Task<object> ListAll()
        {
            return await _customerGroupManager.ListAll();
        }

        public async Task<object> Create(CreateCustomerGroupInput input)
        {
            var obj = _mapper.Map<CustomerGroup>(input);
            return await _customerGroupManager.Create(obj);
        }

        public async Task Update(UpdateCustomerGroupInput input)
        {
            var obj = _mapper.Map<CustomerGroup>(input);
            await _customerGroupManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _customerGroupManager.Delete(Id);

        }

    }
}
