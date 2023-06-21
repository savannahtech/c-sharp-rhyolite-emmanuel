﻿using RhyoliteERP.DomainServices.Payroll.BikTypes.Dto;
using RhyoliteERP.Services.Payroll.BikTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.BikTypes
{
   public interface IBikTypeAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListAll(Guid bikTypeId, Guid employeeId, Guid categoryId);
        Task<object> Create(CreateBikTypeInput input);
        Task<object> CreateBikRate(BikRateInput input);
        Task UpdateBikRate(BikRateInput input);
        Task Update(UpdateBikTypeInput input);
        Task DeleteBikRate(BikRateInput input);

        Task Delete(Guid Id);
    }
}
