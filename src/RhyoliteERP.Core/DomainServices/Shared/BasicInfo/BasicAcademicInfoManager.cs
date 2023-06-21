using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo.Models;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.BasicInfo
{
   public class BasicAcademicInfoManager: Abp.Domain.Services.DomainService, IBasicAcademicInfoManager
    {

        private readonly IRepository<AcademicYear, Guid> _repositoryAcademicYear;
        private readonly IRepository<SchClass, Guid> _repositorySchClass;
        private readonly IRepository<Student, Guid> _repositoryStudent;
        public BasicAcademicInfoManager(IRepository<AcademicYear, Guid> repositoryAcademicYear, IRepository<SchClass, Guid> repositorySchClass, IRepository<Student, Guid> repositoryStudent)
        {
            _repositoryAcademicYear = repositoryAcademicYear;
            _repositorySchClass = repositorySchClass;
            _repositoryStudent = repositoryStudent;
        }

        public async Task<BasicAcademicInfo> GetBasicAcademicInfo(Guid academicYearId,Guid termId,Guid classId)
        {
            var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(x => x.Id == academicYearId);

            var classInfo = await _repositorySchClass.FirstOrDefaultAsync(x => x.Id == classId);
            string className = string.Empty;

            if (classInfo != null && !string.IsNullOrEmpty(classInfo.StreamName))
            {
                className = $"{classInfo.ClassName}-{classInfo.StreamName}";
            }
            else if(classInfo != null && string.IsNullOrEmpty(classInfo.StreamName))
            {
                className = classInfo.ClassName;
            }

            return new BasicAcademicInfo
            {
                AcademicYearName = academicYearInfo == null ? "" : academicYearInfo.Name,
                ClassName = className,
                TermName = academicYearInfo.Terms.Any() ? academicYearInfo.Terms.FirstOrDefault(a => a.Id == termId).Name : "",
            };
        }

        public async Task<BasicAcademicInfo> GetBasicAcademicInfo(Guid academicYearId, Guid termId)
        {
            var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(x => x.Id == academicYearId);
             

            return new BasicAcademicInfo
            {
                AcademicYearName = academicYearInfo == null ? "" : academicYearInfo.Name,
                TermName = academicYearInfo != null && academicYearInfo.Terms.Any() ? academicYearInfo.Terms.FirstOrDefault(a => a.Id == termId).Name : "",
            };
        }

        public async Task<BasicAcademicInfo> GetBasicAcademicInfo(Guid academicYearId, Guid termId, Guid classId,Guid studentId)
        {
            var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(x => x.Id == academicYearId);
            
            var classInfo = await _repositorySchClass.FirstOrDefaultAsync(x => x.Id == classId);

            var studentInfo = await _repositoryStudent.FirstOrDefaultAsync(x => x.Id == studentId);


            string className = string.Empty;

            if (classInfo != null && !string.IsNullOrEmpty(classInfo.StreamName))
            {
                className = $"{classInfo.ClassName}-{classInfo.StreamName}";
            }
            else if (classInfo != null && string.IsNullOrEmpty(classInfo.StreamName))
            {
                className = classInfo.ClassName;
            }

            return new BasicAcademicInfo
            {
                AcademicYearName = academicYearInfo == null ? "": academicYearInfo.Name,
                ClassName = className,
                TermName = academicYearInfo.Terms.Any()? academicYearInfo.Terms.FirstOrDefault(a=>a.Id == termId).Name : "",
                StudentIdentifier = studentInfo.StudentIdentifier,
                StudentName = string.IsNullOrEmpty(studentInfo.MiddleName) ? $"{studentInfo.FirstName} {studentInfo.LastName}" : $"{studentInfo.FirstName} {studentInfo.MiddleName} {studentInfo.LastName}"

            };

        }

        public async Task<BasicAcademicInfo> GetStudentBasicInfo(Guid studentId, Guid classId)
        {

            var classInfo = await _repositorySchClass.FirstOrDefaultAsync(x => x.Id == classId);

            var studentInfo = await _repositoryStudent.FirstOrDefaultAsync(x => x.Id == studentId);


            string className = string.Empty;

            if (classInfo != null && !string.IsNullOrEmpty(classInfo.StreamName))
            {
                className = $"{classInfo.ClassName}-{classInfo.StreamName}";
            }
            else if (classInfo != null && string.IsNullOrEmpty(classInfo.StreamName))
            {
                className = classInfo.ClassName;
            }

            return new BasicAcademicInfo
            {
                ClassName = className,
                StudentIdentifier = studentInfo.StudentIdentifier,
                StudentName = string.IsNullOrEmpty(studentInfo.MiddleName) ? $"{studentInfo.FirstName} {studentInfo.LastName}" : $"{studentInfo.FirstName} {studentInfo.MiddleName} {studentInfo.LastName}"
            };
        }
    }
}
