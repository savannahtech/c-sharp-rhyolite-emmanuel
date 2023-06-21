using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.MonthlySsnitDeductionHistory
{
    public class MonthlySsnitDeductionHistManager: Abp.Domain.Services.DomainService, IMonthlySsnitDeductionHistManager
    {
        private readonly IRepository<MonthlySsnitDeductionHist, Guid> _repositoryMonthlySsnitDeductionHist;

        public MonthlySsnitDeductionHistManager(IRepository<MonthlySsnitDeductionHist, Guid> repositoryMonthlySsnitDeductionHist)
        {
            _repositoryMonthlySsnitDeductionHist = repositoryMonthlySsnitDeductionHist;
        }

         
        public async Task<object> ListAll(int month, int year)
        {
            return await _repositoryMonthlySsnitDeductionHist.GetAllListAsync(x=>x.Month == month && x.Year == year && x.EmployeeId != Guid.Empty);
        }
    }
}
