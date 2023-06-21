using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.BillSetups.Dto
{
   public class CreateBillSetupInput
    {
        public Guid AcademicYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid TermId { get; set; }
        public Guid BillTypeId { get; set; }
        public decimal TotalBillAmount { get; set; }
        public List<BillSetupDetail> Details { get; set; }
    }
}
