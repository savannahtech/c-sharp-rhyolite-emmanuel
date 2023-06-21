﻿using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeRanks
{
   public interface IEmployeeRankManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(EmployeeRank input);
        Task Update(EmployeeRank input);
        Task Delete(Guid Id);
    }
}
