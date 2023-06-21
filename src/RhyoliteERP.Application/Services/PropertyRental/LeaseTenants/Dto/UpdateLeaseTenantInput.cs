using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeaseTenants.Dto
{
    public class UpdateLeaseTenantInput:EntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public string PrimaryEmail { get; set; }
        public string SeconadaryEmail { get; set; }
        //personal information
        public DateTime DateOfBirth { get; set; }
        public string TaxIdentificationNo { get; set; }
        public string Comments { get; set; }
        //emergency contact
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhoneNo { get; set; }
        public string EmergencyContactEmail { get; set; }
        public string EmergencyContactRelationshipToTenant { get; set; }

        //cosigner fields
        public string Address { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string RegionOrState { get; set; }
        public string City { get; set; }
        public int TenantId { get; set; }
    }
}
