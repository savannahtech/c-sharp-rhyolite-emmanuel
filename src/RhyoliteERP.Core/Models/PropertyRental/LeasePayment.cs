using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class LeasePayment : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        
        public Guid LeaseTenantId { get; set; }
        public string LeaseTenantIdentifier { get; set; }
        public Guid ResidentAccountId { get; set; }
        public string ResidentAccountName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public Guid RecievedFromId { get; set; }
        public string RecievedFromName { get; set; }
        public string Memo { get; set; }
        public string AttachmentFileUrl { get; set; }
        public Guid LeaseId { get; set; }
        public Guid RevenueAccountId { get; set; }
        public int TenantId { get; set; }
    }
}
