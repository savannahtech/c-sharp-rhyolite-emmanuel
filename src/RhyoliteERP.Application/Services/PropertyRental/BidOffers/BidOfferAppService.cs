using Abp.Domain.Uow;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Authorization;
using RhyoliteERP.DomainServices.PropertyRental.BidOffers;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.BidOffers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.BidOffers
{
    public class BidOfferAppService: RhyoliteERPAppServiceBase, IBidOfferAppService
    {

        private readonly IBidOfferManager _bidOfferManager;
        private readonly IMapper _mapper;
        private readonly IValidateTenant _validateTenant;
        public BidOfferAppService(IBidOfferManager bidOfferManager, IMapper mapper, IValidateTenant validateTenant)
        {
            _bidOfferManager = bidOfferManager;
            _mapper = mapper;
            _validateTenant = validateTenant;
        }

        public async Task<object> ListAll()
        {
            return await _bidOfferManager.ListAll();
        }


        [HttpPost]
        public async Task Create(CreateBidOfferInput input)
        {

            //resolve tenant ids
            var tenantId = await _validateTenant.Validate();

            var obj = _mapper.Map<BidOffer>(input);
            obj.TenantId = tenantId;    
            await _bidOfferManager.Create(obj);
        }


        public async Task Delete(Guid Id)
        {
            await _bidOfferManager.Delete(Id);
        }
    }
}
