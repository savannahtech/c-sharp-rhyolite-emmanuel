﻿using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.BonusAndOnetimeAllowances.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.BonusAndOnetimeAllowances
{
    public interface IBonusAndOnetimeAllowanceAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListAll(int month, int year);
        Task Create(CreateBonusAndOnetimeAllowanceInput input);
        Task Update(UpdateBonusAndOnetimeAllowanceInput input);
        Task Delete(Guid Id);
    }
}
