﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Ledger.CoaHierachies.Dto
{
   public class UpdateCoaHierachyInput:EntityDto<Guid>
    {
        public Guid ParentId { get; set; }
        public int TenantId { get; set; }
        public double Ordinal { get; set; }
        public string Name { get; set; }
    }
}
