using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.BidOffers.Dto
{
    public class CreateBidOfferInput
    {
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public decimal OfferAmount { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public Guid PropertyUnitId { get; set; }
        public string PropertyUnitNo { get; set; }
        public int TenantId { get; set; }
    }
}
