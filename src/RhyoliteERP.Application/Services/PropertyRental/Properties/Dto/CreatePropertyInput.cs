using Castle.MicroKernel.SubSystems.Conversion;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Properties.Dto
{
    public class CreatePropertyInput
    {
        public string Name { get; set; }
        public string PropertyIdentifier { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid PropertyGroupId { get; set; }
        public string Address { get; set; }
        public Guid CountryId { get; set; }
        public string City { get; set; }
        public string RegionOrState { get; set; }
        public Guid LedgerAccountId { get; set; }
        public decimal PropertyReserve { get; set; }
        public string PropertyManager { get; set; }
        public string PropertyManagerPhoneNo { get; set; }
        public List<RentalOwner> RentalOwners { get; set; }
        public List<MeterReading> MeterReadings { get; set; }

    }
}
