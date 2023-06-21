using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.BidOffers
{
    public class BidOfferManager: Abp.Domain.Services.DomainService, IBidOfferManager
    {
        private readonly IRepository<BidOffer, Guid> _repositoryBidOffer;

        public BidOfferManager(IRepository<BidOffer, Guid> repositoryBidOffer)
        {
            _repositoryBidOffer = repositoryBidOffer;
        }


        public async Task<object> ListAll()
        {
            return await _repositoryBidOffer.GetAllListAsync();
        }


        public async Task Create(BidOffer entity)
        {
            await _repositoryBidOffer.InsertAsync(entity);
        }


        public async Task Delete(Guid Id)
        {
            await _repositoryBidOffer.DeleteAsync(Id);
        }
    }
}
