using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class StudentAttendance : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int NoPresent { get; set; }
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public int TenantId { get; set; }
        [Column(TypeName = "jsonb")] public List<StudentAttendanceDetail> Details { get; set; }

    }
}
