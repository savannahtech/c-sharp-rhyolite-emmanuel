using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.School.Dashboard;
using System.Threading.Tasks;
using System;

namespace RhyoliteERP.Web.Controllers
{
    [Abp.AspNetCore.Mvc.Authorization.AbpMvcAuthorize]
    public class DashboardController : RhyoliteERPControllerBase
    {

        private readonly ISchDashboardAppService _schDashboardAppService;

        public DashboardController(ISchDashboardAppService schDashboardAppService)
        {
            _schDashboardAppService = schDashboardAppService;
        }
        public IActionResult SchoolManager()
        {
            return View();
        }

        public IActionResult Payroll()
        {
            return View();
        }

        public IActionResult Stock()
        {
            return View();
        }

        public IActionResult Banking()
        {
            return View();
        }

        public IActionResult AssetManager()
        {
            return View();
        }


        public IActionResult AccountPayables()
        {
            return View();
        }


        public IActionResult AccountReceivables()
        {
            return View();
        }

        public IActionResult GeneralLedger()
        {
            return View();
        }

        //api
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentGenderDistribution()
        {
            var result = await _schDashboardAppService.GetStudentGenderDistribution();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentGenderDistributionByClass([FromQuery] Guid classId)
        {
            var result = await _schDashboardAppService.GetStudentGenderDistribution(classId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStaffGenderDistribution()
        {
            var result = await _schDashboardAppService.GetStaffGenderDistribution();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPayments()
        {
            var result = await _schDashboardAppService.GetPayments();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPaymentsByClass([FromQuery] Guid classId)
        {
            var result = await _schDashboardAppService.GetPaymentsByClass(classId);
            return Json(result);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetMonthlyPayments()
        {
            var result = await _schDashboardAppService.GetMonthlyPayments();
            return Json(result);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetMonthlyPaymentsByClass([FromQuery] Guid classId)
        {
            var result = await _schDashboardAppService.GetMonthlyPayments(classId);
            return Json(result);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPaymentsTillDate()
        {
            var result = await _schDashboardAppService.GetPaymentsTillDate();
            return Json(result);
        }
    }
}
