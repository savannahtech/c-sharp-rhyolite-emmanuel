using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.School.Staffs;
using RhyoliteERP.Services.School.Staffs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class StaffController : RhyoliteERPControllerBase
    {
        private readonly IStaffAppService _staffAppService;

        public StaffController(IStaffAppService staffAppService)
        {
            _staffAppService = staffAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrUpdate()
        {
            return View();
        }

        public IActionResult AssignSubjects()
        {
            return View();
        }

        public IActionResult AssignClasses()
        {
            return View();
        }

        public IActionResult AssignDesignation()
        {
            return View();
        }

        public IActionResult AssignSpecialDuty()
        {
            return View();
        }

        //api
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetStaff()
        {
            var response = await _staffAppService.ListAll(true);
            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetStaffDetails([FromQuery]Guid id)
        {
            var response = await _staffAppService.GetAsync(id);
            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffInput input)
        {
            var response = await _staffAppService.Create(input);
            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> UpdateStaff([FromBody] UpdateStaffInput input)
        {
            await _staffAppService.Update(input);
            return Json(200);
        }

        public async Task<IActionResult> DelStaff([FromQuery] Guid id)
        {
            await _staffAppService.Delete(id);
            return Json(200);
        }
    }
}
