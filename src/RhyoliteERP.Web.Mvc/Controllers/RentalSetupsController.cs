using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.PropertyRental.MeterTypes;
using RhyoliteERP.Services.PropertyRental.MeterTypes.Dto;
using RhyoliteERP.Services.PropertyRental.PropertyGroups;
using RhyoliteERP.Services.PropertyRental.PropertyGroups.Dto;
using RhyoliteERP.Services.PropertyRental.PropertyTypes;
using RhyoliteERP.Services.PropertyRental.PropertyTypes.Dto;
using RhyoliteERP.Services.PropertyRental.RentalNotificationSettings;
using RhyoliteERP.Services.PropertyRental.RentalNotificationSettings.Dto;
using RhyoliteERP.Services.PropertyRental.VendorCategories;
using RhyoliteERP.Services.PropertyRental.VendorCategories.Dto;
using System;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class RentalSetupsController : RhyoliteERPControllerBase
    {
        private readonly IPropertyTypeAppService _propertyTypeAppService;
        private readonly IMeterTypeAppService _meterTypeAppService;
        private readonly IPropertyGroupAppService _propertyGroupAppService;
        private readonly IVendorCategoryAppService _vendorCategoryAppService;
        private readonly IRentalNotificationSettingAppService _rentalNotificationSettingAppService;
        public RentalSetupsController(IPropertyTypeAppService propertyTypeAppService, IMeterTypeAppService meterTypeAppService, IPropertyGroupAppService propertyGroupAppService, IVendorCategoryAppService vendorCategoryAppService, IRentalNotificationSettingAppService rentalNotificationSettingAppService)
        {
            _propertyTypeAppService = propertyTypeAppService;
            _meterTypeAppService = meterTypeAppService;
            _propertyGroupAppService = propertyGroupAppService;
            _vendorCategoryAppService = vendorCategoryAppService;
            _rentalNotificationSettingAppService = rentalNotificationSettingAppService;
        }

        public IActionResult PropertyTypes()
        {
            return View();
        }

        public IActionResult PropertyGroups()
        {
            return View();
        }

        public IActionResult MeterTypes()
        {
            return View();
        }

        public IActionResult NotificationSettings()
        {
            return View();
        }

        //api

        //property types 
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPropertyTypes()
        {
            var result = await _propertyTypeAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreatePropertyType([FromBody] CreatePropertyTypeInput input)
        {
            var result = await _propertyTypeAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdatePropertyType([FromBody] UpdatePropertyTypeInput input)
        {
            await _propertyTypeAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelPropertyType([FromQuery] Guid id)
        {
            await _propertyTypeAppService.Delete(id);
            return Json(200);
        }

        //meter types 
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetMeterTypes()
        {
            var result = await _meterTypeAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateMeterType([FromBody] CreateMeterTypeInput input)
        {
            var result = await _meterTypeAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateMeterType([FromBody] UpdateMeterTypeInput input)
        {
            await _meterTypeAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelMeterType([FromQuery] Guid id)
        {
            await _meterTypeAppService.Delete(id);
            return Json(200);
        }

        //property groups 
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPropertyGroups()
        {
            var result = await _propertyGroupAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreatePropertyGroup([FromBody] CreatePropertyGroupInput input)
        {
            var result = await _propertyGroupAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdatePropertyGroup([FromBody] UpdatePropertyGroupInput input)
        {
            await _propertyGroupAppService.Update(input);
            return Json(200); 
        }

        public async Task<ActionResult> DelPropertyGroup([FromQuery] Guid id)
        {
            await _propertyGroupAppService.Delete(id);
            return Json(200);
        }

        //vendors
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetVendorCategories()
        {
            var result = await _vendorCategoryAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateVendorCategory([FromBody] CreateVendorCategoryInput input)
        {
            var result = await _vendorCategoryAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateVendorCategory([FromBody] UpdateVendorCategoryInput input)
        {
            await _vendorCategoryAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelVendorCategory([FromQuery] Guid id)
        {
            await _vendorCategoryAppService.Delete(id);
            return Json(200);
        }

        //notifications...
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetNotificationSettings()
        {
            var result = await _rentalNotificationSettingAppService.GetAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateNotificationSettings([FromBody] CreateRentalNotificationSettingInput input)
        {
            await _rentalNotificationSettingAppService.Create(input);
            return Json(200);

        }

        public async Task<ActionResult> UpdateNotificationSettings([FromBody] UpdateRentalNotificationSettingInput input)
        {
            await _rentalNotificationSettingAppService.Update(input);
            return Json(200);
        }

    }
}
