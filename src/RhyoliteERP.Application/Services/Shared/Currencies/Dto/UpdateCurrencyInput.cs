﻿using Abp.Application.Services.Dto;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Currencies.Dto
{
   public class UpdateCurrencyInput:EntityDto<Guid>
    {
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string MinorName { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public List<CurrencyRate> Rates { get; set; }
        public int TenantId { get; set; }
    }
}
