﻿using Abp.Application.Services.Dto;
using Castle.MicroKernel.SubSystems.Conversion;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.SalaryGrades.Dto
{
   public class UpdateSalaryGradeInput:EntityDto<Guid>
    {
        public string Name { get; set; }
        public List<SalaryNotch> SalaryNotches { get; set; }
        public int TenantId { get; set; }
    }
}
