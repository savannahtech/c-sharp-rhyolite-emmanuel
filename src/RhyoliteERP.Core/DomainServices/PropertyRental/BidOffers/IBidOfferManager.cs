using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.BidOffers
{
    public interface IBidOfferManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(BidOffer entity);
        Task Delete(Guid Id);

        // user
    }
}
