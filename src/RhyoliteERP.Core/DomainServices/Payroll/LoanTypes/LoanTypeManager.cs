using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.LoanTypes
{
   public class LoanTypeManager: Abp.Domain.Services.DomainService, ILoanTypeManager
    {
        private readonly IRepository<LoanType, Guid> _repositoryLoanType;

        public LoanTypeManager(IRepository<LoanType, Guid> repositoryLoanType)
        {
            _repositoryLoanType = repositoryLoanType;
        }

        public async Task<object> Create(LoanType entity)
        {
            var datta = await _repositoryLoanType.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryLoanType.InsertAsync(entity);

                return new
                {
                    code = 200,
                    message = "successful"
                };
            }
            else
            {
                return new
                {
                    code = 400,
                    message = "Duplicate records are not allowed."
                };
            }
        }

        public async Task Update(LoanType entity)
        {
            await _repositoryLoanType.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryLoanType.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryLoanType.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryLoanType.DeleteAsync(id);
        }
    }
}
