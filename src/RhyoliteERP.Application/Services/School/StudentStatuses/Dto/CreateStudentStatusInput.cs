using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentStatuses.Dto
{
   public class CreateStudentStatusInput
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
