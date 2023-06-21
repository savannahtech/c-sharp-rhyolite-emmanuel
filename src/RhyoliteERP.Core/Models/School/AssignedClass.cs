using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class AssignedClass : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public Guid StaffId { get; set; }
        public string StaffName { get; set; }
        public int TenantId { get; set; }
        [Column(TypeName = "jsonb")] public List<AssignedClassDetail> Details { get; set; }

    }
}
