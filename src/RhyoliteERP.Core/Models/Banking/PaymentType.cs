using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Banking
{
    public class PaymentType : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string DisplayName { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }

    }
}
