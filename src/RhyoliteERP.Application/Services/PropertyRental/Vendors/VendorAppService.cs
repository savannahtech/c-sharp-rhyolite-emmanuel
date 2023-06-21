using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.VendorCategories;
using RhyoliteERP.DomainServices.PropertyRental.Vendors;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.VendorCategories.Dto;
using RhyoliteERP.Services.PropertyRental.Vendors.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Vendors
{
    public class VendorAppService: RhyoliteERPAppServiceBase, IVendorAppService
    {
        private readonly IVendorManager _vendorManager;
        private readonly IMapper _mapper;

        public VendorAppService(IVendorManager vendorManager, IMapper mapper)
        {
            _vendorManager = vendorManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _vendorManager.ListAll();
        }

        public async Task<object> Create(CreateVendorInput input)
        {
            var obj = _mapper.Map<Vendor>(input);
            return await _vendorManager.Create(obj);
        }

        public async Task Update(UpdateVendorInput input)
        {
            var obj = _mapper.Map<Vendor>(input);
            await _vendorManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _vendorManager.Delete(Id);

        }
    }
}
