using AutoMapper;
using RhyoliteERP.DomainServices.Shared.Suppliers;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.Suppliers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Suppliers
{
    public class SupplierAppService: RhyoliteERPAppServiceBase, ISupplierAppService
    {

        private readonly ISupplierManager _supplierManager;
        private readonly IMapper _mapper;

        public SupplierAppService(ISupplierManager supplierManager, IMapper mapper)
        {
            _supplierManager = supplierManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _supplierManager.ListAll();
        }


        public async Task<object> ListAllByGroup(Guid groupId)
        {
            return await _supplierManager.ListAllByGroup(groupId);
        }

        public async Task<object> Create(CreateSupplierInput input)
        {
            var obj = _mapper.Map<Supplier>(input);
            return await _supplierManager.Create(obj);
        }

        public async Task Update(UpdateSupplierInput input)
        {
            var obj = _mapper.Map<Supplier>(input);
            await _supplierManager.Update(obj);
        }


        public async Task Delete(Guid id)
        {
            await _supplierManager.Delete(id);

        }

    }
}
