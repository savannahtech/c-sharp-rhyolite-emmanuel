using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeSalaryAdvances.Dto
{
   public class CreateEmployeeSalaryAdvanceInput
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string EmployeeName { get; set; }
        public DateTime LoanDate { get; set; }
        public Guid LoanTypeId { get; set; }
        public string LoanTypeName { get; set; }
        public decimal Amount { get; set; }
        public bool IsApproved { get; set; }
    }
}
