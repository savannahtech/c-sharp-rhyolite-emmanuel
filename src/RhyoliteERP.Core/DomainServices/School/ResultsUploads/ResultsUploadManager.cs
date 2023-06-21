﻿using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.ResultsUploads
{
   public class ResultsUploadManager: Abp.Domain.Services.DomainService, IResultsUploadManager
    {
        private readonly IRepository<ResultsUpload, Guid> _repositoryResultsUpload;
        private readonly IRepository<Subject, Guid> _repositorySubject;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ResultsUploadManager(IBasicAcademicInfoManager basicAcademicInfoManager, IRepository<ResultsUpload, Guid> repositoryResultsUpload, IRepository<Subject, Guid> repositorySubject, IUnitOfWorkManager unitOfWorkManager)
        {
            _basicAcademicInfoManager = basicAcademicInfoManager;
            _repositoryResultsUpload = repositoryResultsUpload;
            _repositorySubject = repositorySubject;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task Create(ResultsUpload entity)
        {
            if (entity.StudentId != Guid.Empty)
            {
                var subjectInfo = await _repositorySubject.FirstOrDefaultAsync(x => x.Id == entity.SubjectId);

                var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);
                entity.AcademicYearName = basicInfo.AcademicYearName;
                entity.TermName = basicInfo.TermName;
                entity.ClassName = basicInfo.ClassName;
                entity.StudentName = basicInfo.StudentName;
                entity.StudentIdentifier = basicInfo.StudentIdentifier;
                entity.SubjectName = subjectInfo.Name;
                await _repositoryResultsUpload.InsertAsync(entity);
            }
           

        }

        public async Task Update(ResultsUpload entity)
        {
            var subjectInfo = await _repositorySubject.FirstOrDefaultAsync(x => x.Id == entity.SubjectId);

            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId);
            entity.AcademicYearName = basicInfo.AcademicYearName;
            entity.TermName = basicInfo.TermName;
            entity.ClassName = basicInfo.ClassName;
            entity.StudentName = basicInfo.StudentName;
            entity.StudentIdentifier = basicInfo.StudentIdentifier;
            entity.SubjectName = subjectInfo.Name;

            await _repositoryResultsUpload.UpdateAsync(entity);

        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId)
        {
            return await _repositoryResultsUpload.GetAllListAsync(x=>x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.SubjectId == subjectId && x.ResultTypeId == resultTypeId);
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, int tenantId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                return await _repositoryResultsUpload.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId);
            }
        }

        public async Task Delete(Guid id)
        {
            await _repositoryResultsUpload.DeleteAsync(id);
        }

    }
}
