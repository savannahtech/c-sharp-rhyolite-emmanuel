using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Conducts.Dto
{
   public class CreateConductInput
    {
        public Guid AcademicYearId { get; set; }
        public Guid TermId { get; set; }
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public string ConductText { get; set; }
        public int TenantId { get; set; }
    }
}
