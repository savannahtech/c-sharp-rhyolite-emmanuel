﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Shared
{
   public class CostCenter : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid ParentId { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
    }
}
