using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SchClasses.Dto
{
   public class UpdateClassInput:EntityDto<Guid>
    {
        public Guid LevelId { get; set; }
        public string ClassName { get; set; }
        public Guid StreamId { get; set; }
        public int TenantId { get; set; }
    }
}
