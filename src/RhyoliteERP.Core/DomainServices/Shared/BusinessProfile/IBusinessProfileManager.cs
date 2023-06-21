using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.BusinessProfile
{
    public interface IBusinessProfileManager: Abp.Domain.Services.IDomainService
    {
        Task<object> GetProfile();
        Task Create(CompanyProfile input);
        Task Update(CompanyProfile input);
    }
}
