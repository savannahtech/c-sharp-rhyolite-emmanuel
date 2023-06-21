using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.Payroll.ProcessPay;
using RhyoliteERP.Subscription;
using System;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [Abp.AspNetCore.Mvc.Authorization.AbpMvcAuthorize]
    public class ProcessPayController : RhyoliteERPControllerBase
    {
        private readonly IProcessPayAppService _processPayAppService;
        private readonly ISubscriptionAppService _subscriptionAppService;

        public ProcessPayController(IProcessPayAppService processPayAppService, ISubscriptionAppService subscriptionAppService)
        {
            _processPayAppService = processPayAppService;
            _subscriptionAppService = subscriptionAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PostPayroll()
        {
            return View();
        }


        //api
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> InitPay([FromQuery] Guid id)
        {
            var results =  await _processPayAppService.ProcessPayroll(id);
            return Json(results);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPayrollResults()
        {
            var results = await _processPayAppService.GetPayrollResults();
            return Json(results);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> InitPostPayroll()
        {
             await _processPayAppService.PostPayroll();
            
            // post to GL ()=> check GL Subscription
            var isValidLicense = await _subscriptionAppService.ValidateModuleLicense(RhyoliteERPConsts.GeneralLedger);

            if (isValidLicense)
            {
                // post to GL...
            }

            return Json(new { code = isValidLicense ? 200 :400, message = isValidLicense ? "Payroll successfully posted to GL." : "Unable to post to GL.\nKindly purchase a GL processing bundles to proceed." });
        }
    }
}
