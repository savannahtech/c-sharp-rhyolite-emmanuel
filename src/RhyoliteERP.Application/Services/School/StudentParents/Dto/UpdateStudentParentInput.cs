using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentParents.Dto
{
   public class UpdateStudentParentInput:EntityDto<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid ParentId { get; set; }
        public int TenantId { get; set; }
    }
}
