using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeasePayments.Dto
{
    public class CreateLeasePaymentInput
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public Guid RecievedFromId { get; set; }
        public string RecievedFromName { get; set; }
        public string Memo { get; set; }
        public string AttachmentFileUrl { get; set; }
    }
}
