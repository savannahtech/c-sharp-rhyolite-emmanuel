using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.SalaryGrades.Dto
{
   public class CreateSalaryGradeInput
    {
        public string Name { get; set; }
        public List<SalaryNotch> SalaryNotches { get; set; }
    }
}
