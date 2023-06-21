using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class PropertyUnit : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string UnitNo { get; set; }
        public decimal MarketRent { get; set; }
        public string Size { get; set; }
        public string Address { get; set; }
        public int Rooms { get; set; }
        public int Baths { get; set; }
        public bool IsRented { get; set; }
        public bool IsSold { get; set; }
        public string Status { get; set; }
        public decimal SellingPrice { get; set; }
        public string Description { get; set; }
        public DateTime YearBuilt { get; set; }
        [Column(TypeName = "jsonb")] public List<string> ImageList { get; set; }
        [Column(TypeName = "jsonb")] public List<MeterReading> MeterReadings { get; set; }
        [Column(TypeName = "jsonb")] public List<UnitAmenity> UnitAmenities { get; set; }
        public int TenantId { get; set; }

    }
}
