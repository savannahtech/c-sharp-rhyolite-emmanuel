using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AcademicYears.Dto
{
   public class TermInput
    {
        public Guid AcademicYearId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PrecedenceNo { get; set; }
        public int NoOfDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
