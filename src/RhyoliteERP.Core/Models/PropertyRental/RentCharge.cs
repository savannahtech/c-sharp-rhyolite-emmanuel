using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.PropertyRental
{
    public class RentCharge
    {
        public Guid Id { get; set; }
        public DateTime NextDueDate { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public Guid RentAccountId { get; set; } // as revenue account...

    }
}
