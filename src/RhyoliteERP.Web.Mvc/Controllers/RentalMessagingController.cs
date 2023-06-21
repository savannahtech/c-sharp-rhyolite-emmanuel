using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RhyoliteERP.Controllers;
using RhyoliteERP.DomainServices.School.Messaging.Dto;
using RhyoliteERP.Services.PropertyRental.Reports;
using RhyoliteERP.Services.Shared.MessageTemplates;
using RhyoliteERP.Services.Shared.MessageTemplates.Dto;
using RhyoliteERP.Services.Shared.Messaging;
using RhyoliteERP.Services.Shared.SmsHistories;
using RhyoliteERP.Subscription;
using RhyoliteERP.Web.Helpers;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class RentalMessagingController : RhyoliteERPControllerBase
    {
        private readonly IMessagingAppService _messagingAppService;
        private readonly ISmsHistoryAppService _smsHistoryAppService;
        private readonly IMessageTemplateAppService _messageTemplateAppService;
        private readonly ISubscriptionAppService _subscriptionAppService;
        private readonly IRentalReportAppService _rentalReportAppService;
        private readonly decimal _standardSmsRate;
        private readonly IWebHostEnvironment _env;

        public RentalMessagingController(IConfiguration configuration, IMessagingAppService messagingAppService, ISmsHistoryAppService smsHistoryAppService, IMessageTemplateAppService messageTemplateAppService, ISubscriptionAppService subscriptionAppService, IRentalReportAppService rentalReportAppService, IWebHostEnvironment env)
        {
            _messagingAppService = messagingAppService;
            _smsHistoryAppService = smsHistoryAppService;
            _messageTemplateAppService = messageTemplateAppService;
            _subscriptionAppService = subscriptionAppService;
            _rentalReportAppService = rentalReportAppService;
            _standardSmsRate = Convert.ToDecimal(configuration["Messaging:StandardRate"]);
            _env = env;
        }


        public IActionResult Tenants()
        {
            return View();
        }

        public IActionResult Applicants()
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

        public IActionResult SendFromExcel()
        {
            return View();
        }


        public IActionResult Reports()
        {
            return View();
        }


        //api...
        public async Task<IActionResult> SendBulkMessage([FromBody] StartCampaign startCampaign)
        {

            int totalSmsParts = MesssageUtils.CalculateParts(startCampaign.Message);

            // check sms account balance... 

            var totalSmsCost = startCampaign.RecipientId == Guid.Empty ? totalSmsParts * _standardSmsRate * startCampaign.RecipientCount : totalSmsParts * _standardSmsRate;

            var smsAccountInfo = await _subscriptionAppService.GetSmsAccountBalance();

            if (smsAccountInfo != null && smsAccountInfo.Result.Balance < totalSmsCost)
            {
                return Json(new { code = 400, message = "You have insufficent funds to send messages. Kindly topup your SMS credit." });
            }

            //debit sms account 
            var isDebitSuccessful = await _subscriptionAppService.DebitSmsAccount(new Subscription.Dto.SmsBillInfo { Rate = totalSmsCost, AccountSource = "erp", TenantId = AbpSession.TenantId });

            if (!isDebitSuccessful)
            {
                //notify all support personnel of this unsuccessful debit.

                return Json(new { code = 400, message = "Debit failed. Kindly contact support for immediate assistance." });
            }

            startCampaign.TenantId = AbpSession.TenantId;

            startCampaign.ModuleSource = RhyoliteERPConsts.PropertyRentals;

            await _messagingAppService.Send(startCampaign);

            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> SendMessagesFromExcel([FromForm] StartCampaign startCampaign)
        {

            try
            {
                int totalSmsParts = MesssageUtils.CalculateParts(startCampaign.Message);

                ExcelEngine excelEngine = new ExcelEngine();
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(startCampaign.File.OpenReadStream());
                IWorksheet worksheet = workbook.Worksheets[0];

                // Read data from the worksheet and Export to the DataTable.

                DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

                List<string> recipientList = new List<string>();

                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    var row = excelTable.Rows[i];

                    var recipient = row.Table.Columns.Contains("Recipient") ? row["Recipient"].ToString().Trim() : string.Empty;

                    if (string.IsNullOrEmpty(recipient))
                    {
                        continue;
                    }

                    if (recipient.Length == 9)
                    {
                        recipient = $"0{recipient}";
                    }
                    recipientList.Add(recipient);

                }

                // check sms account balance 
                var totalSmsCost = totalSmsParts * _standardSmsRate * recipientList.Count;

                var smsAccountInfo = await _subscriptionAppService.GetSmsAccountBalance();

                if (smsAccountInfo != null && smsAccountInfo.Result.Balance < totalSmsCost)
                {
                    return Json(new { code = 400, message = "You have insufficent funds to send messages. Kindly topup your SMS credit." });
                }

                //debit sms account 
                var isDebitSuccessful = await _subscriptionAppService.DebitSmsAccount(new Subscription.Dto.SmsBillInfo { Rate = totalSmsCost, AccountSource = "erp", TenantId = AbpSession.TenantId });

                if (!isDebitSuccessful)
                {
                    return Json(new { code = 400, message = "Debit failed. Kindly contact support for immediate assistance." });
                }

                startCampaign.RecipientList = recipientList;
                startCampaign.TenantId = AbpSession.TenantId;
                startCampaign.ModuleSource = RhyoliteERPConsts.PropertyRentals;
                await _messagingAppService.SendPersonalized(startCampaign);

                return Json(new { code = 200, message = "Messeges sent !" });
            }
            catch (Exception ex)
            {

                return Json(new { code = 400, message = $"Message: {ex.Message}\nDetails: {ex.StackTrace}" });

            }


        }

        public async Task<IActionResult> DownloadMessagingUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "Custom Message_Sample_Simple.xlsx"));
            return File(fileBytes, "application/ms-excel", "Custom Message_Sample_Simple.xlsx");
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetMessages([FromQuery] DateTime startDate, DateTime endDate)
        {
            var header = await _rentalReportAppService.GetBusinessProfile($"{RhyoliteERPConsts.SmsReports} {startDate:dd-MMM-yyyy} to {endDate:dd-MMM-yyyy}");

            var details = await _smsHistoryAppService.ListAll(startDate, endDate, RhyoliteERPConsts.PropertyRentals);

            return Json(new { header, details });
        }

        //message templates...
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetMessageTemplates([FromQuery] int pageNo, int pageSize)
        {
            var result = await _messageTemplateAppService.ListAll(pageNo, pageSize);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateMessageTemplate([FromBody] CreateMessageTemplateInput input)
        {
            var result = await _messageTemplateAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateMessageTemplate([FromBody] UpdateMessageTemplateInput input)
        {
            await _messageTemplateAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelMessageTemplate([FromQuery] Guid id)
        {
            await _messageTemplateAppService.Delete(id);
            return Json(200);
        }

    }
}
