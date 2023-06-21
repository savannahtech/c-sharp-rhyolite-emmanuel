using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Reports.Dto
{
   public class StudentsFinalResult
    {
        public Guid AcademicYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid TermId { get; set; }
        public Guid StudentId { get; set; }
        public int TenantId { get; set; }

        public List<FinalResult> FinalResults { get; set; }
    }

    public class FinalResult
    {
        public Guid AcademicYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid TermId { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ResultTypeId { get; set; }
        public decimal ClassScore { get; set; }
        public decimal ExamScore { get; set; }
    }

    public class TerminalReportDto
    {
        public int TenantId { get; set; }
        public List<StudentsFinalResult> StudentsFinalResult { get; set; }
    }
    
}
