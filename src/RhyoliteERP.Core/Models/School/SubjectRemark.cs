using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class SubjectRemark : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string MinimumMarks { get; set; }
        public string MaximumMarks { get; set; }
        public string Remarks { get; set; }
        public int TenantId { get; set; }

    }
}
