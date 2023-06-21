using Abp.Domain.Repositories;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.Reports
{
    public class RentalReportManager: Abp.Domain.Services.DomainService, IRentalReportManager
    {

        private readonly IRepository<CompanyProfile, Guid> _repositoryCompanyProfile;

        public RentalReportManager(IRepository<CompanyProfile, Guid> repositoryCompanyProfile)
        {
            _repositoryCompanyProfile = repositoryCompanyProfile;
        }

        public async Task<object> GetBusinessProfile(string title)
        {
            var datta = await _repositoryCompanyProfile.GetAllListAsync();
            return datta.Select(a => new { a.CompanyName, PrimaryPhoneNo = $"Tel: {a.PhoneNo}", a.Address, Email = $"Email: {a.PrimaryEmailAddress}", ReportTitle = title }).ToList();
        }
    }
}
