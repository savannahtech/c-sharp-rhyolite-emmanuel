using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.LoanApplications
{
   public class LoanApplicationManager: Abp.Domain.Services.DomainService, ILoanApplicationManager
    {
        private readonly IRepository<LoanApplication, Guid> _repositoryLoanApplication;

        public LoanApplicationManager(IRepository<LoanApplication, Guid> repositoryLoanApplication)
        {
            _repositoryLoanApplication = repositoryLoanApplication;
        }

        public async Task Create(LoanApplication entity)
        {
            await _repositoryLoanApplication.InsertAsync(entity);
        }

        public async Task Update(LoanApplication entity)
        {
            await _repositoryLoanApplication.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryLoanApplication.FirstOrDefaultAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryLoanApplication.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryLoanApplication.DeleteAsync(id);

        }
    }
}
