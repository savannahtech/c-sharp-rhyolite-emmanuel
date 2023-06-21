using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.PromotionHistories
{
    public class PromotionHistoryManager : Abp.Domain.Services.DomainService, IPromotionHistoryManager
    {
        private readonly IRepository<PromotionHistory, Guid> _repositoryPromotionHistory;
        private readonly IRepository<SchoolProfile, Guid> _repositorySchoolProfile;
        private readonly IRepository<SchClass, Guid> _repositorySchClass;
        private readonly IRepository<Student, Guid> _repositoryStudent;

        public PromotionHistoryManager(IRepository<PromotionHistory, Guid> repositoryPromotionHistory, IRepository<SchoolProfile, Guid> repositorySchoolProfile, IRepository<SchClass, Guid> repositorySchClass, IRepository<Student, Guid> repositoryStudent)
        {
            _repositoryPromotionHistory = repositoryPromotionHistory;
            _repositorySchoolProfile = repositorySchoolProfile;
            _repositorySchClass = repositorySchClass;
            _repositoryStudent = repositoryStudent;
        }

        public async Task Create(PromotionHistory entity)
        {
            var schprofile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();
            if (schprofile != null)
            {
                var promotedFrom = await _repositorySchClass.FirstOrDefaultAsync(x=>x.Id == entity.PromotedFrom);
                var promotedTo = await _repositorySchClass.FirstOrDefaultAsync(x=>x.Id == entity.PromotedTo);
                var student = await _repositoryStudent.FirstOrDefaultAsync(x=>x.Id == entity.StudentId);

                entity.PromotedFromName = string.IsNullOrEmpty(promotedFrom.StreamName)? promotedFrom.ClassName : $"{promotedFrom.ClassName}-{promotedFrom.ClassName}";
                entity.PromotedToName = string.IsNullOrEmpty(promotedTo.StreamName)? promotedTo.ClassName : $"{promotedTo.ClassName}-{promotedTo.ClassName}";
                entity.AcademicYearName = schprofile.CurrentAcademicYearName;
                entity.StudentName = string.IsNullOrEmpty(student.MiddleName)? $"{student.FirstName} {student.LastName}" : $"{student.FirstName} {student.MiddleName} {student.LastName}";
                entity.StudentIdentifier = student.StudentIdentifier ;
                entity.AcademicYearId = schprofile.CurrentAcademicYearId;
                await _repositoryPromotionHistory.InsertAsync(entity);

            }


        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId)
        {
            return await _repositoryPromotionHistory.GetAllListAsync(x => x.AcademicYearId == academicYearId);
        }

    }
}
