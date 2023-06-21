using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class FeesDescription : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string Description { get; set; }
        public Guid BillTypeId { get; set; }
        public string BillTypeName { get; set; }
        public int TenantId { get; set; }
    }
}
