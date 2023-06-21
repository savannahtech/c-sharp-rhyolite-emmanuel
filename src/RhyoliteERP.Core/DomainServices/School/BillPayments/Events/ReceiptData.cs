using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.BillPayments.Events
{
   public class ReceiptData : EventData
    {
        public Dictionary<string,object> Receipt { get; set; }
    }
}
