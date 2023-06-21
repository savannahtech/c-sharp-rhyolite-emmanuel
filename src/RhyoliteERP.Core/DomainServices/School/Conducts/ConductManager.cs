using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Conducts
{
   public class ConductManager: Abp.Domain.Services.DomainService, IConductManager
    {
        private readonly IRepository<Conduct, Guid> _repositoryConduct;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;

        public ConductManager(IRepository<Conduct, Guid> repositoryConduct, IBasicAcademicInfoManager basicAcademicInfoManager)
        {
            _repositoryConduct = repositoryConduct;
            _basicAcademicInfoManager = basicAcademicInfoManager;
        }


        public async Task Create(Conduct entity)
        {
            var datta = await _repositoryConduct.FirstOrDefaultAsync(x => x.AcademicYearId == entity.AcademicYearId && x.TermId == entity.TermId && x.ClassId == entity.ClassId && x.StudentId == entity.StudentId);
            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);

            if (datta == null)
            {
                entity.AcademicYearName = basicInfo.AcademicYearName;
                entity.TermName = basicInfo.TermName;
                entity.ClassName = basicInfo.ClassName;
                entity.StudentName = basicInfo.StudentName;
                entity.StudentIdentifier = basicInfo.StudentIdentifier;

                await _repositoryConduct.InsertAsync(entity);

               
            }
            else
            {

                datta.AcademicYearName = basicInfo.AcademicYearName;
                datta.TermName = basicInfo.TermName;
                datta.ClassName = basicInfo.ClassName;
                datta.StudentName = basicInfo.StudentName;
                datta.StudentIdentifier = basicInfo.StudentIdentifier;

                await _repositoryConduct.UpdateAsync(datta);

            }
        }

        public async Task Update(Conduct entity)
        {
            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);
            
            entity.AcademicYearName = basicInfo.AcademicYearName;
            entity.TermName = basicInfo.TermName;
            entity.ClassName = basicInfo.ClassName;
            entity.StudentName = basicInfo.StudentName;
            entity.StudentIdentifier = basicInfo.StudentIdentifier;


            await _repositoryConduct.UpdateAsync(entity);
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryConduct.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryConduct.DeleteAsync(id);

        }
    }
}
