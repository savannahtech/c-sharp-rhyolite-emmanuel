using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyExpenseAllocations.Dto
{
    public class UpdatePropertyExpenseAllocationInput
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ExpenseAllocation> Details { get; set; }
        public int TenantId { get; set; }
    }
}
