using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentAttendances
{
   public class StudentAttendanceManager: Abp.Domain.Services.DomainService, IStudentAttendanceManager
    {
        private readonly IRepository<StudentAttendance, Guid> _repositoryStudentAttendance;
        private readonly IRepository<SchoolProfile, Guid> _repositorySchoolProfile;
        private readonly IRepository<Biometric, Guid> _repositoryBiometric;
        private readonly IRepository<Student, Guid> _repositoryStudent;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public StudentAttendanceManager(IRepository<StudentAttendance, Guid> repositoryStudentAttendance, IRepository<SchoolProfile, Guid> repositorySchoolProfile, IRepository<Biometric, Guid> repositoryBiometric, IUnitOfWorkManager unitOfWorkManager, IRepository<Student, Guid> repositoryStudent, IBasicAcademicInfoManager basicAcademicInfoManager)
        {
            _repositoryStudentAttendance = repositoryStudentAttendance;
            _repositorySchoolProfile = repositorySchoolProfile;
            _repositoryBiometric = repositoryBiometric;
            _unitOfWorkManager = unitOfWorkManager;
            _repositoryStudent = repositoryStudent;
            _basicAcademicInfoManager = basicAcademicInfoManager;
        }


        public async Task Create(StudentAttendance entity)
        {
            //obtain current Academic Year and Term
            var schoolProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();

            if (schoolProfile != null)
            {

                var datta = await _repositoryStudentAttendance.FirstOrDefaultAsync(x => x.AttendanceDate.Date == entity.AttendanceDate.Date);

                var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(schoolProfile.CurrentAcademicYearId, schoolProfile.CurrentTermId, entity.ClassId);

                if (datta != null)
                {
                    datta.AcademicYearId = schoolProfile == null ? Guid.Empty : schoolProfile.CurrentAcademicYearId;
                    datta.TermId = schoolProfile == null ? Guid.Empty : schoolProfile.CurrentTermId;
                    datta.ClassId = entity.ClassId;
                    datta.NoPresent = entity.NoPresent;
                    datta.AttendanceDate = entity.AttendanceDate;
                    datta.Details = entity.Details;

                    if (basicInfo != null)
                    {
                        datta.AcademicYearName = basicInfo.AcademicYearName;
                        datta.TermName = basicInfo.TermName;
                        datta.ClassName = basicInfo.ClassName;

                    }
                    await _repositoryStudentAttendance.UpdateAsync(datta);

                }
                else
                {
                    entity.AcademicYearId = schoolProfile == null ? Guid.Empty : schoolProfile.CurrentAcademicYearId;
                    entity.TermId = schoolProfile == null ? Guid.Empty : schoolProfile.CurrentTermId;

                    if (basicInfo != null)
                    {
                        entity.AcademicYearName = basicInfo.AcademicYearName;
                        entity.TermName = basicInfo.TermName;
                        entity.ClassName = basicInfo.ClassName;

                    }

                    await _repositoryStudentAttendance.InsertAsync(entity);
                }

            }
            

        }


        public async Task Delete(Guid Id)
        {
            await _repositoryStudentAttendance.DeleteAsync(Id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryStudentAttendance.GetAllListAsync();
        }

        public async Task MarkSingle(int tenantId,string biometricId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {

                var biometricData = await _repositoryBiometric.FirstOrDefaultAsync(x => x.BiometricId == biometricId);

                if (biometricData != null)
                {
                    var studentInfo = await _repositoryStudent.FirstOrDefaultAsync(x => x.StudentIdentifier == biometricData.StudentIdentifier);

                    var schoolProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();

                    var datta = await _repositoryStudentAttendance.FirstOrDefaultAsync(x => x.AttendanceDate.Date == DateTime.UtcNow.Date && x.ClassId == studentInfo.ClassId);

                    if (datta != null)
                    {
                        datta.AcademicYearId = schoolProfile == null ? Guid.Empty : schoolProfile.CurrentAcademicYearId;
                        datta.TermId = schoolProfile == null ? Guid.Empty : schoolProfile.CurrentTermId;
                        datta.ClassId = studentInfo.ClassId;
                        datta.NoPresent += 1;
                        datta.AttendanceDate = DateTime.UtcNow;
                        datta.Details.Add(new StudentAttendanceDetail { Id = Guid.NewGuid(), 
                            BiometricId = biometricId, 
                            StudentId = studentInfo.Id,
                            StudentIdentifier = studentInfo.StudentIdentifier,
                            StudentName =  string.IsNullOrEmpty(studentInfo.MiddleName) ? $"{studentInfo.FirstName} {studentInfo.LastName}" : $"{studentInfo.FirstName} {studentInfo.MiddleName} {studentInfo.LastName}"
                        });

                        await _repositoryStudentAttendance.UpdateAsync(datta);
                    }
                    else
                    {
                        var studentAttendance = new StudentAttendance
                        {
                            ClassId = studentInfo.ClassId,
                            ClassName = studentInfo.ClassName,
                            AttendanceDate = DateTime.UtcNow,
                            NoPresent = 1,
                            AcademicYearId = schoolProfile.CurrentAcademicYearId,
                            AcademicYearName = schoolProfile.CurrentAcademicYearName,
                            TermId = schoolProfile.CurrentTermId,
                            TermName = schoolProfile.CurrentTermName,
                            Details = new List<StudentAttendanceDetail>
                            {
                                new StudentAttendanceDetail
                                {
                                    Id = Guid.NewGuid(),
                                    BiometricId = biometricId,
                                    StudentId = studentInfo.Id,
                                    StudentIdentifier = studentInfo.StudentIdentifier,
                                    StudentName =  string.IsNullOrEmpty(studentInfo.MiddleName) ? $"{studentInfo.FirstName} {studentInfo.LastName}" : $"{studentInfo.FirstName} {studentInfo.MiddleName} {studentInfo.LastName}"

                                }
                            },
                            TenantId = tenantId

                        };

                        var result = await _repositoryStudentAttendance.InsertAsync(studentAttendance);

                    }

                   
                }


            }


        }
        //enquiry
        public async Task<IEnumerable<object>> ListAll(Guid classId, DateTime startDate, DateTime endDate)
        {
           return await _repositoryStudentAttendance.GetAllListAsync(x => x.ClassId == classId && x.AttendanceDate >= startDate && x.AttendanceDate <= endDate);
        }

    }
}
