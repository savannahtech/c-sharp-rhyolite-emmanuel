using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Countires.Dto
{
    public class UpdateCountryInput : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int NumericIsoCode { get; set; }
        public string Nationality { get; set; }
        public int TenantId { get; set; }
    }
}
