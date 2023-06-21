using Abp.Application.Services.Dto;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.AcademicYears.Dto
{
   public class UpdateAcademicYearInput:EntityDto<Guid>
    {
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TenantId { get; set; }
        public int PrecedenceNo { get; set; }
        public List<Term> Terms { get; set; }
    }
}
