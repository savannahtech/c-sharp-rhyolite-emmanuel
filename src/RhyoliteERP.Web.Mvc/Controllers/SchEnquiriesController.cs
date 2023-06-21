using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.School.AlumniHistories;
using RhyoliteERP.Services.School.BillPayments;
using RhyoliteERP.Services.School.CancelledBills;
using RhyoliteERP.Services.School.CancelledPayments;
using RhyoliteERP.Services.School.PromotionHistories;
using RhyoliteERP.Services.School.StudentAttendances;
using RhyoliteERP.Services.School.StudentBills;
using RhyoliteERP.Services.School.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SchEnquiriesController : RhyoliteERPControllerBase
    {
        private readonly IAlumniHistoryAppService _alumniHistoryAppService;
        private readonly IStudentAttendanceAppService _studentAttendanceAppService;
        private readonly IStudentAppService _studentAppService;
        private readonly IStudentBillAppService _studentBillAppService;
        private readonly IBillPaymentAppService _billPaymentAppService;
        private readonly ICancelledPaymentAppService _cancelledPaymentAppService;
        private readonly ICancelledBillAppService _cancelledBillAppService;
        private readonly IPromotionHistoryAppService _promotionHistoryAppService;
        public SchEnquiriesController(IAlumniHistoryAppService alumniHistoryAppService, IStudentAttendanceAppService studentAttendanceAppService, IStudentAppService studentAppService, IStudentBillAppService studentBillAppService, IBillPaymentAppService billPaymentAppService, ICancelledPaymentAppService cancelledPaymentAppService, ICancelledBillAppService cancelledBillAppService, IPromotionHistoryAppService promotionHistoryAppService)
        {
            _alumniHistoryAppService = alumniHistoryAppService;
            _studentAttendanceAppService = studentAttendanceAppService;
            _studentAppService = studentAppService;
            _studentBillAppService = studentBillAppService;
            _billPaymentAppService = billPaymentAppService;
            _cancelledPaymentAppService = cancelledPaymentAppService;
            _cancelledBillAppService = cancelledBillAppService;
            _promotionHistoryAppService = promotionHistoryAppService;
        }

        public IActionResult StudentAttendance()
        {
            return View();
        }

        public IActionResult StaffAttendance()
        {
            return View();
        }

        public IActionResult StudentsByClass()
        {
            return View();
        }

        public IActionResult StudentsByNationality()
        {
            return View();
        }

        public IActionResult StudentsByReligion()
        {
            return View();
        }

        public IActionResult StudentBalances()
        {
            return View();
        }

        public IActionResult StudentPayments()
        {
            return View();
        }

        public IActionResult CreditMemos()
        {
            return View();
        }

        public IActionResult PaidUpStudents()
        {
            return View();
        }

        public IActionResult StudentDebtors()
        {
            return View();
        }

        public IActionResult DailyPayments()
        {
            return View();
        }

        public IActionResult CancelledBills()
        {
            return View();
        }

        public IActionResult TerminalResults()
        {
            return View();
        }

        public IActionResult Alumni()
        {
            return View();
        }

        public IActionResult PromotionHistory()
        {
            return View();
        }

        //api
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetAlumniByAcademicYear([FromQuery] Guid academicYearId)
        {
            var response = await _alumniHistoryAppService.ListAll(academicYearId);
            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentAttendance([FromQuery] Guid classId, DateTime startDate, DateTime endDate)
        {
            var response = await _studentAttendanceAppService.ListAll(classId, startDate, endDate);
            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentsByClass([FromQuery] Guid classId)
        {
            var result = await _studentAppService.EnqStudentsByClass(classId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentsByNationality([FromQuery] Guid nationalityId)
        {
            var result = await _studentAppService.EnqStudentsByNationality(nationalityId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentsByReligion([FromQuery] Guid religionId)
        {
            var result = await _studentAppService.EnqStudentsByReligion(religionId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetCancelledBills([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            var result = await _cancelledBillAppService.ListAll(academicYearId, termId, classId, billTypeId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentBalances([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            var result = await _cancelledBillAppService.ListAll(academicYearId, termId, classId, billTypeId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPaidUpStudents([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var result = await _studentBillAppService.GetPaidUpStudents(academicYearId, termId, classId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentDebtors([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var result = await _studentBillAppService.GetStudentDebtors(academicYearId, termId, classId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetCreditMemos([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var result = await _billPaymentAppService.ListAllCreditMemos(academicYearId, termId, classId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentPayments([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var result = await _billPaymentAppService.ListAll(academicYearId, termId, classId);
            return Json(result);
        }
        

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetDailyPayments([FromQuery] DateTime paymentDate)
        {
            var result = await _billPaymentAppService.ListDailyPayments(paymentDate);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPromotionHistory([FromQuery] Guid academicYearId)
        {
            var result = await _promotionHistoryAppService.ListAll(academicYearId);
            return Json(result);
        }


    }
}
