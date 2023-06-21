﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Payroll
{
   public class EmployeeBenefitInKind : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string EmployeeName { get; set; }
        public Guid BenefitInKindTypeId { get; set; }
        public string BenefitInKindTypeName { get; set; }
        public decimal Amount { get; set; }
        public int TenantId { get; set; }
    }
}
