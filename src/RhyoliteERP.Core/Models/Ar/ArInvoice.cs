using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ar
{
   public class ArInvoice : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? ProjectedPayDate { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string BatchNumber { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public Guid CustomerGroupId { get; set; }
        public string CustomerGroupName { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSms { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "jsonb")] public List<ArInvoiceDetail> Details { get; set; }
        public int TenantId { get; set; }
    }
}
