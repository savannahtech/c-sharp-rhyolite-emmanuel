using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.BusinessProfile
{
    public class BusinessProfileManager: Abp.Domain.Services.DomainService, IBusinessProfileManager
    {
        private readonly IRepository<CompanyProfile, Guid> _repositoryCompanyProfile;

        public BusinessProfileManager(IRepository<CompanyProfile, Guid> repositoryCompanyProfile)
        {
            _repositoryCompanyProfile = repositoryCompanyProfile;
        }

        public async Task Create(CompanyProfile input)
        {
            await _repositoryCompanyProfile.InsertAsync(input);
        }

        public async Task<object> GetProfile()
        {
            return await _repositoryCompanyProfile.GetAll().FirstOrDefaultAsync();
        }

        public async Task Update(CompanyProfile input)
        {
            await _repositoryCompanyProfile.UpdateAsync(input);
        }

    }
}
