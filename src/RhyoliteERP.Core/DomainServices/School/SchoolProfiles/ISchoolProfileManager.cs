using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.SchoolProfiles
{
   public interface ISchoolProfileManager: Abp.Domain.Services.IDomainService
    {
        Task<SchoolProfile> GetAsync();
        Task Create(SchoolProfile entity);
        Task CreateReportTemplate(string fileUrl, string reportType);

    }
}
