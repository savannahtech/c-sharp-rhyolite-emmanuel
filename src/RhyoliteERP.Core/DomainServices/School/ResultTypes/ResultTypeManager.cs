using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Authorization;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.ResultTypes
{
   public class ResultTypeManager: Abp.Domain.Services.DomainService, IResultTypeManager
    {

        private readonly IRepository<ResultType, Guid> _repositoryResultType;
        private readonly IRepository<Level, Guid> _repositoryLevel;
        private readonly IRepository<SchClass, Guid> _repositorySchClass;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public ResultTypeManager(IRepository<ResultType, Guid> repositoryResultType, IUnitOfWorkManager unitOfWorkManager, IRepository<Level, Guid> repositoryLevel, IRepository<SchClass, Guid> repositorySchClass)
        {
            _repositoryResultType = repositoryResultType;
            _unitOfWorkManager = unitOfWorkManager;
            _repositoryLevel = repositoryLevel;
            _repositorySchClass = repositorySchClass;
        }

        public async Task<object> Create(ResultType entity)
        {

            var level = await _repositoryLevel.FirstOrDefaultAsync(x=>x.Id == entity.LevelId);

            var datta = await _repositoryResultType.FirstOrDefaultAsync(x => x.Name == entity.Name && x.LevelId == entity.LevelId);
            if (datta == null)
            {
                if (level !=null)
                {
                    entity.LevelName = level.Name;
                }
                await _repositoryResultType.InsertAsync(entity);
                return new
                {
                    code = 200,
                    message = "successful"
                };

            }
            else
            {
                return new
                {
                    code = 400,
                    message = "Duplicate records are not allowed."
                };
            }
        }

        public async Task Update(ResultType entity)
        {

            var level = await _repositoryLevel.FirstOrDefaultAsync(x => x.Id == entity.LevelId);
            if (level !=null)
            {
                entity.LevelName = level.Name;
            }
            await _repositoryResultType.UpdateAsync(entity);
        }


        public async Task<IEnumerable<object>> ListAll(Guid levelId)
        {
            return await _repositoryResultType.GetAllListAsync(x => x.LevelId == levelId);
        }

        public async Task<IEnumerable<object>> ListAll(int tenantId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            { 
                return await _repositoryResultType.GetAllListAsync();
            }
        }

        public async Task<IEnumerable<object>> ListByClass(Guid classId)
        {
            var schClass = await _repositorySchClass.FirstOrDefaultAsync(x => x.Id == classId);

            return await _repositoryResultType.GetAllListAsync(x => x.LevelId == schClass.LevelId);
        }

       

        public async Task CreateBulk(List<ResultType> inputs, int tenantId)
        {

            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                foreach (var entity in inputs)
                {
                    var level = await _repositoryLevel.FirstOrDefaultAsync(x => x.Id == entity.Id);
                    entity.LevelName = level.Name;
                    entity.TenantId = tenantId;

                    await _repositoryResultType.InsertAsync(entity);

                }

            }

        }

        public async Task Delete(Guid id)
        {
            await _repositoryResultType.DeleteAsync(id);
        }


    }
}
