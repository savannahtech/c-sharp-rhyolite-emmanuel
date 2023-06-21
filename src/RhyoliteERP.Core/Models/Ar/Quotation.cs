using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ar
{
   public class Quotation : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string QuotationNumber { get; set; }
        public string Notes { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSms { get; set; }
        public bool IsConvertedToInvoice { get; set; }
        public DateTime QuotationDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "jsonb")] public List<QuotationDetail> Details { get; set; }
        public int TenantId { get; set; }
    }
}
