using Castle.MicroKernel.SubSystems.Conversion;
using RhyoliteERP.Models.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class Property : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string Name { get; set; }
        public string PropertyIdentifier { get; set; }
        public Guid PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; }
        public Guid PropertyGroupId { get; set; }
        public string PropertyGroupName { get; set; }
        public string Address { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string City { get; set; }
        public string Size { get; set; }
        public string RegionOrState { get; set; }
        public Guid LedgerAccountId { get; set; }
        public string LedgerAccountName { get; set; }
        public decimal PropertyReserve { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime YearBuilt { get; set; }
        public string PropertyManager { get; set; }
        public string PropertyManagerPhoneNo { get; set; }
        public string PropertyManagerEmail { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string GpsLongitude { get; set; }
        public string GpsLatitude { get; set; }
        public bool IsRented { get; set; }
        public bool IsSold { get; set; }
        [Column(TypeName = "jsonb")] public List<RentalOwner> RentalOwners { get; set; }
        [Column(TypeName = "jsonb")] public List<MeterReading> MeterReadings { get; set; }
        [Column(TypeName = "jsonb")] public List<string> ImageList { get; set; }
        [Column(TypeName = "jsonb")] public List<UnitAmenity> UnitAmenities { get; set; }
        [Column(TypeName = "jsonb")] public List<UnitAmenity> InteriorDetails { get; set; }
        [Column(TypeName = "jsonb")] public List<UnitAmenity> OutdoorDetails { get; set; }
        [Column(TypeName = "jsonb")] public List<UnitAmenity> Utilities { get; set; }
        [Column(TypeName = "jsonb")] public List<string> TwoDimensionPlans { get; set; }
        [Column(TypeName = "jsonb")] public List<string> ThreeDimensionPlans { get; set; }
        [Column(TypeName = "jsonb")] public List<string> Elevations { get; set; }
        public int TenantId { get; set; }
    }
}
