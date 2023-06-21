using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RhyoliteERP.Controllers;
using RhyoliteERP.DomainServices.School.Messaging.Dto;
using RhyoliteERP.Services.School.Reports;
using RhyoliteERP.Services.Shared.MessageTemplates;
using RhyoliteERP.Services.Shared.MessageTemplates.Dto;
using RhyoliteERP.Services.Shared.Messaging;
using RhyoliteERP.Services.Shared.SmsHistories;
using RhyoliteERP.Subscription;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SchMessagingController : RhyoliteERPControllerBase
    {
        
        private readonly IMessagingAppService _messagingAppService;
        private readonly ISmReportAppService _smReportAppService;
        private readonly ISmsHistoryAppService _smsHistoryAppService;
        private readonly IMessageTemplateAppService _messageTemplateAppService;
        private readonly ISubscriptionAppService _subscriptionAppService;
        private readonly decimal _standardSmsRate;

        private readonly IWebHostEnvironment _env;
        public SchMessagingController(IMessagingAppService messagingAppService, ISmReportAppService smReportAppService, ISmsHistoryAppService smsHistoryAppService, IMessageTemplateAppService messageTemplateAppService, IWebHostEnvironment env, ISubscriptionAppService subscriptionAppService, IConfiguration configuration)
        {
            _messagingAppService = messagingAppService;
            _smReportAppService = smReportAppService;
            _smsHistoryAppService = smsHistoryAppService;
            _messageTemplateAppService = messageTemplateAppService;
            _env = env;
            _subscriptionAppService = subscriptionAppService;
            _standardSmsRate = Convert.ToDecimal(configuration["Messaging:StandardRate"]);
        }

        public IActionResult Parent()
        {
            return View();
        }

        public IActionResult Staff()
        {
            return View();
        }

        public IActionResult TerminalReports()
        {
            return View();
        }

        public IActionResult ResendReceipts()
        {
            return View();
        }

        public IActionResult Templates()
        {
            return View();
        }

        public IActionResult Reports()
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

        //api
        public async Task<IActionResult> SendBulkMessage([FromBody] StartCampaign startCampaign)
        {
            startCampaign.TenantId = AbpSession.TenantId;
            startCampaign.ModuleSource = RhyoliteERPConsts.SchoolManager;

            await _messagingAppService.Send(startCampaign);
            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> SendMessagesFromExcel([FromForm] StartCampaign startCampaign)
        {

            try
            {
                int totalSmsParts = CalculateParts(startCampaign.Message);

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
                startCampaign.ModuleSource = RhyoliteERPConsts.SchoolManager;
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
        public async Task<IActionResult> SendReceipt([FromBody] ReceiptDto dto)
        {
            dto.TenantId = AbpSession.TenantId;

            await _messagingAppService.SendStudentReceipt(dto);

            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetMessages([FromQuery]DateTime startDate, DateTime endDate)
        {
            var header = await _smReportAppService.ListSchProfile($"{RhyoliteERPConsts.SmsReports} {startDate:dd-MMM-yyyy} to {endDate:dd-MMM-yyyy}");

            var details = await _smsHistoryAppService.ListAll(startDate,endDate, RhyoliteERPConsts.SchoolManager);

            return Json(new { header, details });
        }

        //message templates...
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetMessageTemplates([FromQuery]int pageNo,int pageSize)
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


        private int CalculateParts(string message)
        {
            const int creditPerPart = 1;
            const int singleMessageLength = 160;
            const int multiMessageLength = 153;
            double len = message.Length;
            if (len <= singleMessageLength)
                return creditPerPart;
            return (int)(Math.Ceiling(len / multiMessageLength) * creditPerPart);
        }

    }
}
