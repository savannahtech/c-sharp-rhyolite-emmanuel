using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyUnits.Dto
{
    public class UpdatePropertyUnitInput:EntityDto<Guid>
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
        public int TenantId { get; set; }
    }
}
