using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentStatuses.Dto
{
   public class UpdateStudentStatusInput:EntityDto<Guid>
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public int TenantId { get; set; }
    }
}
