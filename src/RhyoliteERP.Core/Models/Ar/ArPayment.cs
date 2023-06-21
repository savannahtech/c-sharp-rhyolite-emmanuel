using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ar
{
   public class ArPayment : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime BatchDate { get; set; }
        public decimal AmountToPay { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string ModeOfPayment { get; set; }
        public Guid OuId { get; set; }
        public string OuName { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public string ReferenceNo { get; set; }
        public Guid BankAccountId { get; set; }
        public string BankAccountName { get; set; }
        public string BatchNumber { get; set; }
        public string BatchDescription { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "jsonb")] public List<ArPaymentDetail> Details { get; set; }
        public int TenantId { get; set; }
    }
}
