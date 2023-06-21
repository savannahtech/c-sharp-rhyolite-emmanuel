using Castle.MicroKernel.SubSystems.Conversion;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyUnits.Dto
{
    public class CreatePropertyUnitInput
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string UnitNo { get; set; }
        public decimal MarketRent { get; set; }
        public string Size { get; set; }
        public string Address { get; set; }
        public int Rooms { get; set; }
        public int Baths { get; set; }
        public string Amenities { get; set; }
        public bool AutoCreateUnits { get; set; }
        public int NoOfUnitsPerProperty { get; set; }
        public List<UnitAmenity> UnitAmenities { get; set; }

    }
}
