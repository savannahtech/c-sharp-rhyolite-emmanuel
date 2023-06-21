using RhyoliteERP.DomainServices.School.Students;
using RhyoliteERP.DomainServices.School.Students.Dto;
using RhyoliteERP.Services.School.Students.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Students
{
   public interface IStudentAppService: Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> GetAsync(Guid id);
        Task<IEnumerable<object>> ListAllByClass(Guid classId);
        Task<IEnumerable<object>> ListStudsByClass(Guid classId);
        Task<object> Create(CreateStudentInput input);
        Task<object> Update(UpdateStudentInput input);
        Task Delete(Guid id);
        Task<Guid> GetStudentId(string studentId);
        Task<IEnumerable<object>> ListStudentsForPromotion(Guid classId);
        Task Promote(StudentPromotion studentPromotion);
        Task PromoteAlumni(AlumniStudentPromotion studentPromotion);

        //Enquiry
        Task<IEnumerable<object>> EnqStudentsByClass(Guid classId);
        Task<IEnumerable<object>> EnqStudentsByNationality(Guid nationalityId);
        Task<IEnumerable<object>> EnqStudentsByReligion(Guid religionId);

        //exports
        Task<IEnumerable<object>> AttitudeExcelExport(Guid classId);
        Task<IEnumerable<object>> ConductsExcelExport(Guid classId);
        Task<IEnumerable<object>> TeacherRemarksExcelExport(Guid classId);
        Task<IEnumerable<object>> ResultsUploadExcelExport(Guid classId);
        Task<IEnumerable<object>> OpeningBalanceExcelExport(Guid classId);
    }
}
