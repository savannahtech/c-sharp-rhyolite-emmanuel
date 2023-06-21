using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RhyoliteERP.Controllers;
using RhyoliteERP.RabbitMq;
using RhyoliteERP.Services.School.Attitudes;
using RhyoliteERP.Services.School.Attitudes.Dto;
using RhyoliteERP.Services.School.Conducts;
using RhyoliteERP.Services.School.Conducts.Dto;
using RhyoliteERP.Services.School.ResultsUploads;
using RhyoliteERP.Services.School.ResultsUploads.Dto;
using RhyoliteERP.Services.School.SchoolProfile;
using RhyoliteERP.Services.School.StudentBills;
using RhyoliteERP.Services.School.StudentBills.Dto;
using RhyoliteERP.Services.School.Students;
using RhyoliteERP.Services.School.Students.Dto;
using RhyoliteERP.Services.School.TeacherRemarks;
using RhyoliteERP.Services.School.TeacherRemarks.Dto;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SchImportDataController : RhyoliteERPControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IStudentAppService _studentAppService;
        private readonly IResultsUploadAppService _resultsUploadAppService;
        private readonly IAttitudeAppService _attitudeAppService;
        private readonly IConductAppService _conductAppService;
        private readonly ITeacherRemarkAppService _teacherRemarkAppService;
        private readonly IStudentBillAppService _studentBillAppService;
        private readonly ISchoolProfileAppService _schoolProfileAppService;
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly IConfiguration _configuration;
        public SchImportDataController(IWebHostEnvironment env, IStudentAppService studentAppService, IResultsUploadAppService resultsUploadAppService, IConductAppService conductAppService, IAttitudeAppService attitudeAppService, ITeacherRemarkAppService teacherRemarkAppService, IStudentBillAppService studentBillAppService, ISchoolProfileAppService schoolProfileAppService, IRabbitMqClient rabbitMqClient, IConfiguration configuration)
        {
            _env = env;
            _studentAppService = studentAppService;
            _resultsUploadAppService = resultsUploadAppService;
            _conductAppService = conductAppService;
            _attitudeAppService = attitudeAppService;
            _teacherRemarkAppService = teacherRemarkAppService;
            _studentBillAppService = studentBillAppService;
            _schoolProfileAppService = schoolProfileAppService;
            _rabbitMqClient = rabbitMqClient;
            _configuration = configuration;
        }

        public IActionResult Students()
        {
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> UploadStudentsData(IFormFile customFile, Guid classId)
        {
            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable exceltable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < exceltable.Rows.Count; i++)
            {
                var row = exceltable.Rows[i];
                //search for students in student table and return pk
                var dobRow = row["Date Of Birth"].ToString() ?? string.Empty;

                var input = new CreateStudentInput
                {
                    ClassId = classId,
                    FirstName = row["First Name"].ToString(),
                    LastName = row["Last Name"].ToString(),
                    MiddleName = row["Middle Name"].ToString(),
                    DateOfBirth = string.IsNullOrEmpty(dobRow) ? DateTime.UtcNow.AddYears(-5) : DateTime.Parse(dobRow),
                    EnrollmentDate = DateTime.Now,
                    Gender = row["Gender"].ToString(),
                    HomeAddress = "--",
                    CityOrLocation = "--",
                    StudentIdentifier = row["Student ID"].ToString(),
                    StudImage = "https://res.cloudinary.com/rhyoliteprime/image/upload/v1533814738/images_6.png",
                    StudentStatusId = Guid.Empty,
                    ReligionId = Guid.Empty,
                    AcademicYearId = Guid.Empty, //can be obtained from current academic year.
                    NationalityId = Guid.Empty,
                    EnrollmentType = "Day"

                };

                await _studentAppService.Create(input);

            }


            return View("Students", exceltable);
        }

        public async Task<IActionResult> DownloadStudentsUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "Students_Sample.xlsx"));
            return File(fileBytes, "application/ms-excel", "Students_Sample.xlsx");

        }
       
        public IActionResult Staff()
        {
            return View();
        }

        public IActionResult Results()
        {
            dynamic viewmodel = new ExpandoObject();
            viewmodel.ErrorList = null;
            viewmodel.Exceltable = null;
            return View(viewmodel);
        }

        public async Task<IActionResult> UploadResults(IFormFile customFile, Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId, decimal totalMarks)
        {
            dynamic viewmodel = new ExpandoObject();
             
            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable exceltable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);
            //List<ResultsUploadError> errorList = new List<ResultsUploadError>();

            for (int i = 0; i < exceltable.Rows.Count; i++)
            {
                var row = exceltable.Rows[i];

                var studentID = row["StudentID"].ToString();

                Guid studId = await _studentAppService.GetStudentId(studentID.Trim());
 
                var input = new CreateResultsUploadInput
                {
                    AcademicYearId = academicYearId,
                    TermId = termId,
                    ClassId = classId,
                    StudentId = studId,
                    SubjectId = subjectId,
                    ResultTypeId = resultTypeId,
                    TotalMarks = totalMarks,
                    MarksObtained = Convert.ToDecimal(row["Marks"].ToString()),

                };

                await _resultsUploadAppService.Create(input);

                
            }
             
          
            viewmodel.Exceltable = exceltable;

            _rabbitMqClient.Produce(_configuration["RabbitMqBroker:ExchangeTypes:AmqTopic"], _configuration["RabbitMqBroker:ReportDownloadQueue"],
                  new
                  {
                      TenantId = Convert.ToInt32(AbpSession.TenantId),
                      Name = "Terminal Report Preprocessing",
                      ReportKey = "terminal-report-preprocessing",
                      ReportOptions = JsonConvert.SerializeObject(new { AcademicYearId = academicYearId, TermId = termId, ClassId = classId }),
                      Date = DateTime.UtcNow,
                      RequestedBy = "",
                      Status = "Pending",
                      AccountSource = "erp",

                  });

            return View("Results", viewmodel);

        }

        public async Task<IActionResult> DownloadResultSample([FromQuery] Guid classId)
        {
            var wwwRoot = _env.WebRootPath;

            if (classId != Guid.Empty)
            {
                string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"ResultsUpload_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2016;
                    IWorkbook workbook = application.Workbooks.Create(1);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Import the data to worksheet
                    var students = await _studentAppService.ResultsUploadExcelExport(classId);
                    worksheet.ImportData(students, 1, 1, true);

                    //Saving the workbook as stream
                    FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                    workbook.SaveAs(stream);
                    await stream.DisposeAsync();
                }

                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, "application/ms-excel", "ResultsUpload_Sample.xlsx");

            }
            else
            {
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "ResultsUpload_Sample.xlsx"));
                return File(fileBytes, "application/ms-excel", "ResultsUpload_Sample.xlsx");
            }



        }

        public IActionResult TeacherRemarks()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadTeacherRemarksData(IFormFile customFile, Guid academicYearId, Guid termId, Guid classId)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable exceltable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < exceltable.Rows.Count; i++)
            {
                var row = exceltable.Rows[i];
                //search for students in student table and return pk
                Guid studId = await _studentAppService.GetStudentId(row["StudentID"].ToString());

                var input = new CreateTeacherRemarkInput
                {
                    AcademicYearId = academicYearId,
                    TermId = termId,
                    ClassId = classId,
                    StudentId = studId,
                    Remarks = row["Remarks"].ToString(),

                };

                await _teacherRemarkAppService.Create(input);

            }

            return View("TeacherRemarks", exceltable);
        }


        public async Task<IActionResult> DownloadTeacherRemarksSample([FromQuery] Guid classId)
        {
            var wwwRoot = _env.WebRootPath;

            if (classId != Guid.Empty)
            {
                string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"TeacherRemarks_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2016;
                    IWorkbook workbook = application.Workbooks.Create(1);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Import the data to worksheet
                    var students = await _studentAppService.TeacherRemarksExcelExport(classId);
                    worksheet.ImportData(students, 1, 1, true);

                    //Saving the workbook as stream
                    FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                    workbook.SaveAs(stream);
                    await stream.DisposeAsync();
                }

                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, "application/ms-excel", "TeacherRemarks_Sample.xlsx");

            }
            else
            {
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "TeacherRemarks_Sample.xlsx"));
                return File(fileBytes, "application/ms-excel", "TeacherRemarks_Sample.xlsx");
            }



        }

        public IActionResult Attitude()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttitudesData(IFormFile customFile, Guid academicYearId, Guid termId, Guid classId)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable exceltable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < exceltable.Rows.Count; i++)
            {
                var row = exceltable.Rows[i];
                //search for students in student table and return pk.
                Guid studId = await _studentAppService.GetStudentId(row["StudentID"].ToString());

                if (studId != Guid.Empty)
                {
                    var input = new CreateAttitudeInput
                    {
                        AcademicYearId = academicYearId,
                        TermId = termId,
                        ClassId = classId,
                        StudentId = studId,
                        AttitudeText = row["Attitude"].ToString(),

                    };
                    await _attitudeAppService.Create(input);

                }


            }

            return View("Attitude", exceltable);
        }

        public async Task<IActionResult> DownloadAttitudesUploadSample([FromQuery] Guid classId)
        {
            var wwwRoot = _env.WebRootPath;

            if (classId != Guid.Empty)
            {
                string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"Attitudes_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2016;
                    IWorkbook workbook = application.Workbooks.Create(1);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Import the data to worksheet
                    var students = await _studentAppService.AttitudeExcelExport(classId);
                    worksheet.ImportData(students, 1, 1, true);

                    //Saving the workbook as stream
                    FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                    workbook.SaveAs(stream);
                    await stream.DisposeAsync();
                }

                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, "application/ms-excel", "Attitudes_Sample.xlsx");

            }
            else
            {
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "Attitudes_Sample.xlsx"));
                return File(fileBytes, "application/ms-excel", "Attitudes_Sample.xlsx");
            }

        }

        public IActionResult Conducts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadConductsData(IFormFile customFile, Guid academicYearId, Guid termId, Guid classId)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable exceltable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < exceltable.Rows.Count; i++)
            {
                var row = exceltable.Rows[i];
                //search for students in student table and return pk
                Guid studId = await _studentAppService.GetStudentId(row["StudentID"].ToString());

                if (studId != Guid.Empty)
                {
                    var input = new CreateConductInput
                    {
                        AcademicYearId = academicYearId,
                        TermId = termId,
                        ClassId = classId,
                        StudentId = studId,
                        ConductText = row["Conduct"].ToString(),

                    };
                    await _conductAppService.Create(input);

                }

            }


            return View("Index", exceltable);
        }

        public async Task<IActionResult> DownloadConductsUploadSample([FromQuery] Guid classId)
        {
            var wwwRoot = _env.WebRootPath;

            if (classId != Guid.Empty)
            {
                string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"Conducts_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2016;
                    IWorkbook workbook = application.Workbooks.Create(1);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Import the data to worksheet
                    var students = await _studentAppService.ConductsExcelExport(classId);
                    worksheet.ImportData(students, 1, 1, true);

                    //Saving the workbook as stream
                    FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                    workbook.SaveAs(stream);
                    await stream.DisposeAsync();
                }

                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, "application/ms-excel", "Conducts_Sample.xlsx");

            }
            else
            {
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "Conducts_Sample.xlsx"));
                return File(fileBytes, "application/ms-excel", "Conducts_Sample.xlsx");
            }



        }

        public IActionResult OpeningBalances()
        {
            return View();
        }


        public async Task<IActionResult> DownloadOpeningBalancesUploadSample([FromQuery] Guid classId)
        {
            var wwwRoot = _env.WebRootPath;

            if (classId != Guid.Empty)
            {
                string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"OpeningBalance_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2016;
                    IWorkbook workbook = application.Workbooks.Create(1);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Import the data to worksheet
                    var students = await _studentAppService.OpeningBalanceExcelExport(classId);
                    worksheet.ImportData(students, 1, 1, true);

                    //Saving the workbook as stream
                    FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                    workbook.SaveAs(stream);
                    await stream.DisposeAsync();
                }

                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, "application/ms-excel", "OpeningBalance_Sample.xlsx");

            }
            else
            {
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "OpeningBalance_Sample.xlsx"));
                return File(fileBytes, "application/ms-excel", "OpeningBalance_Sample.xlsx");
            }



        }

        [HttpPost]
        public async Task<IActionResult> UploadOpeningBalancesData(IFormFile customFile, Guid classId)
        {

            //get current academic year
            var schoolProfile = await _schoolProfileAppService.GetAsync();

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable exceltable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < exceltable.Rows.Count; i++)
            {
                var row = exceltable.Rows[i];
                //search for students in student table and return pk
                Guid studId = await _studentAppService.GetStudentId(row["StudentID"].ToString());
                var billAmount = Convert.ToDecimal(row["Amount"].ToString());

                if (studId != Guid.Empty && billAmount > 0)
                {
                    var input = new CreateStudentBillInput
                    {
                        AcademicYearId = schoolProfile != null ? schoolProfile.CurrentAcademicYearId : Guid.Empty,
                        TermId = schoolProfile != null ? schoolProfile.CurrentTermId : Guid.Empty,
                        ClassId = classId,
                        StudentId = studId,
                        BillAmount = billAmount,
                        BillBalance = Convert.ToDecimal(row["Amount"].ToString()),
                        BillDate = DateTime.UtcNow,
                        BillNo = new Random().Next(10000,9999999).ToString(),
                        BillSetupId = Guid.NewGuid(),
                        BillSetupInfo = null,
                        BillStatus = 401,
                        BillTypeId = Guid.NewGuid(),

                    };

                    await _studentBillAppService.CreateOpeningBalance(input);

                }

            }


            return View("OpeningBalances", exceltable);
        }

    }
}
