using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RhyoliteERP.Models.School
{
   public class AcademicYear : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TenantId { get; set; }
        public int PrecedenceNo { get; set; }
        [Column(TypeName = "jsonb")] public List<Term> Terms { get; set; }
    }
}
