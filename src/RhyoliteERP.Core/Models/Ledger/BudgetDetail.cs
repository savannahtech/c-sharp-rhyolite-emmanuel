using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ledger
{
   public class BudgetDetail
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal RevisedAmount { get; set; }
        public int TenantId { get; set; }
    }
}
