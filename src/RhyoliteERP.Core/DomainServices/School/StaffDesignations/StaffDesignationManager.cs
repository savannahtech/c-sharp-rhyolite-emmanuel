using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using RhyoliteERP.Authorization;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StaffDesignations
{
   public class StaffDesignationManager: Abp.Domain.Services.DomainService, IStaffDesignationManager
    {
        private readonly IRepository<StaffDesignation, Guid> _repositoryStaffDesignation;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public StaffDesignationManager(IRepository<StaffDesignation, Guid> repositoryStaffDesignation, IUnitOfWorkManager unitOfWorkManager)
        {
            _repositoryStaffDesignation = repositoryStaffDesignation;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<object> Create(StaffDesignation entity)
        {
            var datta = await _repositoryStaffDesignation.FirstOrDefaultAsync(x => x.Name == entity.Name);
            if (datta == null)
            {

                await _repositoryStaffDesignation.InsertAsync(entity);
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


        public async Task CreateBulk(List<StaffDesignation> inputs, int tenantId)
        {

            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                foreach (var entity in inputs)
                {
                    entity.TenantId = tenantId;

                    await _repositoryStaffDesignation.InsertAsync(entity);

                }

            }

        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryStaffDesignation.GetAllListAsync();

        }

        public async Task Delete(Guid id)
        {
            await _repositoryStaffDesignation.DeleteAsync(id);

        }

        public async Task Update(StaffDesignation entity)
        {
            await _repositoryStaffDesignation.UpdateAsync(entity);
        }

    }
}
