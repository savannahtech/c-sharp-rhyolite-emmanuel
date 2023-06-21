using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SchoolProfile.Dto
{
   public class CreateSchoolProfileInput
    {
        public string SchoolName { get; set; }
        public Guid CurrentAcademicYearId { get; set; }
        public string CurrentAcademicYearName { get; set; }
        public Guid CurrentTermId { get; set; }
        public string CurrentTermName { get; set; }
        public string SecondaryEmailAddress { get; set; }
        public string RegionOrState { get; set; }
        public string City { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SchoolHead { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public string AssistantSchoolHead { get; set; }
        public string Website { get; set; }
        public string PostalAddress { get; set; }
        public string District { get; set; }
        public string StreetAddress { get; set; }
        public string Accountant { get; set; }

        //Default Values
        public bool AutoEmailReceiptNotification { get; set; }
        public bool AutoSMSReceiptNotification { get; set; }

        //Email Settings
        public string MailHostName { get; set; }
        public bool IsSSLEnabled { get; set; }
        public int PortNumber { get; set; }
        public string PrimaryEmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public string SchoolLogoUrl { get; set; }
        public int TenantId { get; set; }
    }
}
