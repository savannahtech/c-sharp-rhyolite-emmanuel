﻿using Abp.Application.Services.Dto;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.AllowanceTypes.Dto
{
   public class UpdateAllowanceTypeInput:EntityDto<Guid>
    {
        public string Name { get; set; }
        public int AllowanceDays { get; set; }
        public Guid ExpenseAccountId { get; set; }
        public string ExpenseAccountName { get; set; }
        public bool Taxable { get; set; }
        public List<AllowanceRate> AllowanceRates { get; set; }
        public int TenantId { get; set; }
    }
}
