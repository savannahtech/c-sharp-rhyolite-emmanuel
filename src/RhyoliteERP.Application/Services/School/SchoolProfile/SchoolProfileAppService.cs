using AutoMapper;
using RhyoliteERP.DomainServices.School.SchoolProfiles;
using RhyoliteERP.Services.School.SchoolProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SchoolProfile
{
   public class SchoolProfileAppService: RhyoliteERPAppServiceBase, ISchoolProfileAppService
    {
        private readonly ISchoolProfileManager _schoolProfileManager;
        private readonly IMapper _mapper;

        public SchoolProfileAppService(ISchoolProfileManager schoolProfileManager, IMapper mapper)
        {
            _schoolProfileManager = schoolProfileManager;
            _mapper = mapper;
        }

        public async Task<Models.School.SchoolProfile> GetAsync()
        {
            return await _schoolProfileManager.GetAsync();
        }


        public async Task Create(CreateSchoolProfileInput input)
        {
            var obj = _mapper.Map<Models.School.SchoolProfile>(input);
            await _schoolProfileManager.Create(obj);
        }

        public async Task CreateReportTemplate(string fileUrl, string reportType)
        {
            await _schoolProfileManager.CreateReportTemplate(fileUrl, reportType);
        }
    }
}
