﻿using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyTypes.Dto
{
    public class UpdatePropertyTypeInput:EntityDto<Guid>
    {
        public string Name { get; set; }
        public int TenantId { get; set; }
    }
}
