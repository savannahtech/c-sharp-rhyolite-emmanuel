using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class ExpenseAllocation
    {
        public Guid Id { get; set; }
        public string UnitNo { get; set; }
        public Guid PropertyUnitId { get; set; }
        public decimal AllocationPercentage { get; set; }
    }
}
