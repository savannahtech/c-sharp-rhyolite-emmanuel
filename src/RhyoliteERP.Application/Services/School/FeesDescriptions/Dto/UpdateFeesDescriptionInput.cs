using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.FeesDescriptions.Dto
{
   public class UpdateFeesDescriptionInput:EntityDto<Guid>
    {
        public string Description { get; set; }
        public Guid BillTypeId { get; set; }
        public int TenantId { get; set; }
    }
}
