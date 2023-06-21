using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentParents.Dto
{
   public class CreateStudentParentInput
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
    }
}
