using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class StudentStatement : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentIdentifier { get; set; }

        [Column(TypeName = "jsonb")] public List<Statement> Statement { get; set; }
        public int TenantId { get; set; }
    }
}
