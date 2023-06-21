using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
    public class ResultType : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public Guid LevelId { get; set; }
        public string LevelName { get; set; }
        public int TenantId { get; set; }
    }
}
