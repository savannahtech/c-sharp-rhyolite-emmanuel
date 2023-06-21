using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RhyoliteERP.Controllers;
using RhyoliteERP.Redis;
using RhyoliteERP.Services.School.BillPayments;
using RhyoliteERP.Services.School.BillPayments.Dto;
using RhyoliteERP.Services.School.Reports;
using RhyoliteERP.Services.School.StudentBills;
using RhyoliteERP.Services.School.StudentBills.Dto;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class BillsAndPaymentsController : RhyoliteERPControllerBase
    {

        private readonly IStudentBillAppService _studentBillAppService;
        private readonly IBillPaymentAppService _billPaymentAppService;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly ISmReportAppService _smReportAppService;
        private readonly int _paymentReceiptDatabaseId;


        public BillsAndPaymentsController(IStudentBillAppService studentBillAppService, IConfiguration configuration, IBillPaymentAppService billPaymentAppService, IRedisCacheManager redisCacheManager, ISmReportAppService smReportAppService)
        {
            _studentBillAppService = studentBillAppService;
            _billPaymentAppService = billPaymentAppService;
            _redisCacheManager = redisCacheManager;
            _paymentReceiptDatabaseId = Convert.ToInt32(configuration["RedisCache:PaymentReceiptDatabaseId"]);
            _smReportAppService = smReportAppService;
        }

        public IActionResult ProcessBills()
        {
            return View();
        }

        public IActionResult ViewBills()
        {
            return View();
        }

        public IActionResult Payments()
        {
            return View();
        }

        public IActionResult CancelOpeningBalances()
        {
            return View();
        }

        
        public async Task<IActionResult> Receipt(string id)
        {
            dynamic viewmodel = new ExpandoObject();


            var receiptCache = await _redisCacheManager.GetValueAsync(_paymentReceiptDatabaseId, id);
            var paymentData = JsonConvert.DeserializeObject<RhyoliteERP.Models.School.BillPayment>(receiptCache);

            var header = await _smReportAppService.ListSchProfile(string.Empty);
            var details = await _smReportAppService.GetReceipt(paymentData.AcademicYearId, paymentData.TermId, paymentData.ClassId, paymentData.StudentId, paymentData.ReceiptNo);
            viewmodel.Header = header;
            viewmodel.Details = details;
            return View(viewmodel);
        }

        public IActionResult PostPayments()
        {
            return View();
        }

        public IActionResult CancelBills()
        {
            return View();
        }

        public IActionResult CancelPayments()
        {
            return View();
        }

        public IActionResult CreditMemo()
        {
            return View();
        }

        public IActionResult OpeningBalances()
        {
            return View();
        }

        

        //api
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetBillSetup([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            var result = await _studentBillAppService.ListAll(academicYearId, termId, classId, billTypeId);

            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> CreateBills([FromBody] List<CreateStudentBillInput> billList)
        {
            await _studentBillAppService.CreateBatch(billList);
            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> CreateOpeningBalance([FromBody] CreateStudentBillInput openingBalance)
        {
            await _studentBillAppService.CreateOpeningBalance(openingBalance);
            return Json(200);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentBills([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            var result = await _studentBillAppService.ListAll(academicYearId, termId, classId, billTypeId);

            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetBillsToCancel([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            var result = await _studentBillAppService.ListBillsToCancel(academicYearId, termId, classId, billTypeId);

            return Json(result);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> ListStudentBills([FromQuery] Guid id)
        {
            var result = await _studentBillAppService.ListStudentBills(id);
            return Json(result);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> PayStudentBills([FromBody] List<CreateBillPaymentInput> paymentList)
        {
            List<object> receiptList = new List<object>();

            foreach (var payment in paymentList)
            {
               var encodedReceipt =  await _billPaymentAppService.Create(payment);
                receiptList.Add(encodedReceipt);
            }
            return Json(receiptList);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> CreateCreditMemo([FromBody] CreateBillPaymentInput input)
        {
            var encodedReceipt = await _billPaymentAppService.CreateCreditMemo(input);
            return Json(encodedReceipt);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetBillPayments([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var response = await _billPaymentAppService.ListAllUnPosted(academicYearId, termId, classId);
            return Json(response);
        }

        public async Task<JsonResult> PostBillPayments([FromBody] List<Guid> ids)
        {
            await _billPaymentAppService.PostPayments(ids);
            return Json(200);
        }

        public async Task<JsonResult> CancelStudentPayments([FromBody] List<CancelBillPaymentInput> paymentList)
        {
            await _billPaymentAppService.CancelBatch(paymentList);
            return Json(200);
        }

         
        public async Task<JsonResult> CancelStudentBills([FromBody] List<CancelStudentBillInput> billList)
        {
            await _studentBillAppService.CancelBatch(billList);
            return Json(200);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetOpeningBalances([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var result = await _studentBillAppService.ListOpeningBalancesToCancel(academicYearId, termId, classId);

            return Json(result);
        }

         

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPostedPayments([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var response = await _billPaymentAppService.ListAllPosted(academicYearId, termId, classId);
            return Json(response);
        }
    }
}
