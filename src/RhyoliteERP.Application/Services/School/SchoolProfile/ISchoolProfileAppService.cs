using Abp.Application.Services;
using RhyoliteERP.Services.School.SchoolProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SchoolProfile
{
   public interface ISchoolProfileAppService: IApplicationService
    {
        Task<Models.School.SchoolProfile> GetAsync();
        Task Create(CreateSchoolProfileInput entity);
        Task CreateReportTemplate(string fileUrl, string reportType);

    }
}
