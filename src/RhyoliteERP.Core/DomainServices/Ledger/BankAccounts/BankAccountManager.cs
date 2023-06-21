using Abp.Domain.Repositories;
using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.BankAccounts
{
   public class BankAccountManager : Abp.Domain.Services.DomainService, IBankAccountManager
    {
        private readonly IRepository<BankAccount, Guid> _repositoryBankAccount;

        public BankAccountManager(IRepository<BankAccount, Guid> repositoryBankAccount)
        {
            _repositoryBankAccount = repositoryBankAccount;
        }

        public async Task<object> Create(BankAccount entity)
        {
            var datta = await _repositoryBankAccount.FirstOrDefaultAsync(x => x.BankName == entity.BankName);

            if (datta == null)
            {
                await _repositoryBankAccount.InsertAsync(entity);

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

        public async Task Update(BankAccount entity)
        {
            await _repositoryBankAccount.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryBankAccount.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryBankAccount.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryBankAccount.DeleteAsync(id);

        }
    }
}
