using Abp.Domain.Repositories;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Suppliers
{
   public class SupplierManager: Abp.Domain.Services.DomainService,  ISupplierManager
    {

        private readonly IRepository<Supplier, Guid> _repositorySupplier;

        public SupplierManager(IRepository<Supplier, Guid> repositorySupplier)
        {
            _repositorySupplier = repositorySupplier;
        }

        public async Task<object> Create(Supplier entity)
        {
            var datta = await _repositorySupplier.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositorySupplier.InsertAsync(entity);

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

        public async Task Update(Supplier entity)
        {
            await _repositorySupplier.UpdateAsync(entity);
        }

        public async Task<object> ListAll()
        {
            return await _repositorySupplier.GetAllListAsync();
        }

        public async Task<object> ListAllByGroup(Guid groupId)
        {
            return await _repositorySupplier.GetAllListAsync(x => x.SupplierGroupId == groupId);
        }


        public async Task Delete(Guid id)
        {
            await _repositorySupplier.DeleteAsync(id);

        }
    }
}
