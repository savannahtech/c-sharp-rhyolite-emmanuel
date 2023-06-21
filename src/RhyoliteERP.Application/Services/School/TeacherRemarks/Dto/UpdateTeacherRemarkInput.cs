using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.TeacherRemarks.Dto
{
   public class UpdateTeacherRemarkInput:EntityDto<Guid>
    {
        public Guid AcademicYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid TermId { get; set; }
        public Guid StudentId { get; set; }
        public string Remarks { get; set; }
        public int TenantId { get; set; }
    }
}
