using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class MeterReading
    {
        public Guid Id { get; set; }
        public string UnitNo { get; set; }
        public Guid PropertyUnitId { get; set; }
        public decimal PreviousValue { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime ReadingDate { get; set; }
        public Guid MeterTypeId { get; set; }
        public string MeterTypeName { get; set; }
        public decimal Usage { get; set; }

    }
}
