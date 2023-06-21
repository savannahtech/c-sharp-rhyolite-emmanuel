using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.TeacherRemarks
{
   public class TeacherRemarkManager: Abp.Domain.Services.DomainService, ITeacherRemarkManager
    {
        private readonly IRepository<TeacherRemark, Guid> _repositoryTeacherRemark;
        private readonly IRepository<Student, Guid> _repositoryStudent;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;

        public TeacherRemarkManager(IRepository<TeacherRemark, Guid> repositoryTeacherRemark, IRepository<Student, Guid> repositoryStudent, IBasicAcademicInfoManager basicAcademicInfoManager)
        {
            _repositoryTeacherRemark = repositoryTeacherRemark;
            _repositoryStudent = repositoryStudent;
            _basicAcademicInfoManager = basicAcademicInfoManager;
        }

        public async Task Create(TeacherRemark entity)
        {
            var datta = await _repositoryTeacherRemark.FirstOrDefaultAsync(x => x.AcademicYearId == entity.AcademicYearId && x.TermId == entity.TermId && x.StudentId == entity.StudentId);

            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);

            if (datta != null)
            {
                datta.Remarks = entity.Remarks;
                entity.AcademicYearName = basicInfo.AcademicYearName;
                entity.TermName = basicInfo.TermName;
                entity.ClassName = basicInfo.ClassName;
                entity.StudentName = basicInfo.StudentName;
                entity.StudentIdentifier = basicInfo.StudentIdentifier;
                await _repositoryTeacherRemark.UpdateAsync(datta);
            }
            else
            {
                entity.AcademicYearName = basicInfo.AcademicYearName;
                entity.TermName = basicInfo.TermName;
                entity.ClassName = basicInfo.ClassName;
                entity.StudentName = basicInfo.StudentName;
                entity.StudentIdentifier = basicInfo.StudentIdentifier;
                await _repositoryTeacherRemark.InsertAsync(entity);
            }
        }
       
        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryTeacherRemark.GetAllListAsync();
        }


        public async Task Delete(Guid id)
        {
            await _repositoryTeacherRemark.DeleteAsync(id);

        }

        public async Task Update(TeacherRemark entity)
        {

            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);

            entity.AcademicYearName = basicInfo.AcademicYearName;
            entity.TermName = basicInfo.TermName;
            entity.ClassName = basicInfo.ClassName;
            entity.StudentName = basicInfo.StudentName;
            entity.StudentIdentifier = basicInfo.StudentIdentifier;
            await _repositoryTeacherRemark.UpdateAsync(entity);
        }

    }
}
