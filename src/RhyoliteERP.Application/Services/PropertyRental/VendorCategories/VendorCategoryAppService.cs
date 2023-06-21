using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.VendorCategories;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.VendorCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.VendorCategories
{
    public class VendorCategoryAppService: RhyoliteERPAppServiceBase, IVendorCategoryAppService
    {
        private readonly IVendorCategoryManager _meterTypeManager;
        private readonly IMapper _mapper;

        public VendorCategoryAppService(IVendorCategoryManager meterTypeManager, IMapper mapper)
        {
            _meterTypeManager = meterTypeManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _meterTypeManager.ListAll();
        }

        public async Task<object> Create(CreateVendorCategoryInput input)
        {
            var obj = _mapper.Map<VendorCategory>(input);
            return await _meterTypeManager.Create(obj);
        }

        public async Task Update(UpdateVendorCategoryInput input)
        {
            var obj = _mapper.Map<VendorCategory>(input);
            await _meterTypeManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _meterTypeManager.Delete(Id);

        }
    }
}
