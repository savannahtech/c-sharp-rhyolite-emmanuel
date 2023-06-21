using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class Sibling : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid SiblingClassId { get; set; }
        public string SiblingClassName { get; set; }
        public Guid SiblingStudentId { get; set; }
        public string SiblingStudentIdentifier { get; set; }
        public string SiblingStudentName { get; set; }
        //=>
        public Guid StudentId { get; set; }
        public string StudentIdentifier { get; set; }
        public string StudentName { get; set; }
        public int TenantId { get; set; }
    }
}
