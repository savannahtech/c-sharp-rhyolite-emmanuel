using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyExpenseAllocations
{
    public class PropertyExpenseAllocationManager: Abp.Domain.Services.DomainService, IPropertyExpenseAllocationManager
    {
        private readonly IRepository<PropertyExpenseAllocation, Guid> _repositorPropertyExpenseAllocation;

        public PropertyExpenseAllocationManager(IRepository<PropertyExpenseAllocation, Guid> repositoryPropertyExpenseAllocation)
        {
            _repositorPropertyExpenseAllocation = repositoryPropertyExpenseAllocation;
        }

        public async Task Create(PropertyExpenseAllocation entity)
        {
            await _repositorPropertyExpenseAllocation.InsertAsync(entity);
        }

        public async Task Update(PropertyExpenseAllocation entity)
        {
            await _repositorPropertyExpenseAllocation.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositorPropertyExpenseAllocation.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositorPropertyExpenseAllocation.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositorPropertyExpenseAllocation.DeleteAsync(id);
        }

    }
}
