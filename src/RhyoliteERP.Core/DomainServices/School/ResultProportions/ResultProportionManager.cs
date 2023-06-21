using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.ResultProportions
{
   public class ResultProportionManager : Abp.Domain.Services.DomainService, IResultProportionManager
    {
        private readonly IRepository<ResultProportion, Guid> _repositoryResultProportion;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;
        public ResultProportionManager(IRepository<ResultProportion, Guid> repositoryResultProportion, IBasicAcademicInfoManager basicAcademicInfoManager)
        {
            _repositoryResultProportion = repositoryResultProportion;
            _basicAcademicInfoManager = basicAcademicInfoManager;
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid levelId)
        {
            return await _repositoryResultProportion.GetAllListAsync(x=>x.AcademicYearId == academicYearId && x.TermId == termId && x.LevelId == levelId);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryResultProportion.GetAllListAsync();
        }
        public async Task Create(ResultProportion entity)
        {
            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId);
            entity.AcademicYearName = basicInfo.AcademicYearName;
            entity.TermName = basicInfo.TermName;
            await _repositoryResultProportion.InsertAsync(entity);
        }

        public async Task Update(ResultProportion entity)
        {
            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId);
            entity.AcademicYearName = basicInfo.AcademicYearName;
            entity.TermName = basicInfo.TermName;
            await _repositoryResultProportion.UpdateAsync(entity);

        }

        public async Task Delete(Guid id)
        {
            await _repositoryResultProportion.DeleteAsync(id);
        }
    }
}
