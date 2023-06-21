using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RhyoliteERP.Controllers;
using RhyoliteERP.DomainServices.School.AcademicYears.Dto;
using RhyoliteERP.Services.School.AcademicYears;
using RhyoliteERP.Services.School.AcademicYears.Dto;
using RhyoliteERP.Services.School.BillSetups;
using RhyoliteERP.Services.School.BillSetups.Dto;
using RhyoliteERP.Services.School.BillTypes;
using RhyoliteERP.Services.School.BillTypes.Dto;
using RhyoliteERP.Services.School.ClassStreams;
using RhyoliteERP.Services.School.ClassStreams.Dto;
using RhyoliteERP.Services.School.FeesDescriptions;
using RhyoliteERP.Services.School.FeesDescriptions.Dto;
using RhyoliteERP.Services.School.Levels;
using RhyoliteERP.Services.School.Levels.Dto;
using RhyoliteERP.Services.School.ResultTypes;
using RhyoliteERP.Services.School.ResultTypes.Dto;
using RhyoliteERP.Services.School.SchClasses;
using RhyoliteERP.Services.School.SchClasses.Dto;
using RhyoliteERP.Services.School.SchoolProfile;
using RhyoliteERP.Services.School.SchoolProfile.Dto;
using RhyoliteERP.Services.School.SpecialDuties;
using RhyoliteERP.Services.School.SpecialDuties.Dto;
using RhyoliteERP.Services.School.StaffDesignations;
using RhyoliteERP.Services.School.StaffDesignations.Dto;
using RhyoliteERP.Services.School.StudentStatuses;
using RhyoliteERP.Services.School.StudentStatuses.Dto;
using RhyoliteERP.Services.School.SubjectRemarks;
using RhyoliteERP.Services.School.SubjectRemarks.Dto;
using RhyoliteERP.Services.School.Subjects;
using RhyoliteERP.Services.School.Subjects.Dto;
using RhyoliteERP.Utils;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SchSetupsController : RhyoliteERPControllerBase
    {
        private readonly IAcademicYearAppService _academicYearAppService;
        private readonly IBillTypeAppService _billTypeAppService;
        private readonly IClassStreamAppService _classStreamAppService;
        private readonly IFeesDescriptionAppService _feesDescriptionAppService;
        private readonly ILevelAppService _levelAppService;
        private readonly IStudentStatusAppService _studentStatusAppService;
        private readonly ISubjectAppService _subjectAppService;
        private readonly ISpecialDutyAppService _specialDutyAppService;
        private readonly IStaffDesignationAppService _staffDesignationAppService;
        private readonly IResultTypeAppService _resultTypeAppService;
        private readonly ISchClassAppService _schClassAppService;
        private readonly IBillSetupAppService _billSetupAppService;
        private readonly ISchoolProfileAppService _schoolProfileAppService;
        private readonly ISubjectRemarkAppService _subjectRemarkAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string _templateStorageBaseUrl;
        public SchSetupsController(IConfiguration configuration,IAcademicYearAppService academicYearAppService, IBillTypeAppService billTypeAppService, IClassStreamAppService classStreamAppService, IFeesDescriptionAppService feesDescriptionAppService, ILevelAppService levelAppService, IStudentStatusAppService studentStatusAppService, ISubjectAppService subjectAppService, ISpecialDutyAppService specialDutyAppService, IStaffDesignationAppService staffDesignationAppService, IResultTypeAppService resultTypeAppService, ISchClassAppService schClassAppService, IBillSetupAppService billSetupAppService, ISchoolProfileAppService schoolProfileAppService, ISubjectRemarkAppService subjectRemarkAppService, IWebHostEnvironment webHostEnvironment)
        {
            _academicYearAppService = academicYearAppService;
            _billTypeAppService = billTypeAppService;
            _classStreamAppService = classStreamAppService;
            _feesDescriptionAppService = feesDescriptionAppService;
            _levelAppService = levelAppService;
            _studentStatusAppService = studentStatusAppService;
            _subjectAppService = subjectAppService;
            _specialDutyAppService = specialDutyAppService;
            _staffDesignationAppService = staffDesignationAppService;
            _resultTypeAppService = resultTypeAppService;
            _schClassAppService = schClassAppService;
            _billSetupAppService = billSetupAppService;
            _schoolProfileAppService = schoolProfileAppService;
            _subjectRemarkAppService = subjectRemarkAppService;
            _webHostEnvironment = webHostEnvironment;
            _templateStorageBaseUrl = configuration["ReportingApi:TemplateStorageBaseUrl"];
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SchoolProfile()
        {
            return View();
        }

        public IActionResult AcademicYears()
        {
            return View();
        }


        public IActionResult BillTypes()
        {
            return View();
        }

        public IActionResult BillSetups()
        {
            return View();
        }

        public IActionResult ClassStreams()
        {
            return View();
        }

        public IActionResult FeesDescriptions()
        {
            return View();
        }

        public IActionResult Levels()
        {
            return View();
        }

        public IActionResult LevelsAndClasses()
        {
            return View();
        }

        public IActionResult StudentStaffStatus()
        {
            return View();
        }

        public IActionResult Subjects()
        {
            return View();
        }


        public IActionResult SubjectRemarks()
        {
            return View();
        }


        public IActionResult SpecialDuties()
        {
            return View();
        }

        public IActionResult StaffDesignations()
        {
            return View();
        }

        public IActionResult ResultsTypes()
        {
            return View();
        }

        public IActionResult BillTemplate()
        {
            return View();
        }

        public IActionResult TerminalReportTemplate()
        {
            return View();
        }

        //api

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetSchoolProfile()
        {
            var result = await _schoolProfileAppService.GetAsync();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> SaveProfile([FromBody] CreateSchoolProfileInput input)
        {
            await _schoolProfileAppService.Create(input);
            return Json(200);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetAcaYrs()
        {
            var result = await _academicYearAppService.ListAll();
            return Json(result);
        }
         

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateAcaYear([FromBody] CreateAcademicYearInput input)
        {
            var result = await _academicYearAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateAcaYr([FromBody] UpdateAcademicYearInput input)
        {
            await _academicYearAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelAcaYr([FromQuery] Guid id)
        {
            await _academicYearAppService.Delete(id);
            return Json(200);

        }

        //terms
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateTerm([FromBody] TermInput input)
        {
           var result =  await _academicYearAppService.CreateTerm(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateTerm([FromBody] TermInput input)
        {
            await _academicYearAppService.UpdateTerm(input);
            return Json(200);

        }

        public async Task<ActionResult> DeleteTerm([FromBody] TermInput input)
        {
            await _academicYearAppService.DeleteTerm(input);
            return Json(200);
        }

        //bill types 

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetBillTypes()
        {
            var result = await _billTypeAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateBillType([FromBody] CreateBillTypeInput input)
        {
            var result = await _billTypeAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateBillType([FromBody] UpdateBillTypeInput input)
        {
            await _billTypeAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelBillType([FromQuery] Guid id)
        {
            await _billTypeAppService.Delete(id);
            return Json(200);
        }

        // class streams
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetClassStreams()
        {
            var result = await _classStreamAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateClassStream([FromBody] CreateClassStreamInput input)
        {
            var result = await _classStreamAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateClassStream([FromBody] UpdateClassStreamInput input)
        {
            await _classStreamAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelClassStream([FromQuery] Guid id)
        {
            await _classStreamAppService.Delete(id);
            return Json(200);
        }

        // fees descriptions
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetFeesDescriptions([FromQuery]Guid id)
        {
            var result = await _feesDescriptionAppService.ListAll(id);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateFeesDescription([FromBody] CreateFeesDescriptionInput input)
        {
            var result = await _feesDescriptionAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateFeesDescription([FromBody] UpdateFeesDescriptionInput input)
        {
            await _feesDescriptionAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelFeesDescription([FromQuery] Guid id)
        {
            await _feesDescriptionAppService.Delete(id);
            return Json(200);
        }

        // levels
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetLevels()
        {
            var result = await _levelAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateLevel([FromBody] CreateLevelInput input)
        {
            var result = await _levelAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateLevel([FromBody] UpdateLevelInput input)
        {
            await _levelAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelLevel([FromQuery] Guid id)
        {
            await _levelAppService.Delete(id);
            return Json(200);
        }

        // student status
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetStatus()
        {
            var result = await _studentStatusAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateStatus([FromBody] CreateStudentStatusInput input)
        {
            var result = await _studentStatusAppService.Create(input);
            return Json(result);

        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> UpdateStatus([FromBody] UpdateStudentStatusInput input)
        {
           var result = await _studentStatusAppService.Update(input);
            return Json(result);

        }

        public async Task<ActionResult> DelStatus([FromQuery] Guid id)
        {
            await _studentStatusAppService.Delete(id);
            return Json(200);
        }

        //subjects
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetSubjects()
        {
            var result = await _subjectAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateSubject([FromBody] CreateSubjectInput input)
        {
            var result = await _subjectAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateSubject([FromBody] UpdateSubjectInput input)
        {
            await _subjectAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelSubject([FromQuery] Guid id)
        {
            await _subjectAppService.Delete(id);
            return Json(200);
        }

        //special duties
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetSpecialDuties()
        {
            var result = await _specialDutyAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateSpecialDuty([FromBody] CreateSpecialDutyInput input)
        {
            var result = await _specialDutyAppService.Create(input);
            return Json(result);

        }

        public async Task<ActionResult> UpdateSpecialDuty([FromBody] UpdateSpecialDutyInput input)
        {
            await _specialDutyAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelSpecialDuty([FromQuery] Guid id)
        {
            await _specialDutyAppService.Delete(id);
            return Json(200);
        }

        //staff designations
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetStaffDesignations()
        {
            var result = await _staffDesignationAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateStaffDesignation([FromBody] CreateStaffDesignationInput input)
        {
            var result = await _staffDesignationAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateStaffDesignation([FromBody] UpdateStaffDesignationInput input)
        {
            await _staffDesignationAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelStaffDesignation([FromQuery] Guid id)
        {
            await _staffDesignationAppService.Delete(id);
            return Json(200);
        }

        //results type
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetResultTypes([FromQuery] Guid id)
        {
            var result = await _resultTypeAppService.ListAll(id);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetResultTypesByClass([FromQuery] Guid id)
        {
            var result = await _resultTypeAppService.ListByClass(id);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateResultType([FromBody] CreateResultTypeInput input)
        {
            var result = await _resultTypeAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateResultType([FromBody] UpdateResultTypeInput input)
        {
            await _resultTypeAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelResultType([FromQuery] Guid id)
        {
            await _resultTypeAppService.Delete(id);
            return Json(200);
        }

        //classes
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetClassesByLevel([FromQuery] Guid id)
        {
            var classes = await _schClassAppService.ListAll(id);
            var streams = await _classStreamAppService.ListAll();
            return Json(new { classes, streams });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetClasses()
        {
            var result = await _schClassAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateClass([FromBody] CreateClassInput input)
        {
            var result = await _schClassAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateClass([FromBody] UpdateClassInput input)
        {
            await _schClassAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelClass([FromQuery] Guid id)
        {
            await _schClassAppService.Delete(id);
            return Json(200);
        }

        //bill setups
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetBillSetups([FromQuery]Guid academicYearId,Guid termId, Guid classId, Guid billTypeId)
        {
            var result = await _billSetupAppService.GetAsync(academicYearId, termId, classId, billTypeId);

            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> CreateBillSetup([FromBody] CreateBillSetupInput input)
        {
            var result = await _billSetupAppService.Create(input);
            return Json(result);
        }

        public async Task<JsonResult> DelBillSetup([FromBody] Guid id)
        {
            await _billSetupAppService.Delete(id);
            return Json(200);

        }

        //bill setup details

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> UpdateBillSetupDetail([FromBody] CreateBillSetupInput input)
        {
            var result = await _billSetupAppService.Create(input);
            return Json(result);
        }

        public async Task<JsonResult> DelBillSetupDetail([FromQuery] Guid id,Guid headerId)
        {
            await _billSetupAppService.DeleteDetail(id, headerId);
            return Json(200);

        }

        //subject remarks
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetSubjectRemarks()
        {
            var result = await _subjectRemarkAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateSubjectRemark([FromBody] CreateSubjectRemarkInput input)
        {
            await _subjectRemarkAppService.Create(input);
            return Json(200);

        }

        public async Task<ActionResult> UpdateSubjectRemark([FromBody] UpdateSubjectRemarkInput input)
        {
            await _subjectRemarkAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelSubjectRemark([FromQuery] Guid id)
        {
            await _subjectRemarkAppService.Delete(id);
            return Json(200);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetBillTemplate()
        {
            var response =  await _schoolProfileAppService.GetAsync();
            if (response != null) {

                response.BillTemplateFileUrl = $"{_templateStorageBaseUrl}{response.BillTemplateFileUrl}";
                return Json(response);

            }
            return Json(response);
        }
        

        [HttpPost]
        public async Task<IActionResult> UploadReportTemplate(IFormFile htmlFile, string reportType)
        {
            var wwwRoot = _webHostEnvironment.WebRootPath;

            if (htmlFile != null)
            {
                string extension = Path.GetExtension(htmlFile.FileName);

                var absoluteFilePath = $"{wwwRoot}/report-templates/{Guid.NewGuid():N}{extension}";

                var filePath = $"{Guid.NewGuid():N}{extension}";

                var fileBytes = await new UtilService().GetBytes(htmlFile.OpenReadStream());

                await System.IO.File.WriteAllBytesAsync(absoluteFilePath, fileBytes);

                await _schoolProfileAppService.CreateReportTemplate(filePath, reportType);

            }


            return View("BillTemplate");
        }


    }
}
