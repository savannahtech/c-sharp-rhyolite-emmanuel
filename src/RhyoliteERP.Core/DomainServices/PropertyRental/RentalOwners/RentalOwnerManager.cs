using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.RentalOwners
{
    public class RentalOwnerManager: Abp.Domain.Services.DomainService, IRentalOwnerManager
    {
        private readonly IRepository<RentalOwner, Guid> _repositoryRentalOwner;

        public RentalOwnerManager(IRepository<RentalOwner, Guid> repositoryRentalOwner)
        {
            _repositoryRentalOwner = repositoryRentalOwner;
        }

        public async Task Create(RentalOwner entity)
        {
            await _repositoryRentalOwner.InsertAsync(entity);
        }

        public async Task Update(RentalOwner entity)
        {
            await _repositoryRentalOwner.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryRentalOwner.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryRentalOwner.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryRentalOwner.DeleteAsync(id);
        }
    }
}
