using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class TerminalReport : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {

        public Guid AcademicYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid TermId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentIdentifier { get; set; }
        public string ClassName { get; set; }
        public string AcademicYearName { get; set; }
        public string PromotedTo { get; set; }
        public decimal TermAverage { get; set; }
        public int NumberOnRoll { get; set; }
        public string TermName { get; set; }
        public string NextTermBegins { get; set; }
        public int Position { get; set; }
        public int Attendance { get; set; }
        public int TotalAttendanceDays { get; set; }
        public string Attitude { get; set; }
        public string Conduct { get; set; }
        public string ClassTeacher { get; set; }
        public string ClassTeacherRemarks { get; set; }
        public string HeadTeacherSignatureImageUrl { get; set; }
        [Column(TypeName = "jsonb")] public List<SubjectResult> SubjectResults { get; set; }
        public int TenantId { get; set; }


    }
}
