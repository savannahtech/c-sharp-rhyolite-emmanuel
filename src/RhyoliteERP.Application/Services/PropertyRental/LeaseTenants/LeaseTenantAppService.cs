using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.LeaseTenants;
using RhyoliteERP.DomainServices.PropertyRental.MeterTypes;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.LeaseTenants.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeaseTenants
{
    public class LeaseTenantAppService: RhyoliteERPAppServiceBase, ILeaseTenantAppService
    {
        private readonly ILeaseTenantManager _leaseTenantManager;
        private readonly IMapper _mapper;

        public LeaseTenantAppService(ILeaseTenantManager leaseTenantManager, IMapper mapper)
        {
            _leaseTenantManager = leaseTenantManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _leaseTenantManager.ListAll();
        }


        public async Task<object> ListUnitTenants(Guid propertyId, Guid propertyUnitId)
        {
            return await _leaseTenantManager.GetUnitTenants(propertyId, propertyUnitId);
        }
        public async Task Create(CreateLeaseTenantInput input)
        {
            var obj = _mapper.Map<LeaseTenant>(input);
            await _leaseTenantManager.Create(obj);
        }

        public async Task Update(UpdateLeaseTenantInput input)
        {
            var obj = _mapper.Map<LeaseTenant>(input);
            await _leaseTenantManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _leaseTenantManager.Delete(Id);

        }

    }
}
