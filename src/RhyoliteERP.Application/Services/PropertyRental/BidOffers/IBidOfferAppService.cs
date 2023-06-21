using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.BidOffers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.BidOffers
{
    public interface IBidOfferAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateBidOfferInput entity);
        Task Delete(Guid Id);
    }
}
