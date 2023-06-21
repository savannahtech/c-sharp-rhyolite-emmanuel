using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class TenantOrCosigner
    {
        public Guid Id { get; set; }
        public string TenantIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SeconadaryPhoneNo { get; set; }
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
        public bool IsCosigner { get; set; }
        public Guid LeasedPropertyId { get; set; }
        public string LeasedPropertyName { get; set; }
        public Guid LeasedPropertyUnitId { get; set; }
        public string LeasedPropertyUnitNo { get; set; }

    }
}
