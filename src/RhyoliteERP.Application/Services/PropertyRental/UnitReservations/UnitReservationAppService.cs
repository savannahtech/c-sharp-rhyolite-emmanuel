using Abp.Domain.Uow;
using AutoMapper;
using RhyoliteERP.Authorization;
using RhyoliteERP.DomainServices.PropertyRental.UnitReservations;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.UnitReservations.Dto;
using RhyoliteERP.Services.PropertyRental.VendorCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.UnitReservations
{
    public class UnitReservationAppService:RhyoliteERPAppServiceBase, IUnitReservationAppService
    {
        private readonly IUnitReservationManager _unitReservationManager;
        private readonly IValidateTenant _validateTenant;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMapper _mapper;

        public UnitReservationAppService(IUnitReservationManager unitReservationManager, IMapper mapper, IValidateTenant validateTenant, IUnitOfWorkManager unitOfWorkManager)
        {
            _unitReservationManager = unitReservationManager;
            _mapper = mapper;
            _validateTenant = validateTenant;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<object> ListAll()
        {
            return await _unitReservationManager.ListAll();
        }


        public async Task Create(CreateUnitReservationInput input)
        {
            var tenantId = await _validateTenant.Validate();

            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                var obj = _mapper.Map<UnitReservation>(input);
                obj.TenantId = tenantId;
                await _unitReservationManager.Create(obj);

            }
            
        }

        public async Task Delete(Guid Id)
        {
            await _unitReservationManager.Delete(Id);

        }
    }
}
