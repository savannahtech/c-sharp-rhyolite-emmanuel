using Abp.Domain.Repositories;
using RhyoliteERP.Models.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Ledger.CoaDetails
{
   public class CoaDetailManager : Abp.Domain.Services.DomainService, ICoaDetailManager
    {
        private readonly IRepository<CoaDetail, Guid> _repositoryCoaDetail;
        private readonly IRepository<CoaControl, Guid> _repositoryCoaControl;

        public CoaDetailManager(IRepository<CoaDetail, Guid> repositoryCoaDetail, IRepository<CoaControl, Guid> repositoryCoaControl)
        {
            _repositoryCoaDetail = repositoryCoaDetail;
            _repositoryCoaControl = repositoryCoaControl;
        }

        public async Task<object> Create(CoaDetail entity)
        {
            var coaControl =
                await _repositoryCoaControl.FirstOrDefaultAsync(x => x.Id == entity.AccountHeaderId);

            if (entity.AccountNo >= coaControl.MinAccount && entity.AccountNo <= coaControl.MaxAccount)
            {
                 
                entity.AccountName.Trim();
                await _repositoryCoaDetail.InsertAsync(entity);
                return new
                {
                    code = 200,
                    message = "Successful",
                };
            }
            else
            {
                return new
                {
                    code = 400,
                    message = "Account No. not in range",
                };

            }
        }

        public async Task Delete(Guid id)
        {
            await _repositoryCoaDetail.DeleteAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryCoaDetail.GetAllListAsync();
        }
        public async Task<IEnumerable<object>> ListActiveAccounts()
        {
            return await _repositoryCoaDetail.GetAllListAsync(x=>x.Status == "Active");
        }
        public async Task Update(CoaDetail entity)
        {
            await _repositoryCoaDetail.UpdateAsync(entity);
        }

    }
}
