using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class ResultProportion : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public Guid LevelId { get; set; }
        public string LevelName { get; set; }
        public decimal ExamPercentage { get; set; }
        public decimal ClassPercentage { get; set; }
        public int TenantId { get; set; }
    }
}
