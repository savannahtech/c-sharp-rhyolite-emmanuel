using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.ResidentAccounts.Dto
{
    public class CreateResidentAccountInput
    {
        public Guid LeaseTenantId { get; set; }
        public string LeaseTenantIdentifier { get; set; }
        public string AccountCaption { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
    }
}
