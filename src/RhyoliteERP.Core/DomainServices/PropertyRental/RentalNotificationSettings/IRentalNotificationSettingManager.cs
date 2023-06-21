using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.RentalNotificationSettings
{
    public interface IRentalNotificationSettingManager: Abp.Domain.Services.IDomainService
    {
        Task<object> GetAll();
        Task Create(RentalNotificationSetting input);
        Task Update(RentalNotificationSetting input);

    }
}
