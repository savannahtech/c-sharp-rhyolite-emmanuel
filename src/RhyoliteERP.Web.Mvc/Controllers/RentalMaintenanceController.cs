using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.PropertyRental.VendorCategories.Dto;
using RhyoliteERP.Services.PropertyRental.Vendors;
using System.Threading.Tasks;
using System;
using RhyoliteERP.Services.PropertyRental.Vendors.Dto;
using RhyoliteERP.Services.PropertyRental.WorkOrders;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class RentalMaintenanceController : RhyoliteERPControllerBase
    {
        private readonly IVendorAppService _vendorAppService;
        private readonly IWorkOrderAppService _workOrderAppService;
        public RentalMaintenanceController(IVendorAppService vendorAppService, IWorkOrderAppService workOrderAppService)
        {
            _vendorAppService = vendorAppService;
            _workOrderAppService = workOrderAppService;
        }



        public IActionResult Vendors()
        {
            return View();
        }

        public IActionResult WorkOrders()
        {
            return View();
        }

        public IActionResult PropertyInspections()
        {
            return View();
        }

        //api

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetVendors()
        {
            var result = await _vendorAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateVendor([FromBody] CreateVendorInput input)
        {
            var result = await _vendorAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateVendor([FromBody] UpdateVendorInput input)
        {
            await _vendorAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelVendor([FromQuery] Guid id)
        {
            await _vendorAppService.Delete(id);
            return Json(200);
        }
    }
}
