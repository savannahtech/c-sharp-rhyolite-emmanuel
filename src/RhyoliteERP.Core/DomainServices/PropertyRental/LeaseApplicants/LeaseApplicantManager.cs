using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.LeaseApplicants
{
    public class LeaseApplicantManager: Abp.Domain.Services.DomainService, ILeaseApplicantManager
    {
        private readonly IRepository<LeaseApplicant, Guid> _repositoryLeaseApplicant;

        public LeaseApplicantManager(IRepository<LeaseApplicant, Guid> repositoryLeaseApplicant)
        {
            _repositoryLeaseApplicant = repositoryLeaseApplicant;
        }


        public async Task<object> ListAll()
        {
            return await _repositoryLeaseApplicant.GetAllListAsync();
        }


        public async Task Create(LeaseApplicant entity)
        {
            await _repositoryLeaseApplicant.InsertAsync(entity);
        }


        public async Task Delete(Guid Id)
        {
            await _repositoryLeaseApplicant.DeleteAsync(Id);
        }
    }
}
