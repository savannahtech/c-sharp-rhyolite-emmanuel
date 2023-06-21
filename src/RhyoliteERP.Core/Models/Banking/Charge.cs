using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Banking
{
    public class Charge : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string Name { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public bool IsPenalty { get; set; }
        public int FeeFrequency { get; set; }
        public int ChargePaymentMode { get; set; }
        public Guid IncomeOrLiabilityAccountId { get; set; }
        public Guid TaxGroupId { get; set; }
        public int TenantId { get; set; }

    }
}
