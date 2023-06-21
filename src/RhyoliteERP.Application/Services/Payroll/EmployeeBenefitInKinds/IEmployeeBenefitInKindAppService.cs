﻿using RhyoliteERP.Services.Payroll.EmployeeBenefitInKinds.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeBenefitInKinds
{
   public interface IEmployeeBenefitInKindAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListAll(Guid employeeId);
        Task<object> Create(CreateEmployeeBenefitInKindInput input);
        Task Update(UpdateEmployeeBenefitInKindInput input);
        Task Delete(Guid Id);
    }
}
