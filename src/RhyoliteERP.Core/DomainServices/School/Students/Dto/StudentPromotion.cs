using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Students.Dto
{
   public class StudentPromotion
    {
        public DateTime DatePromoted { get; set; }
        public Guid PromotedFrom { get; set; }
        public Guid PromotedTo { get; set; }
        public List<Student> Students { get; set; }

    }

    public class AlumniStudentPromotion
    {
     
        public List<AlumniHistory> Students { get; set; }

    }
}
