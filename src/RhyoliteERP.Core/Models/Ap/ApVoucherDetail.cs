using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ap
{
   public class ApVoucherDetail
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsWithHolding { get; set; }
    }
}
