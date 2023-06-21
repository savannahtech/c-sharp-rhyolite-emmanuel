using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.SmsHistories
{
   public class SmsHistoryManager: Abp.Domain.Services.DomainService, ISmsHistoryManager
    {
        private readonly IRepository<SmsHistory, Guid> _repositorySmsHistory;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SmsHistoryManager(IRepository<SmsHistory, Guid> repositorySmsHistory, IUnitOfWorkManager unitOfWorkManager)
        {
            _repositorySmsHistory = repositorySmsHistory;
            _unitOfWorkManager = unitOfWorkManager;
        }


        public async Task Create(SmsHistory entity)
        {
            using (_unitOfWorkManager.Current.SetTenantId(entity.TenantId))
            {
                await _repositorySmsHistory.InsertAsync(entity);

            }
        }

        public async Task<object> ListAll(DateTime startDate, DateTime endDate, int source)
        {
           return await _repositorySmsHistory.GetAllListAsync(x=> startDate >= x.CreationTime  && endDate <= x.CreationTime && x.ModuleSource == source);
        }
       
    }
}
