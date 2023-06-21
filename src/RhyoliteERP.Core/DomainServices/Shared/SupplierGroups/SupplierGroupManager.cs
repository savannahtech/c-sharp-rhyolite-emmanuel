using Abp.Domain.Repositories;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.SupplierGroups
{
    public class SupplierGroupManager : Abp.Domain.Services.DomainService, ISupplierGroupManager
    {

        private readonly IRepository<SupplierGroup, Guid> _repositorySupplierGroup;

        public SupplierGroupManager(IRepository<SupplierGroup, Guid> repositorySupplierGroup)
        {
            _repositorySupplierGroup = repositorySupplierGroup;
        }

        public async Task<object> Create(SupplierGroup entity)
        {
            var datta = await _repositorySupplierGroup.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositorySupplierGroup.InsertAsync(entity);

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

        public async Task Update(SupplierGroup entity)
        {
            await _repositorySupplierGroup.UpdateAsync(entity);
        }

        public async Task<object> ListAll()
        {
            return await _repositorySupplierGroup.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositorySupplierGroup.DeleteAsync(id);

        }
    }
}
