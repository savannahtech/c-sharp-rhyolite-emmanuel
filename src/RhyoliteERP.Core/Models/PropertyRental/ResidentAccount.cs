using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class ResidentAccount: Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid LeaseTenantId { get; set; }
        public string LeaseTenantIdentifier { get; set; }
        public string AccountCaption { get; set; } //propertyname - unit name : Tenant 1, Tenant 2, Tenant 3
        public decimal CurrentBalance { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
        //lease info
        public Guid LeasedPropertyId { get; set; }
        public string LeasedPropertyName { get; set; }
        public Guid LeasedPropertyUnitId { get; set; }
        public string LeasedPropertyUnitNo { get; set; }
        public int TenantId { get; set; }

    }
}
