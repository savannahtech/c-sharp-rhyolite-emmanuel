using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Siblings
{
   public class SiblingManager: Abp.Domain.Services.DomainService, ISiblingManager
    {
        private readonly IRepository<Sibling,Guid> _repositorySibling;
        private readonly IRepository<Student,Guid> _repositoryStudent;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;
        public SiblingManager(IRepository<Sibling, Guid> repositorySibling, IBasicAcademicInfoManager basicAcademicInfoManager, IRepository<Student, Guid> repositoryStudent)
        {
            _repositorySibling = repositorySibling;
            _basicAcademicInfoManager = basicAcademicInfoManager;
            _repositoryStudent = repositoryStudent;
        }



        public async Task Create(Sibling entity)
        {
            var datta = await _repositorySibling.FirstOrDefaultAsync(x => x.SiblingStudentId == entity.SiblingStudentId);
            if (datta != null)
            {
                var siblingBasicInfo = await _basicAcademicInfoManager.GetStudentBasicInfo(entity.SiblingStudentId, entity.SiblingClassId);
                var studentBasicInfo = await _repositoryStudent.FirstOrDefaultAsync(x => x.Id == entity.StudentId);

                datta.SiblingClassId = entity.SiblingClassId;
                datta.SiblingClassName = siblingBasicInfo.ClassName;
                datta.SiblingStudentName = siblingBasicInfo.StudentName;
                datta.SiblingStudentIdentifier = siblingBasicInfo.StudentIdentifier;
                datta.StudentId = entity.StudentId;

                entity.StudentIdentifier = studentBasicInfo.StudentIdentifier;
                entity.StudentName = string.IsNullOrEmpty(studentBasicInfo.MiddleName) ? $"{studentBasicInfo.FirstName} {studentBasicInfo.LastName}" : $"{studentBasicInfo.FirstName} {studentBasicInfo.MiddleName} {studentBasicInfo.LastName}";

                await _repositorySibling.UpdateAsync(datta);
            }
            else
            {
                var siblingBasicInfo = await _basicAcademicInfoManager.GetStudentBasicInfo(entity.SiblingStudentId, entity.SiblingClassId);
                var studentBasicInfo = await _repositoryStudent.FirstOrDefaultAsync(x=>x.Id == entity.StudentId);

                entity.SiblingClassName = siblingBasicInfo.ClassName;
                entity.SiblingStudentName = siblingBasicInfo.StudentName;
                entity.SiblingStudentIdentifier = siblingBasicInfo.StudentIdentifier;

                entity.StudentIdentifier = studentBasicInfo.StudentIdentifier;
                entity.StudentName = string.IsNullOrEmpty(studentBasicInfo.MiddleName) ? $"{studentBasicInfo.FirstName} {studentBasicInfo.LastName}" : $"{studentBasicInfo.FirstName} {studentBasicInfo.MiddleName} {studentBasicInfo.LastName}";

                await _repositorySibling.InsertAsync(entity);
            }
        }


        public async Task<IEnumerable<object>> ListAll(Guid studentId)
        {
           return await _repositorySibling.GetAllListAsync(x=>x.StudentId == studentId);
        }

        public async Task Delete(Guid id)
        {
            await _repositorySibling.DeleteAsync(id);
        }

        public async Task Update(Sibling entity)
        {
            var siblingBasicInfo = await _basicAcademicInfoManager.GetStudentBasicInfo(entity.SiblingStudentId, entity.SiblingClassId);
            var studentBasicInfo = await _repositoryStudent.FirstOrDefaultAsync(x => x.Id == entity.StudentId);

            entity.SiblingClassId = entity.SiblingClassId;
            entity.SiblingClassName = siblingBasicInfo.ClassName;
            entity.SiblingStudentName = siblingBasicInfo.StudentName;
            entity.SiblingStudentIdentifier = siblingBasicInfo.StudentIdentifier;
            entity.StudentId = entity.StudentId;

            entity.StudentIdentifier = studentBasicInfo.StudentIdentifier;
            entity.StudentName = string.IsNullOrEmpty(studentBasicInfo.MiddleName) ? $"{studentBasicInfo.FirstName} {studentBasicInfo.LastName}" : $"{studentBasicInfo.FirstName} {studentBasicInfo.MiddleName} {studentBasicInfo.LastName}";

            await _repositorySibling.UpdateAsync(entity);
        }
    }
}
