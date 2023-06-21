using Abp.Domain.Uow;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Authorization;
using RhyoliteERP.DomainServices.PropertyRental.ScheduledTours;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.BidOffers.Dto;
using RhyoliteERP.Services.PropertyRental.ScheduledTours.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.ScheduledTours
{
    public class ScheduledTourAppService: RhyoliteERPAppServiceBase, IScheduledTourAppService
    {
        private readonly IScheduledTourManager _scheduledTourManager;
        private readonly IMapper _mapper;
        private readonly IValidateTenant _validateTenant;
        public ScheduledTourAppService(IScheduledTourManager scheduledTourManager, IMapper mapper, IValidateTenant validateTenant)
        {
            _scheduledTourManager = scheduledTourManager;
            _mapper = mapper;
            _validateTenant = validateTenant;
        }

        public async Task<object> ListAll()
        {
            return await _scheduledTourManager.ListAll();
        }


        [HttpPost]
        public async Task Create(CreateScheduledTourInput input)
        {

            //resolve tenant ids
            var tenantId = await _validateTenant.Validate();

            var obj = _mapper.Map<ScheduledTour>(input);
            obj.TenantId = tenantId;
            await _scheduledTourManager.Create(obj);
        }


        public async Task Delete(Guid Id)
        {
            await _scheduledTourManager.Delete(Id);
        }

    }
}
