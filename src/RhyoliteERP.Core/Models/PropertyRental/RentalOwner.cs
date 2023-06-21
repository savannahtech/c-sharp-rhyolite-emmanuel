using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class RentalOwner: Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string RentalOwnerIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public bool IsCompany { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public string Address { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string RegionOrState { get; set; }
        public string Comments { get; set; }
        public string RentalPropertiesOwned { get; set; }
        public int TenantId { get; set; }

    }
}
