using Abp.Domain.Services;
using RhyoliteERP.DomainServices.Shared.BasicInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.BasicInfo
{
   public interface IBasicAcademicInfoManager:IDomainService
    {
       Task<BasicAcademicInfo> GetBasicAcademicInfo(Guid academicYearId, Guid termId);
       Task<BasicAcademicInfo> GetStudentBasicInfo(Guid studentId, Guid classId);
       Task<BasicAcademicInfo> GetBasicAcademicInfo(Guid academicYearId, Guid termId, Guid classId);
       Task<BasicAcademicInfo> GetBasicAcademicInfo(Guid academicYearId, Guid termId, Guid classId, Guid studentId);
    }
}
