using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ap
{
   public class ApInvoice : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string AccountNumber { get; set; }
        public string BatchNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime ProjectedPayDate { get; set; }
        public decimal Balance { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string VoucherNumber { get; set; }
        public Guid LiabilityAccountId { get; set; }
        public string LiabilityAccountName { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSms { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public decimal TotalInvoiceAmount { get; set; }
        public bool VatWithHeld { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "jsonb")] public List<ApInvoiceDetail> Details { get; set; }
        public int TenantId { get; set; }
    }
}
