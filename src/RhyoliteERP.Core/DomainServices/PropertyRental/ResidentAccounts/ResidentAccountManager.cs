using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.ResidentAccounts
{
    public class ResidentAccountManager : Abp.Domain.Services.DomainService , IResidentAccountManager
    {
        private readonly IRepository<ResidentAccount, Guid> _repositoryResidentAccount;

        public ResidentAccountManager(IRepository<ResidentAccount, Guid> repositoryResidentAccount)
        {
            _repositoryResidentAccount = repositoryResidentAccount;
        }


        public async Task<object> ListAll()
        {
            return await _repositoryResidentAccount.GetAllListAsync();
        }


        public async Task Create(ResidentAccount entity)
        {
            await _repositoryResidentAccount.InsertAsync(entity);
        }
    }
}
