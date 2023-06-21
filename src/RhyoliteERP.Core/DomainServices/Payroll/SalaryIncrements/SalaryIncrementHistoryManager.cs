﻿using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.SalaryIncrements
{
   public class SalaryIncrementHistoryManager: Abp.Domain.Services.DomainService, ISalaryIncrementHistoryManager
    {

        private readonly IRepository<Models.Payroll.SalaryIncrementHistory, Guid> _repositorySalaryIncrementHistory;

        public SalaryIncrementHistoryManager(IRepository<Models.Payroll.SalaryIncrementHistory, Guid> repositorySalaryIncrementHistory)
        {
            _repositorySalaryIncrementHistory = repositorySalaryIncrementHistory;
        }

        public async Task Create(Models.Payroll.SalaryIncrementHistory entity)
        {
            await _repositorySalaryIncrementHistory.InsertAsync(entity);
        }

        public async Task<object> ListAll()
        {
            return await _repositorySalaryIncrementHistory.GetAllListAsync();
        }

    }
}
