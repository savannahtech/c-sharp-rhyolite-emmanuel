using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using RhyoliteERP.Authorization;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.SpecialDuties
{
   public class SpecialDutyManager: Abp.Domain.Services.DomainService, ISpecialDutyManager
    {
        private readonly IRepository<SpecialDuty, Guid> _repositorySpecialDuty;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SpecialDutyManager(IRepository<SpecialDuty, Guid> repositorySpecialDuty, IUnitOfWorkManager unitOfWorkManager)
        {
            _repositorySpecialDuty = repositorySpecialDuty;
            _unitOfWorkManager = unitOfWorkManager;
        }


        public async Task<object> Create(SpecialDuty entity)
        {
            var datta = await _repositorySpecialDuty.FirstOrDefaultAsync(x => x.Name == entity.Name);
            if (datta == null)
            {
                await _repositorySpecialDuty.InsertAsync(entity);
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

        public async Task CreateBulk(List<SpecialDuty> inputs, int tenantId)
        {

            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                foreach (var entity in inputs)
                {
                    entity.TenantId = tenantId;

                    await _repositorySpecialDuty.InsertAsync(entity);

                }

            }

        }



        public async Task Delete(Guid id)
        {
            await _repositorySpecialDuty.DeleteAsync(id);
        }

        public async Task Update(SpecialDuty entity)
        {
            await _repositorySpecialDuty.UpdateAsync(entity);
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositorySpecialDuty.GetAllListAsync();
        }
    }
}
