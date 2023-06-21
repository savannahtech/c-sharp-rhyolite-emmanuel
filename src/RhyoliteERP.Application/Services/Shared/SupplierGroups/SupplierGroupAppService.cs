using AutoMapper;
using RhyoliteERP.DomainServices.Shared.Religions;
using RhyoliteERP.DomainServices.Shared.SupplierGroups;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.Religions.Dto;
using RhyoliteERP.Services.Shared.SupplierGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.SupplierGroups
{
    public class SupplierGroupAppService: RhyoliteERPAppServiceBase, ISupplierGroupAppService
    {

        private readonly ISupplierGroupManager _supplierGroupManager;
        private readonly IMapper _mapper;

        public SupplierGroupAppService(ISupplierGroupManager supplierGroupManager, IMapper mapper)
        {
            _supplierGroupManager = supplierGroupManager;
            _mapper = mapper;
        }


        public async Task<object> ListAll()
        {
            return await _supplierGroupManager.ListAll();
        }

        public async Task<object> Create(CreateSupplierGroupInput input)
        {
            var obj = _mapper.Map<SupplierGroup>(input);
            return await _supplierGroupManager.Create(obj);
        }

        public async Task Update(UpdateSupplierGroupInput input)
        {
            var obj = _mapper.Map<SupplierGroup>(input);
            await _supplierGroupManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _supplierGroupManager.Delete(Id);

        }

    }
}
