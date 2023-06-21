﻿using RhyoliteERP.Services.Payroll.LoanTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.LoanTypes
{
   public interface ILoanTypeAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateLoanTypeInput input);
        Task Update(UpdateLoanTypeInput input);
        Task Delete(Guid Id);
    }
}
