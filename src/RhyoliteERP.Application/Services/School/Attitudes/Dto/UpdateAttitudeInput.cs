﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Attitudes.Dto
{
   public class UpdateAttitudeInput:EntityDto<Guid>
    {
        public Guid AcademicYearId { get; set; }
        public Guid TermId { get; set; }
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public string AttitudeText { get; set; }
        public int TenantId { get; set; }
    }
}
