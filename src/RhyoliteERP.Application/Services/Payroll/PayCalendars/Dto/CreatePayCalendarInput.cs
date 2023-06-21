using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.PayCalendars.Dto
{
   public class CreatePayCalendarInput
    {
        public int Year { get; set; }
        public List<PayCalendarDetail> PayCalendarDetails { get; set; }
    }
}
