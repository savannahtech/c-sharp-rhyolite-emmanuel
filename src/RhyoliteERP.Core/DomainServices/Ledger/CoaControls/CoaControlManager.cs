using Abp.Domain.Repositories;
using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.CoaControls
{
   public class CoaControlManager : Abp.Domain.Services.DomainService, ICoaControlManager
    {
        private readonly IRepository<CoaControl, Guid> _repositoryCoaControl;

        public CoaControlManager(IRepository<CoaControl, Guid> repositoryCoaControl)
        {
            _repositoryCoaControl = repositoryCoaControl;
        }

        public async Task<object> Create(CoaControl entity)
        {
            var coaControl =
                await _repositoryCoaControl.FirstOrDefaultAsync(x => x.Id == entity.AccountGroupId && x.MinAccount == entity.MinAccount && x.MaxAccount == entity.MaxAccount);

            if (coaControl == null)
            {
                await _repositoryCoaControl.InsertAsync(entity);

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

        public async Task Update(CoaControl entity)
        {
            await _repositoryCoaControl.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryCoaControl.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll(Guid id)
        {
            return await _repositoryCoaControl.GetAllListAsync(x=>x.AccountGroupId == id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryCoaControl.GetAllListAsync();
        }

        public async Task Delete(Guid id)
        {
            await _repositoryCoaControl.DeleteAsync(id);

        }
    }
}
