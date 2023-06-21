using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.DomainServices.School.Messaging.Dto;
using RhyoliteERP.Services.Payroll.Reports;
using RhyoliteERP.Services.School.Reports;
using RhyoliteERP.Services.Shared.MessageTemplates;
using RhyoliteERP.Services.Shared.Messaging;
using RhyoliteERP.Services.Shared.SmsHistories;
using System;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class PayrollMessagingController : RhyoliteERPControllerBase
    {
        private readonly IMessagingAppService _messagingAppService;
        private readonly IPayrollReportsAppService _payrollReportsAppService;
        private readonly ISmsHistoryAppService _smsHistoryAppService;
        private readonly IMessageTemplateAppService _messageTemplateAppService;
        public PayrollMessagingController(IMessagingAppService messagingAppService, ISmsHistoryAppService smsHistoryAppService, IMessageTemplateAppService messageTemplateAppService, IPayrollReportsAppService payrollReportsAppService)
        {
            _messagingAppService = messagingAppService;
            _smsHistoryAppService = smsHistoryAppService;
            _messageTemplateAppService = messageTemplateAppService;
            _payrollReportsAppService = payrollReportsAppService;
        }


        public IActionResult Employees()
        {
            return View();
        }

        public IActionResult EmailPaySlips()
        {
            return View();
        }

        public IActionResult Templates()
        {
            return View();
        }

        public IActionResult TokenExpressions()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }

        //api
        public async Task<IActionResult> SendBulkMessage([FromBody] StartCampaign startCampaign)
        {

            startCampaign.TenantId = AbpSession.TenantId;
            startCampaign.ModuleSource = RhyoliteERPConsts.Payroll;
            
            //debit sms account

            await _messagingAppService.Send(startCampaign);
            return Json(200);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetMessages([FromQuery] DateTime startDate, DateTime endDate)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("");

            var reportDetails = await _smsHistoryAppService.ListAll(startDate, endDate, RhyoliteERPConsts.Payroll);

            return Json(new { reportHeader, reportDetails });
        }
    }
}
