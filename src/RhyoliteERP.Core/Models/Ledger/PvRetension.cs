using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ledger
{
   public class PvRetension : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string Description { get; set; }
        public Guid VoucherHeaderId { get; set; }
        public Guid CreditGLId { get; set; }
        public string CreditGLName { get; set; }
        public bool IsFixedAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public int TenantId { get; set; }
    }
}
