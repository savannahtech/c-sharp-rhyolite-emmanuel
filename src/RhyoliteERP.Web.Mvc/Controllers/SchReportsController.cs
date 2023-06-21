using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.School.Reports;
using RhyoliteERP.Services.Shared.ReportDownloads;
using RhyoliteERP.Services.Shared.ReportDownloads.Dto;
using RhyoliteERP.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [Abp.AspNetCore.Mvc.Authorization.AbpMvcAuthorize]
    public class SchReportsController : RhyoliteERPControllerBase
    {
        private readonly ISmReportAppService _smReportAppService;
        private readonly IUserAppService _userAppService;
        private readonly IReportDownloadAppService _reportDownloadAppService;


        public SchReportsController(ISmReportAppService smReportAppService, IUserAppService userAppService, IReportDownloadAppService reportDownloadAppService)
        {
            _smReportAppService = smReportAppService;
            _userAppService = userAppService;
            _reportDownloadAppService = reportDownloadAppService;
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

        public IActionResult EnrollmentByGender()
        {
            return View();
        }

        public IActionResult AttendanceSummary()
        {
            return View();
        }

        public IActionResult StudentBalances()
        {
            return View();
        }

        public IActionResult RequestBulkBills()
        {
            return View();
        }

        public IActionResult StudentBill()
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

        public IActionResult PaymentReceipt()
        {
            return View();
        }

        public IActionResult CreditMemo()
        {
            return View();
        }

        public IActionResult BillSetups()
        {
            return View();
        }

        public IActionResult CancelledBills()
        {
            return View();
        }

        public IActionResult CancelledPayments()
        {
            return View();
        }

        
        public IActionResult SubjectResults()
        {
            return View();
        }
        public IActionResult TerminalReport()
        {
            return View();
        }

        
        public IActionResult BulkTerminalReports()
        {
            return View();
        }

        public IActionResult Alumni()
        {
            return View();
        }

        public IActionResult AlumniBalances()
        {
            return View();
        }


        //api

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentsByClass([FromQuery] Guid classId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.StudentsByClass);
            var details = await _smReportAppService.ListStudsByClass(classId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentsByNationality([FromQuery] Guid nationalityId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.StudentsByNationality);
            var details = await _smReportAppService.ListStudsByNationality(nationalityId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentsByReligion([FromQuery] Guid religionId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.StudentsByReligion);
            var details = await _smReportAppService.ListStudsByReligion(religionId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetEnrollmentByGender()
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.EnrollmentByGender);
            var details = await _smReportAppService.ListEnrollmentByGender();

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetAttendanceSummary([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.AttendanceSummary);
            var details = await _smReportAppService.ListAttendanceSummary(academicYearId, termId, classId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentBalances([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.StudentBalances);
            var details = await _smReportAppService.ListStudentBalances(academicYearId, termId, classId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> RequestReportDownload([FromBody] CreateReportDownloadInput dto)
        {
            var currentUser = await _userAppService.GetUser(Convert.ToInt64(AbpSession.UserId));
            dto.RequestedBy = currentUser == null ? "Rhyolite User" : currentUser.FullName;
            dto.Date = DateTime.Now;
            dto.Status = "Pending";
            dto.AccountSource = "erp";
            dto.TenantId = Convert.ToInt32(AbpSession.TenantId);

            await _reportDownloadAppService.Create(dto);

            return Json(200);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPaidUpStudents([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.PaidUpStudents);
            var details = await _smReportAppService.ListPaidupStudents(academicYearId, termId, classId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentDebtors([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.StudentDebtors);
            var details = await _smReportAppService.ListStudentDebtors(academicYearId, termId, classId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetDailyPayments([FromQuery] DateTime paymentDate)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.DailyPayments);
            var details = await _smReportAppService.ListDailyPayments(paymentDate);

            return Json(new { header, details });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetStudentPayments([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {
            var response = await _smReportAppService.ListStudentsPayments(academicYearId, termId, classId, studentId);

            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPaymentReceipt([FromQuery] Guid academicYearId, Guid termId, Guid studentId, Guid classId,string receiptNo)
        {
            var header = await _smReportAppService.ListSchProfile(string.Empty);
            var details = await _smReportAppService.GetReceipt(academicYearId, termId, classId, studentId, receiptNo);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetCreditMemos([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.CreditMemo);
            var details = await _smReportAppService.ListCreditMemos(academicYearId, termId, classId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetBillSetups([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.BillSetup);
            var details = await _smReportAppService.ListBillSetups(academicYearId, termId, classId, billTypeId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetCancelledBills([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.CancelledBills);
            var details = await _smReportAppService.ListCancelledBills(academicYearId, termId, classId, billTypeId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetCancelledPayments([FromQuery] Guid academicYearId, Guid termId, Guid classId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.CancelledPayments);
            var details = await _smReportAppService.ListCancelledPayments(academicYearId, termId, classId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetTerminalSubjectResults([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.TerminalSubjectResults);
            var details = await _smReportAppService.ListTerminalSubjectResults(academicYearId, termId, classId, subjectId, resultTypeId);
            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetTerminalReport([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.TerminalSubjectResults);
            var details = await _smReportAppService.GetTerminalReport(academicYearId, termId, classId, studentId);
            var summary = await _smReportAppService.GetTerminalReportSummary(academicYearId, termId, classId, studentId);

            return Json(new { header, details , summary });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetAlumni([FromQuery] Guid academicYearId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.Alumni);
            var details = await _smReportAppService.ListAllAlumni(academicYearId);

            return Json(new { header, details });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetAlumniBalances([FromQuery] Guid academicYearId)
        {
            var header = await _smReportAppService.ListSchProfile(RhyoliteERPConsts.AlumniBalances);
            var details = await _smReportAppService.ListAlumniBalances(academicYearId);

            return Json(new { header, details });
        }
    }
}
