﻿using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.LoanTypes
{
   public interface ILoanTypeManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(LoanType input);
        Task Update(LoanType input);
        Task Delete(Guid Id);
    }
}
