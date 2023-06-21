using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Staffs.Dto
{
   public class CreateStaffInput
    {
        public string StaffIdentifier { get; set; }
        public string StaffImage { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string SSN { get; set; }
        public DateTime DateEmployed { get; set; }
        public bool IsTeachingStaff { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HomeAddress { get; set; }
        public string CityOrLocation { get; set; }
        public string MaritalStatus { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string EmailAddress { get; set; }
        public string EmerConPerson { get; set; }
        public string EmerConPersonPhone { get; set; }
        public Guid NationalityId { get; set; }
        public Guid StaffStatusId { get; set; }
        public int TenantId { get; set; }
    }
}
