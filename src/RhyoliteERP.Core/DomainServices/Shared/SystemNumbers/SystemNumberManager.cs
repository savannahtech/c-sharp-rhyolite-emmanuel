using Abp.Domain.Repositories;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.SystemNumbers
{
   public class SystemNumberManager: Abp.Domain.Services.DomainService, ISystemNumberManager    
    {
        private readonly IRepository<SystemNumber, Guid> _repositorySystemNumber;

        public SystemNumberManager(IRepository<SystemNumber, Guid> repositorySystemNumber)
        {
            _repositorySystemNumber = repositorySystemNumber;
        }

        public async Task<object> Create(SystemNumber entity)
        {
            var datta = await _repositorySystemNumber.FirstOrDefaultAsync(x => x.ModuleName == entity.ModuleName && x.ItemName == entity.ItemName);

            if (datta == null)
            {
                await _repositorySystemNumber.InsertAsync(entity);

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

        public async Task Update(SystemNumber entity)
        {
            await _repositorySystemNumber.UpdateAsync(entity);
        }

        public async Task<SystemNumber> GetByItemName(string itemName)
        {
            return await _repositorySystemNumber.FirstOrDefaultAsync(x=> x.ItemName == itemName.Trim());
        }

        public async Task<object> ListAll()
        {
            return await _repositorySystemNumber.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositorySystemNumber.DeleteAsync(id);

        }
    }
}
