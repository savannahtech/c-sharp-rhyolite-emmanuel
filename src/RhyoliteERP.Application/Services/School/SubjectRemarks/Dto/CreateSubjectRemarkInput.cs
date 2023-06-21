using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SubjectRemarks.Dto
{
    public class CreateSubjectRemarkInput
    {
        public string MinimumMarks { get; set; }
        public string MaximumMarks { get; set; }
        public string Remarks { get; set; }
    }
}
