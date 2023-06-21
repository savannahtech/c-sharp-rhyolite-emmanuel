using Abp.Domain.Repositories;
using RhyoliteERP.Models.Ledger;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.CostCenters
{
    public class CostCenterManager: Abp.Domain.Services.DomainService, ICostCenterManager
    {
        private readonly IRepository<CostCenter, Guid> _repositoryCostCenter;

        public CostCenterManager(IRepository<CostCenter, Guid> repositoryCostCenter)
        {
            _repositoryCostCenter = repositoryCostCenter;
        }

        public async Task<object> Create(CostCenter entity)
        {
            var datta = await _repositoryCostCenter.FirstOrDefaultAsync(x => x.Name == entity.Name && x.ParentId == entity.ParentId);

            if (datta == null)
            {
                await _repositoryCostCenter.InsertAsync(entity);

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

        public async Task Update(CostCenter entity)
        {
            await _repositoryCostCenter.UpdateAsync(entity);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryCostCenter.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryCostCenter.DeleteAsync(id);

        }

    }
}
