﻿using Castle.MicroKernel.SubSystems.Conversion;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.DeductionTypes.Dto
{
   public class CreateDeductionTypeInput
    {
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        [Column(TypeName = "jsonb")] public List<DeductionRate> Rates { get; set; }
    }
}
