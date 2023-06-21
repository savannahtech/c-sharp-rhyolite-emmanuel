﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.TaxReliefs.Dto
{
   public class UpdateTaxReliefInput:EntityDto<Guid>
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int TenantId { get; set; }
    }
}
