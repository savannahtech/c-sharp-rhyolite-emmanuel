using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class TeacherRemark : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public Guid StudentId { get; set; }
        public string StudentIdentifier { get; set; }
        public string StudentName { get; set; }
        public string Remarks { get; set; }
        public int TenantId { get; set; }
    }
}
