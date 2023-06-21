using RhyoliteERP.Services.PropertyRental.RentalNotificationSettings.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.RentalNotificationSettings
{
    public interface IRentalNotificationSettingAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> GetAll();
        Task Create(CreateRentalNotificationSettingInput input);
        Task Update(UpdateRentalNotificationSettingInput input);

    }
}
