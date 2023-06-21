using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ledger
{
   public class Journal : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string BatchNumber { get; set; }
        public DateTime BatchDate { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
        public bool IsPosted { get; set; }
        [Column(TypeName = "jsonb")] public List<JournalDetail> JournalDetails { get; set; }
        public int TenantId { get; set; }
    }
}
