using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.ResultsUploads.Dto
{
   public class UpdateResultsUploadInput: EntityDto<Guid>
    {
        public Guid AcademicYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TermId { get; set; }
        public Guid ResultTypeId { get; set; }
        public decimal TotalMarks { get; set; }
        public Guid StudentId { get; set; }
        public decimal MarksObtained { get; set; }
        public int TenantId { get; set; }
    }
}
