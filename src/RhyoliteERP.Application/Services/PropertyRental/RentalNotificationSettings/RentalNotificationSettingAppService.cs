using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.RentalNotificationSettings;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.RentalNotificationSettings.Dto;
using RhyoliteERP.Services.PropertyRental.Vendors.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.RentalNotificationSettings
{
    public class RentalNotificationSettingAppService: RhyoliteERPAppServiceBase, IRentalNotificationSettingAppService
    {

        private readonly IRentalNotificationSettingManager _rentalNotificationSettingManager;
        private readonly IMapper _mapper;

        public RentalNotificationSettingAppService(IRentalNotificationSettingManager rentalNotificationSettingManager, IMapper mapper)
        {
            _rentalNotificationSettingManager = rentalNotificationSettingManager;
            _mapper = mapper;
        }


        public async Task<object> GetAll()
        {
            return await _rentalNotificationSettingManager.GetAll();
        }

        public async Task Create(CreateRentalNotificationSettingInput input)
        {
            var obj = _mapper.Map<RentalNotificationSetting>(input);
            await _rentalNotificationSettingManager.Create(obj);
        }

        public async Task Update(UpdateRentalNotificationSettingInput input)
        {
            var obj = _mapper.Map<RentalNotificationSetting>(input);
            await _rentalNotificationSettingManager.Update(obj);
        }
    }
}
