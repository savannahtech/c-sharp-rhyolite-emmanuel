using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentAttendances.Dto
{
   public class CreateStudentAttendanceInput
    {
        public Guid ClassId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int NoPresent { get; set; }
        public List<StudentAttendanceDetail> Details { get; set; }
    }
}
