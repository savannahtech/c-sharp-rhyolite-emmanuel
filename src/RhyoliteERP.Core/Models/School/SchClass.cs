using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class SchClass : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid LevelId { get; set; }
        public string LevelName { get; set; }
        public string ClassName { get; set; }
        public Guid StreamId { get; set; }
        public string StreamName { get; set; }
        public int TenantId { get; set; }
    }
}
