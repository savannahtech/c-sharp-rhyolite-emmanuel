using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Students.Events
{
   public class PricingModelData: EventData
    {
        public string ParameterName { get; set; }
        public int ParameterCount { get; set; }
        public string ModuleName { get; set; }
        public int TenantId { get; set; }
        public string AccountSource { get; set; }
    }
}
