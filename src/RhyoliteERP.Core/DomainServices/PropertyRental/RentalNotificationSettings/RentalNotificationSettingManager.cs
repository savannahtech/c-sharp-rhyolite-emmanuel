using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.RentalNotificationSettings
{
    public class RentalNotificationSettingManager: Abp.Domain.Services.DomainService, IRentalNotificationSettingManager
    {

        private readonly IRepository<RentalNotificationSetting, Guid> _repositoryRentalNotificationSetting;

        public RentalNotificationSettingManager(IRepository<RentalNotificationSetting, Guid> repositoryRentalNotificationSetting)
        {
            _repositoryRentalNotificationSetting = repositoryRentalNotificationSetting;
        }


        public async Task Create(RentalNotificationSetting entity)
        {
            await _repositoryRentalNotificationSetting.InsertAsync(entity);

        }

        public async Task Update(RentalNotificationSetting entity)
        {
            await _repositoryRentalNotificationSetting.UpdateAsync(entity);
        }

      
        public async Task<object> GetAll()
        {
            return await _repositoryRentalNotificationSetting.GetAll().FirstOrDefaultAsync();
        }
        
    }
}
