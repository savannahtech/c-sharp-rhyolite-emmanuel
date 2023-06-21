using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
   public class BillDetail
    {
        public Guid Id { get; set; }
        public Guid FeeId { get; set; }
        public string FeeName { get; set; }
        public Guid BillTypeId { get; set; }
        public string BillTypeName { get; set; }
        public decimal FeeAmount { get; set; }
    }
}
