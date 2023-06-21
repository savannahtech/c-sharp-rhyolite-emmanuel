using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.LoanTypes.Dto
{
   public class CreateLoanTypeInput
    {
        public string Name { get; set; }
        public bool ChargeInterest { get; set; }
    }
}
