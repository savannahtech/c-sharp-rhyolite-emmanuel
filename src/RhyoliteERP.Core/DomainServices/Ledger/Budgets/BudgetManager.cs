using Abp.Domain.Repositories;
using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.Budgets
{
   public class BudgetManager : Abp.Domain.Services.DomainService, IBudgetManager
    {
        private readonly IRepository<Budget, Guid> _repositoryBudget;

        public BudgetManager(IRepository<Budget, Guid> repositoryBudget)
        {
            _repositoryBudget = repositoryBudget;
        }

        public async Task<object> Create(Budget entity)
        {
            var datta = await _repositoryBudget.FirstOrDefaultAsync(x => x.AccountId == entity.AccountId && x.OuId == entity.OuId && x.BeginDate.Date == entity.BeginDate && x.EndDate.Date == entity.EndDate.Date);

            if (datta == null)
            {
                await _repositoryBudget.InsertAsync(entity);

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

        public async Task Update(Budget entity)
        {
            await _repositoryBudget.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryBudget.FirstOrDefaultAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryBudget.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryBudget.DeleteAsync(id);

        }
    }
}
