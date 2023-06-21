using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class Vendor: Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {

        public string VendorIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public Guid VendorCategoryId { get; set; }
        public string VendorCategoryName { get; set; }
        public Guid ExpenseAccountId { get; set; }
        public string AccountNo { get; set; }
        public string PrimaryPhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string RegionOrState { get; set; }
        public string City { get; set; }
        public string Website { get; set; }
        public string Comments { get; set; }
        public string InsuranceProvider { get; set; }
        public string PolicyNo { get; set; }
        public DateTime PolicyExpirationDate { get; set; }
        public int TenantId { get; set; }

    }
}
