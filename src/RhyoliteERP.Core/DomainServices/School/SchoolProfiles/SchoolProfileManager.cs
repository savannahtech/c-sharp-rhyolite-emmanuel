using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.SchoolProfiles
{
   public class SchoolProfileManager: Abp.Domain.Services.DomainService, ISchoolProfileManager
    {
        private readonly IRepository<SchoolProfile, Guid> _repositorySchoolProfile;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;
        public SchoolProfileManager(IRepository<SchoolProfile, Guid> repositorySchoolProfile, IBasicAcademicInfoManager basicAcademicInfoManager)
        {
            _repositorySchoolProfile = repositorySchoolProfile;
            _basicAcademicInfoManager = basicAcademicInfoManager;
        }

        public async Task Create(SchoolProfile entity)
        {
            var academicYearInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.CurrentAcademicYearId, entity.CurrentTermId);
            var datta = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();

            if (datta != null)
            {
                datta.SchoolName = entity.SchoolName;
                datta.CurrentAcademicYearId = entity.CurrentAcademicYearId;
                datta.CurrentAcademicYearName = academicYearInfo.AcademicYearName;
                datta.CurrentTermId = entity.CurrentTermId;
                datta.CurrentTermName = academicYearInfo.TermName;
                datta.SecondaryEmailAddress = entity.SecondaryEmailAddress;
                datta.City = entity.City;
                datta.PrimaryPhoneNo = entity.PrimaryPhoneNo;
                datta.Website = entity.Website;
                datta.PostalAddress = entity.PostalAddress;
                datta.District = entity.District;
                datta.SecondaryPhoneNo = entity.SecondaryPhoneNo;
                datta.Accountant = entity.Accountant;
                datta.MailHostName = entity.MailHostName;
                datta.IsSSLEnabled = entity.IsSSLEnabled;
                datta.PortNumber = entity.PortNumber;
                datta.PrimaryEmailAddress = entity.PrimaryEmailAddress;
                datta.EmailPassword = entity.EmailPassword;
                datta.SchoolHead = entity.SchoolHead;
                datta.SchoolLogoUrl = !string.IsNullOrEmpty(entity.SchoolLogoUrl) ? entity.SchoolLogoUrl : "";
                datta.RegionOrState = entity.RegionOrState;
                datta.AssistantSchoolHead = entity.AssistantSchoolHead;
                datta.AutoEmailReceiptNotification = entity.AutoEmailReceiptNotification;
                datta.AutoSMSReceiptNotification = entity.AutoSMSReceiptNotification;

                await _repositorySchoolProfile.UpdateAsync(datta);

            }
            else
            {
                entity.CurrentAcademicYearName = academicYearInfo.AcademicYearName;
                entity.CurrentTermName = academicYearInfo.TermName;

                await _repositorySchoolProfile.InsertAsync(entity);
            }

        }

        public async Task<SchoolProfile> GetAsync()
        {
            var data = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();
            return data;
        }

        public async Task CreateReportTemplate(string fileUrl, string reportType)
        {
            var profile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();
            if (profile != null) {

                switch (reportType)
                {
                    case "bill":
                        profile.BillTemplateFileUrl = fileUrl;
                        break;
                    case "terminal-report":
                        profile.TerminalReportTemplateFileUrl = fileUrl;
                        break;
                    default:
                        break;
                }

                await _repositorySchoolProfile.UpdateAsync(profile);
            }
        }

         
    }
}
