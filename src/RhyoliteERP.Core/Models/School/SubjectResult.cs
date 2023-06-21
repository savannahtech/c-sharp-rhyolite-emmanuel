using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class SubjectResult
    {
        public Guid AcademicYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid TermId { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
        public decimal ClassScore { get; set; }
        public decimal ExamScore { get; set; }
        public decimal TotalScore { get; set; }
        public string SubjectRemarks { get; set; }

    }
}
