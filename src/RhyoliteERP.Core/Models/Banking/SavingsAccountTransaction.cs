using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Banking
{
    public class SavingsAccountTransaction:Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid SavingsAccountId { get; set; }
        public Guid CostCenterId { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal RunningBalanceDerived { get; set; }
        public decimal CumulativeBalanceDerived { get; set; }
        public bool IsReversed { get; set; }
        public int TenantId { get; set; }

    }
}
