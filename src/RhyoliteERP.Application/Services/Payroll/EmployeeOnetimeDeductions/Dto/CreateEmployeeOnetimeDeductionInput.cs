﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeOnetimeDeductions.Dto
{
   public class CreateEmployeeOnetimeDeductionInput
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string EmployeeName { get; set; }
        public Guid DeductionTypeId { get; set; }
        public string DeductionTypeName { get; set; }
        public bool IsFixedAmount { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
