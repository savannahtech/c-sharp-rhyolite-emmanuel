using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class BillSetup : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public Guid BillTypeId { get; set; }
        public string BillTypeName { get; set; }
        public decimal TotalBillAmount { get; set; }
        [Column(TypeName = "jsonb")] public List<BillSetupDetail> Details { get; set; }
        public int TenantId { get; set; }

    }
}
