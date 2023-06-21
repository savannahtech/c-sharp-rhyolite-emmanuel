﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.OvertimeTimeSheets.Dto
{
   public class UpdateOvertimeTimeSheetInput:EntityDto<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string JobDescription { get; set; }
        public Guid OvertimeTypeId { get; set; }
        public string OvertimeTypeName { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal OvertimeMinutes { get; set; }
        public bool Taxable { get; set; }
        public int TenantId { get; set; }
    }
}
