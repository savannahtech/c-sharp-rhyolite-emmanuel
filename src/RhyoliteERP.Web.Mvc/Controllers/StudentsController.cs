using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.DomainServices.School.Students.Dto;
using RhyoliteERP.Services.School.ResultsUploads;
using RhyoliteERP.Services.School.ResultsUploads.Dto;
using RhyoliteERP.Services.School.StudentAttendances;
using RhyoliteERP.Services.School.StudentAttendances.Dto;
using RhyoliteERP.Services.School.Students;
using RhyoliteERP.Services.School.Students.Dto;
using RhyoliteERP.Services.School.StudentStatements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class StudentsController : RhyoliteERPControllerBase
    {
        private readonly IStudentAppService _studentAppService;
        private readonly IStudentAttendanceAppService _studentAttendanceAppService;
        private readonly IResultsUploadAppService _resultsUploadAppService;
        private readonly IStudentStatementAppService _studentStatementAppService;
        public StudentsController(IStudentAppService studentAppService, IStudentAttendanceAppService studentAttendanceAppService, IResultsUploadAppService resultsUploadAppService, IStudentStatementAppService studentStatementAppService)
        {
            _studentAppService = studentAppService;
            _studentAttendanceAppService = studentAttendanceAppService;
            _resultsUploadAppService = resultsUploadAppService;
            _studentStatementAppService = studentStatementAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrUpdate()
        {
            return View();
        }

        public IActionResult Promotion()
        {
            return View();
        }

        public IActionResult AlumniPromotion()
        {
            return View();
        }

        public IActionResult Attendance()
        {
            return View();
        }

        public IActionResult Results()
        {
            return View();
        }
        

        //api
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetStudents()
        {
            var response = await _studentAppService.ListAll();
            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentInput input)
        {
            var response = await _studentAppService.Create(input);
            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentInput input)
        {
            var response = await _studentAppService.Update(input);
            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetStudentByClass([FromQuery]Guid id)
        {
            var response = await _studentAppService.ListAllByClass(id);
            return Json(response);
        }
        public async Task<IActionResult> Promote([FromBody] StudentPromotion studentPromotion)
        {
            await _studentAppService.Promote(studentPromotion);
            return Json(200);
        }

        public async Task<IActionResult> PromoteAlumni([FromBody] AlumniStudentPromotion studentPromotion)
        {
            await _studentAppService.PromoteAlumni(studentPromotion);
            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetStudentDetails([FromQuery] Guid id)
        {
            var response = await _studentAppService.GetAsync(id);

            return Json(response);
        }

        public async Task<IActionResult> DelStudent([FromQuery] Guid id)
        {
            await _studentAppService.Delete(id);
            return Json(200);
        }

        //

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> CreateStudentAttendance([FromBody] CreateStudentAttendanceInput input)
        {
            await _studentAttendanceAppService.Create(input);
            return Json(200);
        }

        public async Task<IActionResult> DelStudentAttendance([FromQuery] Guid id)
        {
            await _studentAttendanceAppService.Delete(id);
            return Json(200);
        }

        //results
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetStudentResults([FromQuery] Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId)
        {
            var response = await _resultsUploadAppService.ListAll(academicYearId, termId, classId, subjectId, resultTypeId);

            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetStudentStatement([FromQuery] Guid id)
        {
            var response = await _studentStatementAppService.GetStatement(id);
            return Json(response);
        }

        public async Task<ActionResult> CreateStudentResult([FromBody] CreateResultsUploadInput input)
        {
            await _resultsUploadAppService.Create(input);
            return Json(200);

        }

        public async Task<ActionResult> UpdateStudentResult([FromBody] UpdateResultsUploadInput input)
        {
            await _resultsUploadAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelStudentResult([FromQuery] Guid id)
        {
            await _resultsUploadAppService.Delete(id);
            return Json(200);
        }
    }
}
