using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ap
{
   public class PaymentApproval : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceDue { get; set; }
        public decimal AmountToPay { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }
    }
}
