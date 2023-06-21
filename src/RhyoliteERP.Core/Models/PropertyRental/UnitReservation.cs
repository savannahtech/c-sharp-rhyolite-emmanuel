using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class UnitReservation : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string AccompaniedGuests { get; set; }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string PurposeOfTravel { get; set; }
        public bool InterestedInCarRent { get; set; }
        public string SpecialRequest { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string RegionOrState { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public Guid PropertyUnitId { get; set; }
        public string PropertyUnitNo { get; set; }
        public int TenantId { get; set; }

    }
}
