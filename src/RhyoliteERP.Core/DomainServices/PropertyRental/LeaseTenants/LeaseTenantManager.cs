using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.LeaseTenants
{
    public class LeaseTenantManager: Abp.Domain.Services.DomainService, ILeaseTenantManager
    {
        private readonly IRepository<LeaseTenant, Guid> _repositoryLeaseTenant;

        public LeaseTenantManager(IRepository<LeaseTenant, Guid> repositoryLeaseTenant)
        {
            _repositoryLeaseTenant = repositoryLeaseTenant;
        }

        public async Task Create(LeaseTenant entity)
        {
            await _repositoryLeaseTenant.InsertAsync(entity);

        }

        public async Task Update(LeaseTenant entity)
        {
            await _repositoryLeaseTenant.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryLeaseTenant.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryLeaseTenant.GetAllListAsync();
        }

        public async Task<object> GetUnitTenants(Guid propertyId, Guid propertyUnitId)
        {
            if (propertyUnitId != Guid.Empty)
            {
                return await _repositoryLeaseTenant.GetAllListAsync(x=> x.LeasedPropertyUnitId == propertyUnitId);

            }
            else
            {
                return await _repositoryLeaseTenant.GetAllListAsync(x => x.LeasedPropertyId == propertyId);

            }

        }
        public async Task Delete(Guid id)
        {
            await _repositoryLeaseTenant.DeleteAsync(id);
        }

    }
}
