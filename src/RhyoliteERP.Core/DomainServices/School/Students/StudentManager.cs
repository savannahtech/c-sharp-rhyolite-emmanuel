using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.DomainServices.School.AlumniHistories;
using RhyoliteERP.DomainServices.School.Students.Dto;
using RhyoliteERP.DomainServices.School.Students.Events;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Students
{
   public class StudentManager: Abp.Domain.Services.DomainService, IStudentManager
    {

        private readonly IRepository<Student, Guid> _repositoryStudent;
        private readonly IRepository<PromotionHistory, Guid> _repositoryPromotionHistory;
        private readonly IRepository<SchClass, Guid> _repositorySchClass;
        private readonly IRepository<Religion, Guid> _repositoryReligion;
        private readonly IRepository<Country, Guid> _repositoryCountry;
        private readonly IRepository<AcademicYear, Guid> _repositoryAcademicYear;
        private readonly IRepository<StudentStatus, Guid> _repositoryStudentStatus;
        private readonly IRepository<SchoolProfile, Guid> _repositorySchoolProfile;
        private readonly IAlumniHistoryManager _alumniHistoryManager;
        public IEventBus EventBus { get; set; }

        public StudentManager(IRepository<Student, Guid> repositoryStudent, IRepository<StudentStatus, Guid> repositoryStudentStatus, IRepository<SchClass, Guid> repositorySchClass, IRepository<Religion, Guid> repositoryReligion, IRepository<Country, Guid> repositoryCountry, IRepository<PromotionHistory, Guid> repositoryPromotionHistory, IAlumniHistoryManager alumniHistoryManager, IRepository<AcademicYear, Guid> repositoryAcademicYear, IRepository<SchoolProfile, Guid> repositorySchoolProfile)
        {
            _repositoryStudent = repositoryStudent;
            _repositoryStudentStatus = repositoryStudentStatus;
            EventBus = NullEventBus.Instance;
            _repositorySchClass = repositorySchClass;
            _repositoryReligion = repositoryReligion;
            _repositoryCountry = repositoryCountry;
            _repositoryPromotionHistory = repositoryPromotionHistory;
            _alumniHistoryManager = alumniHistoryManager;
            _repositoryAcademicYear = repositoryAcademicYear;
            _repositorySchoolProfile = repositorySchoolProfile;
        }

        public async Task<object> Create(Student entity)
        {
            //obtain default employee status...
            var defaultStatus = await _repositoryStudentStatus.FirstOrDefaultAsync(x => x.IsDefault);
            var classInfo = await _repositorySchClass.FirstOrDefaultAsync(entity.ClassId);
            var religionInfo = await _repositoryReligion.FirstOrDefaultAsync(entity.ReligionId);
            var countryInfo = await _repositoryCountry.FirstOrDefaultAsync(entity.NationalityId);
            var academicYearInfo = await _repositoryAcademicYear.FirstOrDefaultAsync(entity.AcademicYearId);

            var datta = await _repositoryStudent.FirstOrDefaultAsync(x => x.StudentIdentifier == entity.StudentIdentifier);
            if (datta == null)
            {
                entity.StudentStatusId = entity.StudentStatusId == Guid.Empty ? defaultStatus.Id : entity.StudentStatusId;
                entity.StudentStatusName = defaultStatus == null ? "" : defaultStatus.Name;
               
                if (classInfo != null && !string.IsNullOrEmpty(classInfo.StreamName))
                {
                    entity.ClassName = $"{classInfo.ClassName}-{classInfo.StreamName}";
                }
                else if (classInfo != null && string.IsNullOrEmpty(classInfo.StreamName))
                {
                    entity.ClassName = classInfo.ClassName;
                }

                if (religionInfo != null)
                {
                    entity.ReligionName = religionInfo.Name;
                }

                if (countryInfo != null)
                {
                    entity.NationalityName = countryInfo.Nationality;

                }

                if (academicYearInfo != null)
                {
                    entity.AcademicYearName = academicYearInfo.Name;

                }

                entity.FirstName = entity.FirstName.Trim();

                entity.LastName = entity.LastName.Trim();


                if (!string.IsNullOrEmpty(entity.MiddleName))
                {
                    entity.MiddleName = entity.MiddleName.Trim();

                }


                return await _repositoryStudent.InsertAsync(entity);
            }
            else
            {
                datta.StudentStatusId = entity.StudentStatusId == Guid.Empty ? defaultStatus.Id : entity.StudentStatusId;
                datta.StudentStatusName = defaultStatus == null ? "" : defaultStatus.Name;

            }

            var studentCount = await _repositoryStudent.CountAsync();

            // produce to pricing model ...
            EventBus.Trigger(new PricingModelData { ParameterName = "student-count", ParameterCount = studentCount + 1, AccountSource = "erp", ModuleName = "School Manager", TenantId = entity.TenantId });

            return await _repositoryStudent.UpdateAsync(datta);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryStudent.FirstOrDefaultAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryStudent.GetAllListAsync();
        }

        public async Task Promote(StudentPromotion studentPromotion)
        {
            var schoolProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();

            foreach (var student in studentPromotion.Students)
            {
                await _repositoryStudent.UpdateAsync(student);

                var promotedFromClassInfo = await _repositorySchClass.FirstOrDefaultAsync(x => x.Id == studentPromotion.PromotedFrom);
                var promotedToClassInfo = await _repositorySchClass.FirstOrDefaultAsync(x => x.Id == studentPromotion.PromotedTo);

                var history = new PromotionHistory
                {
                    DatePromoted = studentPromotion.DatePromoted,
                    PromotedFrom = studentPromotion.PromotedFrom,
                    PromotedTo = studentPromotion.PromotedTo,
                    StudentId = student.Id,
                    AcademicYearId = schoolProfile.CurrentAcademicYearId,
                    AcademicYearName = schoolProfile.CurrentAcademicYearName,
                    StudentIdentifier = student.StudentIdentifier,
                    StudentName = string.IsNullOrEmpty(student.MiddleName) ? $"{student.FirstName} {student.LastName}" : $"{student.FirstName} {student.MiddleName} {student.LastName}"

                };


                if (promotedFromClassInfo != null && !string.IsNullOrEmpty(promotedFromClassInfo.StreamName))
                {
                    history.PromotedFromName = $"{promotedFromClassInfo.ClassName}-{promotedFromClassInfo.StreamName}";
                }
                else if (promotedFromClassInfo != null && string.IsNullOrEmpty(promotedFromClassInfo.StreamName))
                {
                    history.PromotedFromName = promotedFromClassInfo.ClassName;
                }


                if (promotedToClassInfo != null && !string.IsNullOrEmpty(promotedToClassInfo.StreamName))
                {
                    history.PromotedToName = $"{promotedToClassInfo.ClassName}-{promotedToClassInfo.StreamName}";
                }
                else if (promotedToClassInfo != null && string.IsNullOrEmpty(promotedToClassInfo.StreamName))
                {
                    history.PromotedToName = promotedToClassInfo.ClassName;
                }

                await _repositoryPromotionHistory.InsertAsync(history);
            }
        }


        public async Task PromoteAlumni(AlumniStudentPromotion studentPromotion)
        {
            await _alumniHistoryManager.CreateBatch(studentPromotion.Students);

            foreach (var student in studentPromotion.Students)
            {
                await _repositoryStudent.DeleteAsync(student.Id);
            }
        }

        public async Task<IEnumerable<object>> ListStudentsForPromotion(Guid classId)
        {
            return await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);
        }

        public async Task<IEnumerable<object>> ListAllByClass(Guid classId)
        {
           return await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);
        }

        public async Task<IEnumerable<object>> OpeningBalanceExcelExport(Guid classId)
        {
            var datta = await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);

            var query = from u1 in datta
                        select new
                        {
                            StudentID = u1.StudentIdentifier,
                            Amount = 0,
                            u1.Gender,
                            StudentName = u1.MiddleName == null ? $"{u1.FirstName} {u1.LastName}" : $"{u1.FirstName} {u1.MiddleName} {u1.LastName}",
                        };

            return query.OrderBy(a => a.StudentName).ToList();
        }

       
        public async Task<IEnumerable<object>> AttitudeExcelExport(Guid classId)
        {
            var datta = await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);
            var query = from u1 in datta
                        select new
                        {
                            StudentID = u1.StudentIdentifier,
                            Attitude = "",
                            StudentName = u1.MiddleName == null ? $"{u1.FirstName} {u1.LastName}" : $"{u1.FirstName} {u1.MiddleName} {u1.LastName}",
                        };
            return query.OrderBy(a => a.StudentName).ToList();

        }

        public async Task<IEnumerable<object>> ConductsExcelExport(Guid classId)
        {
            var datta = await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);
            var query = from u1 in datta
                        select new
                        {
                            StudentID = u1.StudentIdentifier,
                            Conduct = "",
                            StudentName = u1.MiddleName == null ? $"{u1.FirstName} {u1.LastName}" : $"{u1.FirstName} {u1.MiddleName} {u1.LastName}",
                        };
            return query.OrderBy(a => a.StudentName).ToList();

        }


        public async Task<IEnumerable<object>> TeacherRemarksExcelExport(Guid classId)
        {
            var datta = await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);
            var query = from u1 in datta
                        select new
                        {
                            StudentID = u1.StudentIdentifier,
                            Remarks = "",
                            StudentName = u1.MiddleName == null ? $"{u1.FirstName} {u1.LastName}" : $"{u1.FirstName} {u1.MiddleName} {u1.LastName}",
                        };
            return query.OrderBy(a => a.StudentName).ToList();

        }

        public async Task<IEnumerable<object>> ResultsUploadExcelExport(Guid classId)
        {
            var datta = await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);
            var query = from u1 in datta
                        select new
                        {
                            StudentID = u1.StudentIdentifier,
                            Marks = 0,
                            StudentName = u1.MiddleName == null ? $"{u1.FirstName} {u1.LastName}" : $"{u1.FirstName} {u1.MiddleName} {u1.LastName}",
                        };
            return query.OrderBy(a => a.StudentName).ToList();

        }

        public async Task<IEnumerable<object>> ListStudsByClass(Guid classId)
        {
            var datta = await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);
            var query = from u1 in datta
                        select new
                        {
                            u1.Id,
                            StudentID = u1.StudentIdentifier,
                            u1.LastName,
                            u1.FirstName,
                            MiddleName = u1.MiddleName == null ? "" : u1.MiddleName,
                            StudentName = u1.MiddleName == null ? $"{u1.FirstName} {u1.LastName}" : $"{u1.FirstName} {u1.MiddleName} {u1.LastName}",

                        };
            return query.OrderBy(a => a.StudentName).ToList();

        }

        public async Task Delete(Guid id)
        {
            await _repositoryStudent.DeleteAsync(id);
        }


        public async Task<object> Update(Student entity)
        {
            var classInfo = await _repositorySchClass.FirstOrDefaultAsync(entity.ClassId);
            var religionInfo = await _repositoryReligion.FirstOrDefaultAsync(entity.ReligionId);
            var countryInfo = await _repositoryCountry.FirstOrDefaultAsync(entity.NationalityId);
            var defaultStatus = await _repositoryStudentStatus.FirstOrDefaultAsync(x => x.IsDefault);

            entity.StudentStatusId = entity.StudentStatusId == Guid.Empty ? defaultStatus.Id : entity.StudentStatusId;
            entity.StudentStatusName = defaultStatus == null ? "Active" : defaultStatus.Name;

            if (classInfo != null && !string.IsNullOrEmpty(classInfo.StreamName))
            {
                entity.ClassName = $"{classInfo.ClassName}-{classInfo.StreamName}";
            }
            else if (classInfo != null && string.IsNullOrEmpty(classInfo.StreamName))
            {
                entity.ClassName = classInfo.ClassName;
            }

            if (religionInfo != null)
            {
                entity.ReligionName = religionInfo.Name;
            }

            if (countryInfo != null)
            {
                entity.NationalityName = countryInfo.Nationality;

            }

            entity.FirstName = entity.FirstName.Trim();

            entity.LastName = entity.LastName.Trim();


            if (!string.IsNullOrEmpty(entity.MiddleName))
            {
                entity.MiddleName = entity.MiddleName.Trim();

            }

            return await _repositoryStudent.UpdateAsync(entity);
        }

        public async Task<Guid> GetStudentId(string studentIdentifier)
        {
            var obj = await _repositoryStudent.FirstOrDefaultAsync(a => !string.IsNullOrEmpty(studentIdentifier) && a.StudentIdentifier == studentIdentifier.Trim());
            if (obj != null)
            {
                return obj.Id;

            }
            else
            {
                return Guid.Empty;
            }
        }

        //
        public async Task<IEnumerable<object>> EnqStudsByClass(Guid classId)
        {
            return await _repositoryStudent.GetAllListAsync(a => a.ClassId == classId);
        }

        public async Task<IEnumerable<object>> EnqStudsByNationality(Guid nationalityId)
        {
            return await _repositoryStudent.GetAllListAsync(a => a.NationalityId == nationalityId);
        }


        public async Task<IEnumerable<object>> EnqStudsByReligion(Guid religionId)
        {
            return await _repositoryStudent.GetAllListAsync(a => a.ReligionId == religionId);
        }

    }

}
