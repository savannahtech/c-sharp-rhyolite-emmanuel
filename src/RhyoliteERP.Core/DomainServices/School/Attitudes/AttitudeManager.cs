using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Attitudes
{
   public class AttitudeManager: Abp.Domain.Services.DomainService, IAttitudeManager
    {

        private readonly IRepository<Attitude, Guid> _repositoryAttitude;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;

        public AttitudeManager(IRepository<Attitude, Guid> repositoryAttitude, IBasicAcademicInfoManager basicAcademicInfoManager)
        {
            _repositoryAttitude = repositoryAttitude;
            _basicAcademicInfoManager = basicAcademicInfoManager;
        }


        public async Task Create(Attitude entity)
        {
            var datta = await _repositoryAttitude.FirstOrDefaultAsync(x => x.AcademicYearId == entity.AcademicYearId && x.TermId == entity.TermId && x.ClassId == entity.ClassId);
            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);

            if (datta == null)
            {

                entity.AcademicYearName = basicInfo.AcademicYearName;
                entity.TermName = basicInfo.TermName;
                entity.ClassName = basicInfo.ClassName;
                entity.StudentName = basicInfo.StudentName;
                entity.StudentIdentifier = basicInfo.StudentIdentifier;
                await _repositoryAttitude.InsertAsync(entity);
                
            }
            else
            {

                datta.AcademicYearName = basicInfo.AcademicYearName;
                datta.TermName = basicInfo.TermName;
                datta.ClassName = basicInfo.ClassName;
                datta.StudentName = basicInfo.StudentName;
                datta.StudentIdentifier = basicInfo.StudentIdentifier;
                await _repositoryAttitude.UpdateAsync(datta);

            }
        }

        public async Task Update(Attitude entity)
        {
            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);

            entity.AcademicYearName = basicInfo.AcademicYearName;
            entity.TermName = basicInfo.TermName;
            entity.ClassName = basicInfo.ClassName;
            entity.StudentName = basicInfo.StudentName;
            entity.StudentIdentifier = basicInfo.StudentIdentifier;

            await _repositoryAttitude.UpdateAsync(entity);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryAttitude.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryAttitude.DeleteAsync(id);

        }
    }
}
