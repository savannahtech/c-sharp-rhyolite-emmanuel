using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Students.Dto
{
   public class CreateStudentInput
    {
        public string StudentIdentifier { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid NationalityId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public Guid ReligionId { get; set; }
        public Guid ClassId { get; set; }
        public Guid AcademicYearId { get; set; }
        public string HomeAddress { get; set; }
        public string CityOrLocation { get; set; }
        public string StudImage { get; set; }
        public Guid StudentStatusId { get; set; }
        public string EnrollmentType { get; set; }
        public int TenantId { get; set; }
    }
}
