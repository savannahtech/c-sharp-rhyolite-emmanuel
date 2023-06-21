using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class MessageTemplate : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string MessageContent { get; set; }
        public string MessageSubject { get; set; }
        public string Alias { get; set; }
        public int TenantId { get; set; }
    }
}
