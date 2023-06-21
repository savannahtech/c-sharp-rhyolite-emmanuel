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

namespace RhyoliteERP.DomainServices.School.Levels
{
   public class LevelManager: Abp.Domain.Services.DomainService, ILevelManager
    {
        private readonly IRepository<Level, Guid> _repositoryLevel;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public LevelManager(IRepository<Level, Guid> repositoryLevel, IUnitOfWorkManager unitOfWorkManager)
        {
            _repositoryLevel = repositoryLevel;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<object> Create(Level entity)
        {
            var datta = await _repositoryLevel.FirstOrDefaultAsync(x => x.Name == entity.Name);
            if (datta == null)
            {
                await _repositoryLevel.InsertAsync(entity);

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

        [HttpPost]
        public async Task CreateBulk(List<Level> inputs, int tenantId)
        {

            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                foreach (var entity in inputs)
                {
                    entity.TenantId = tenantId;
                    await _repositoryLevel.InsertAsync(entity);

                }

            }

        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryLevel.GetAllListAsync();
        }

        public async Task Delete(Guid id)
        {
            await _repositoryLevel.DeleteAsync(id);

        }

        public async Task Update(Level entity)
        {
            await _repositoryLevel.UpdateAsync(entity);
        }
    }
}
