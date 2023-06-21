using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using Syncfusion.XlsIO;
using System.Data;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using RhyoliteERP.Services.Payroll.AllowanceTypes;
using RhyoliteERP.Services.Payroll.DeductionTypes;
using RhyoliteERP.Services.Payroll.BikTypes;
using RhyoliteERP.Services.Payroll.EmployeeCategories;
using RhyoliteERP.Services.Payroll.EmployeeBioDatas;
using RhyoliteERP.Services.Payroll.EmployeeAllowances;
using System.Collections.Generic;
using RhyoliteERP.Services.Payroll.EmployeeAllowances.Dto;
using RhyoliteERP.Services.Payroll.LoanTypes;
using RhyoliteERP.Services.Payroll.EmployeeSalaryInfos.Dto;
using RhyoliteERP.Services.Payroll.EmployeeSalaryInfos;
using RhyoliteERP.Services.Payroll.EmployeeSnits.Dto;
using RhyoliteERP.Services.Payroll.EmployeeSnits;
using RhyoliteERP.Models.Payroll;
using Castle.MicroKernel.Registration;
using RhyoliteERP.Services.Payroll.EmployeeDeductions.Dto;
using RhyoliteERP.Services.Payroll.EmployeeDeductions;

namespace RhyoliteERP.Web.Controllers
{
    [Abp.AspNetCore.Mvc.Authorization.AbpMvcAuthorize]
    public class PayrollImportDataController : RhyoliteERPControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IAllowanceTypeAppService _allowanceTypeAppService;
        private readonly IDeductionTypeAppService _deductionTypeAppService;
        private readonly IBikTypeAppService _bikTypeAppService;
        private readonly IEmployeeCategoryAppService _employeeCategoryAppService;
        private readonly IEmployeeBioDataAppService _employeeBioDataAppService;
        private readonly IEmployeeAllowanceAppService _employeeAllowanceAppService;
        private readonly ILoanTypeAppService _loanTypeAppService;
        private readonly IEmployeeSalaryInfoAppService _employeeSalaryInfoAppService;
        private readonly IEmployeeSnitAppService _employeeSnitAppService;
        private readonly IEmployeeDeductionAppService _employeeDeductionAppService;
        public PayrollImportDataController(IWebHostEnvironment env, IAllowanceTypeAppService allowanceTypeAppService, IDeductionTypeAppService deductionTypeAppService, IBikTypeAppService bikTypeAppService, IEmployeeCategoryAppService employeeCategoryAppService, IEmployeeBioDataAppService employeeBioDataAppService, IEmployeeAllowanceAppService employeeAllowanceAppService, ILoanTypeAppService loanTypeAppService, IEmployeeSalaryInfoAppService employeeSalaryInfoAppService, IEmployeeSnitAppService employeeSnitAppService, IEmployeeDeductionAppService employeeDeductionAppService)
        {
            _env = env;
            _allowanceTypeAppService = allowanceTypeAppService;
            _deductionTypeAppService = deductionTypeAppService;
            _bikTypeAppService = bikTypeAppService;
            _employeeCategoryAppService = employeeCategoryAppService;
            _employeeBioDataAppService = employeeBioDataAppService;
            _employeeAllowanceAppService = employeeAllowanceAppService;
            _loanTypeAppService = loanTypeAppService;
            _employeeSalaryInfoAppService = employeeSalaryInfoAppService;
            _employeeSnitAppService = employeeSnitAppService;
            _employeeDeductionAppService = employeeDeductionAppService;
        }

        public IActionResult AllowancesTypes()
        {
            return View();
        }

        public IActionResult DeductionTypes()
        {
            return View();
        }

        public IActionResult BenefitsInKindTypes()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View();
        }

        public IActionResult EmployeeAllowances()
        {
            return View();
        }
        public IActionResult EmployeeDeductions()
        {
            return View();
        }

        public IActionResult EmployeeCategories()
        {
            return View();
        }

        public IActionResult LoanTypes()
        {
            return View();
        }

        public IActionResult SalaryInfo()
        {
            return View();
        }

        public IActionResult Ssnit()
        {
            return View();
        }

        //api
        [HttpPost]
        public async Task<IActionResult> UploadAllowanceTypes(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                var row = excelTable.Rows[i];

                await _allowanceTypeAppService.Create(new Services.Payroll.AllowanceTypes.Dto.CreateAllowanceTypeInput
                {
                    Name= row.Table.Columns.Contains("Name") ? row["Name"].ToString().Trim() : string.Empty,
                    ExpenseAccountId = Guid.Empty,
                    AllowanceDays = 0, 
                    Taxable = row.Table.Columns.Contains("Is Taxable") && row["Is Taxable"].ToString().Trim() == "Yes", 
                    ExpenseAccountName = string.Empty
                });

            }

            return View("AllowancesTypes", excelTable);
        }

        public async Task<IActionResult> DownloadAllowanceTypesUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "AllowanceTypes_Sample.xlsx"));
            return File(fileBytes, "application/ms-excel", "AllowanceTypes_Sample.xlsx");

        }


        [HttpPost]
        public async Task<IActionResult> UploadDeductionTypes(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {

                var row = excelTable.Rows[i];

                await _deductionTypeAppService.Create(new Services.Payroll.DeductionTypes.Dto.CreateDeductionTypeInput
                {
                    Name = row.Table.Columns.Contains("Name") ? row["Name"].ToString().Trim() : string.Empty,
                    AccountId = Guid.Empty
                });

            }

            return View("DeductionTypes", excelTable);
        }


        public async Task<IActionResult> DownloadDeductionTypesUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "DeductionTypes_Sample.xlsx"));
            return File(fileBytes, "application/ms-excel", "DeductionTypes_Sample.xlsx");

        }

        [HttpPost]
        public async Task<IActionResult> UploadBenefitsInKindTypes(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                var row = excelTable.Rows[i];

                await _bikTypeAppService.Create(new Services.Payroll.BikTypes.Dto.CreateBikTypeInput
                {
                    Name = row.Table.Columns.Contains("Name") ? row["Name"].ToString().Trim() : string.Empty,
                    ExpenseAccountId = Guid.Empty, 
                    Taxable = row.Table.Columns.Contains("Is Taxable") && row["Is Taxable"].ToString().Trim() == "Yes", 

                });

            }

            return View("BenefitsInKindTypes", excelTable);
        }


        public async Task<IActionResult> DownloadBenefitsInKindTypesUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "BenefitInKindTypes_Sample.xlsx"));
            return File(fileBytes, "application/ms-excel", "BenefitInKindTypes_Sample.xlsx");

        }

        [HttpPost]
        public async Task<IActionResult> UploadEmployeeCategories(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                var row = excelTable.Rows[i];

                await _employeeCategoryAppService.Create(new Services.Payroll.EmployeeCategories.Dto.CreateEmployeeCategoryInput
                {
                    Name = row.Table.Columns.Contains("Name") ? row["Name"].ToString().Trim() : string.Empty,

                });

            }

            return View("EmployeeCategories", excelTable);
        }

        public async Task<IActionResult> DownloadEmployeeCategoriesUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "EmployeeCategories_Sample.xlsx"));
            return File(fileBytes, "application/ms-excel", "EmployeeCategories_Sample.xlsx");

        }

        [HttpPost]
        public async Task<IActionResult> UploadEmployees(IFormFile customFile)
        {

            try
            {
                //read data from excel file
                ExcelEngine excelEngine = new ExcelEngine();
                //Loads or open an existing workbook through Open method of IWorkbooks
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
                IWorksheet worksheet = workbook.Worksheets[0];

                // Read data from the worksheet and Export to the DataTable.

                DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    var row = excelTable.Rows[i];

                    await _employeeBioDataAppService.Create(new Services.Payroll.EmployeeBioDatas.Dto.CreateEmployeeBioDataInput
                    {
                        FirstName = row.Table.Columns.Contains("First Name") ? row["First Name"].ToString().Trim() : string.Empty,
                        LastName = row.Table.Columns.Contains("Last Name") ? row["Last Name"].ToString().Trim() : string.Empty,
                        OtherName = row.Table.Columns.Contains("Other Name") ? row["Other Name"].ToString().Trim() : string.Empty,
                        EmployeeIdentifier = row.Table.Columns.Contains("Employee ID") ? row["Employee ID"].ToString().Trim() : string.Empty,
                        PrimaryPhoneNumber = row.Table.Columns.Contains("Phone No") ? row["Phone No"].ToString().Trim() : string.Empty,
                        PersonalEmail = row.Table.Columns.Contains("Email") ? row["Email"].ToString().Trim() : string.Empty,
                        Gender = row.Table.Columns.Contains("Gender") ? row["Gender"].ToString().Trim() : string.Empty,
                        CategoryId = Guid.Empty,
                        CategoryName = string.Empty,
                        CityOrLocation = string.Empty,
                        CompanyEmail = string.Empty,
                        DateAppointed = DateTime.UtcNow,
                        DateOfBirth = DateTime.UtcNow,
                        DepartmentId = Guid.Empty,
                        DepartmentName = string.Empty,
                        DriverLicenseNo = string.Empty,
                        EmployeePhoto = string.Empty,
                        HealthIssues = string.Empty,
                        Height = 0,
                        Hometown = string.Empty,
                        Interests = string.Empty,
                        Languages = string.Empty,
                        LeaveDaysEntitled = 0,
                        NationalityId = Guid.Empty,
                        NationalityName = string.Empty,
                        LicenseExpiryDate = string.Empty,
                        LicenseIssueDate = string.Empty,
                        MaritalStatus = string.Empty,
                        MedicalExpensesLimit = 0,
                        NationalHealthInsuranceNo = string.Empty,
                        NationalID = string.Empty,
                        NationalIdentificationNo = string.Empty,
                        PassportIssueDate = string.Empty,
                        PassportExpiryDate = string.Empty,
                        PassportNo = string.Empty,
                        ReligionId = Guid.Empty,
                        ReligionName = string.Empty,
                        ResidenceAddress = string.Empty,
                        SalaryGradeId = Guid.Empty,
                        SalaryGradeName = string.Empty,
                        SalaryNotchId = Guid.Empty,
                        SalaryNotchName = string.Empty,
                        StatusId = Guid.Empty,
                        SecondaryPhoneNumber = string.Empty,
                        StatusName = string.Empty,
                        TaxIdentificationNo = string.Empty,
                        UserId = 0,
                        VotersIDNo = string.Empty,
                        Weight = 0,

                    });

                }

                return View("Employees", excelTable);

            }
            catch (Exception ex)
            {

                Logger.Info($"{ex.StackTrace}\n{ex.Message}");
                return View("Employees", null);

            }


        }

        public async Task<IActionResult> DownloadEmployeesUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "Employees_Sample.xlsx"));
            return File(fileBytes, "application/ms-excel", "Employees_Sample.xlsx");

        }


        [HttpPost]
        public async Task<IActionResult> UploadEmployeeAllowances(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            List<ImportEmployeeAllowanceInput> employeeAllowanceList = new List<ImportEmployeeAllowanceInput>();

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                var row = excelTable.Rows[i];

                var employeeAllowance = new ImportEmployeeAllowanceInput
                {
                    EmployeeIdentifier = row.Table.Columns.Contains("Employee ID") ? row["Employee ID"].ToString().Trim() : string.Empty, 
                    AllowanceDays = row.Table.Columns.Contains("Allowance Days") ? Convert.ToInt32(row["Allowance Days"].ToString().Trim()) : 0,
                    AllowanceTypeName = row.Table.Columns.Contains("Allowance Type") ? row["Allowance Type"].ToString().Trim() : string.Empty,
                    Amount = row.Table.Columns.Contains("Amount") ? Convert.ToDecimal(row["Amount"].ToString().Trim()) : 0, 
                    Taxable = row.Table.Columns.Contains("Is Taxable") && row["Is Taxable"].ToString().Trim() == "Yes",
                };

                employeeAllowanceList.Add(employeeAllowance);
               
            }

            await _employeeAllowanceAppService.Import(employeeAllowanceList);

            return View("EmployeeAllowances", excelTable);
        }


        public async Task<IActionResult> DownloadEmployeeAllowancesUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"EmployeeAllowances_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Import the data to worksheet
                var results = await _employeeBioDataAppService.ExportAllowances();
                worksheet.ImportData(results, 1, 1, true);

                //Saving the workbook as stream
                FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                workbook.SaveAs(stream);
                await stream.DisposeAsync();
            }


            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(fileBytes, "application/ms-excel", "EmployeeAllowances_Sample.xlsx");

        }


        [HttpPost]
        public async Task<IActionResult> UploadEmployeeDeductions(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            List<ImportEmployeeDeductionInput> employeeDeductionList = new List<ImportEmployeeDeductionInput>();

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                var row = excelTable.Rows[i];

                var employeeDeduction = new ImportEmployeeDeductionInput
                {
                    EmployeeIdentifier = row.Table.Columns.Contains("Employee ID") ? row["Employee ID"].ToString().Trim() : string.Empty,
                    EmployerAmount = row.Table.Columns.Contains("Employer Amount") ? Convert.ToDecimal(row["Employer Amount"].ToString().Trim()) : 0,
                    Amount = row.Table.Columns.Contains("Amount") ? Convert.ToDecimal(row["Amount"].ToString().Trim()) : 0,
                    DeductionTypeName = row.Table.Columns.Contains("Deduction Type") ? row["Deduction Type"].ToString().Trim() : string.Empty,
                };

                employeeDeductionList.Add(employeeDeduction);

            }

            await _employeeDeductionAppService.Import(employeeDeductionList);

            return View("EmployeeDeductions", excelTable);
        }

        public async Task<IActionResult> DownloadEmployeeDeductionsUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"EmployeeDeductions_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Import the data to worksheet
                var results = await _employeeBioDataAppService.ExportDeductions();
                worksheet.ImportData(results, 1, 1, true);

                //Saving the workbook as stream
                FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                workbook.SaveAs(stream);
                await stream.DisposeAsync();
            }

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(fileBytes, "application/ms-excel", "EmployeeDeductions_Sample.xlsx");

        }

        [HttpPost]
        public async Task<IActionResult> UploadLoanTypes(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                var row = excelTable.Rows[i];

                await _loanTypeAppService.Create(new Services.Payroll.LoanTypes.Dto.CreateLoanTypeInput
                {
                    Name = row.Table.Columns.Contains("Name") ? row["Name"].ToString().Trim() : string.Empty,

                });

            }

            return View("LoanTypes", excelTable);
        }


        public async Task<IActionResult> DownloadLoanTypesUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRoot + "/DataSample/", "LoanTypes_Sample.xlsx"));
            return File(fileBytes, "application/ms-excel", "LoanTypes_Sample.xlsx");

        }

        [HttpPost]
        public async Task<IActionResult> UploadSalaryInfo(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            List<ImportEmployeeSalaryInfoInput> employeeSalaryInfoList = new List<ImportEmployeeSalaryInfoInput>();

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {

                var row = excelTable.Rows[i];

                var employeeSalaryInfo = new ImportEmployeeSalaryInfoInput
                {
                    EmployeeIdentifier = row.Table.Columns.Contains("Employee ID") ? row["Employee ID"].ToString().Trim() : string.Empty,
                    SalaryType = row.Table.Columns.Contains("Salary Type") ? row["Salary Type"].ToString().Trim() : string.Empty,
                    PayType = row.Table.Columns.Contains("Pay Type") ? row["Pay Type"].ToString().Trim() : string.Empty,
                    BankName = row.Table.Columns.Contains("Bank Name") ? row["Bank Name"].ToString().Trim() : string.Empty, 
                    AccountNumber = row.Table.Columns.Contains("Account Number") ? row["Account Number"].ToString().Trim() : string.Empty,
                    CurrencyName = row.Table.Columns.Contains("Currency") ? row["Currency"].ToString().Trim() : string.Empty,
                    MonthlySalary = row.Table.Columns.Contains("Monthly Salary") ? Convert.ToDecimal(row["Monthly Salary"].ToString().Trim()) : 0,

                };

                employeeSalaryInfoList.Add(employeeSalaryInfo);

            }

            await _employeeSalaryInfoAppService.Import(employeeSalaryInfoList);

            return View("SalaryInfo", excelTable);
        }

        public async Task<IActionResult> DownloadSalaryInfoUploadSample()
        {
            var wwwRoot = _env.WebRootPath;


            string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"SalaryInfo_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Import the data to worksheet
                var results = await _employeeBioDataAppService.ExportSalaryInfo();
                worksheet.ImportData(results, 1, 1, true);

                //Saving the workbook as stream
                FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                workbook.SaveAs(stream);
                await stream.DisposeAsync();
            }


            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(fileBytes, "application/ms-excel", "SalaryInfo_Sample.xlsx");

        }

        [HttpPost]
        public async Task<IActionResult> UploadSsnit(IFormFile customFile)
        {

            //read data from excel file
            ExcelEngine excelEngine = new ExcelEngine();
            //Loads or open an existing workbook through Open method of IWorkbooks
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(customFile.OpenReadStream());
            IWorksheet worksheet = workbook.Worksheets[0];

            // Read data from the worksheet and Export to the DataTable.

            DataTable excelTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            List<ImportEmployeeSnitInput> employeeSsnitList =  new List<ImportEmployeeSnitInput>();

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                var row = excelTable.Rows[i];

                var employeeSalaryInfo = new ImportEmployeeSnitInput
                {
                    EmployeeIdentifier = row.Table.Columns.Contains("Employee ID") ? row["Employee ID"].ToString().Trim() : string.Empty,
                    SocialSecurityNo = row.Table.Columns.Contains("Social Security No") ? row["Social Security No"].ToString().Trim() : string.Empty,
                    SocialSecurityFundEmployerContribution = row.Table.Columns.Contains("SSF Employer Contribution (%)") ? Convert.ToDecimal(row["SSF Employer Contribution (%)"].ToString().Trim()) : 0,
                    SocialSecurityFundEmployeeContribution = row.Table.Columns.Contains("SSF Employee Contribution (%)") ? Convert.ToDecimal(row["SSF Employee Contribution (%)"].ToString().Trim()) : 0,
                    ProvidentFundEmployeeContribution = row.Table.Columns.Contains("PF Employee Contribution (%)") ? Convert.ToDecimal(row["PF Employee Contribution (%)"].ToString().Trim()) : 0,
                    ProvidentFundEmployerContribution = row.Table.Columns.Contains("PF Employer Contribution (%)") ? Convert.ToDecimal(row["PF Employer Contribution (%)"].ToString().Trim()) : 0,
                    SecondProvidentFundEmployeeContribution = row.Table.Columns.Contains("2nd PF Employee Contribution (%)") ? Convert.ToDecimal(row["2nd PF Employee Contribution (%)"].ToString().Trim()) : 0,
                    SecondProvidentFundEmployerContribution = row.Table.Columns.Contains("2nd PF Employer Contribution (%)") ? Convert.ToDecimal(row["2nd PF Employer Contribution (%)"].ToString().Trim()) : 0,
                    SuperAnnuationEmployerContribution = row.Table.Columns.Contains("Super Annuation Employer Contribution") ? Convert.ToDecimal(row["2nd PF Employer Contribution (%)"].ToString().Trim()) : 0,
                    SuperAnnuationEmployeeContribution = row.Table.Columns.Contains("Super Annuation Employee Contribution") ? Convert.ToDecimal(row["Super Annuation Employee Contribution"].ToString().Trim()) : 0

                };

                employeeSsnitList.Add(employeeSalaryInfo);
            }

            await _employeeSnitAppService.Import(employeeSsnitList);

            return View("Ssnit", excelTable);
        }

        public async Task<IActionResult> DownloadSsnitUploadSample()
        {
            var wwwRoot = _env.WebRootPath;

            string fullPath = Path.Combine(wwwRoot + "/DataSample/", $"EmployeeSsnit_Sample_{DateTime.UtcNow:dd-MMM-yyyy-hh-mm-ss}.xlsx");

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Import the data to worksheet
                var results = await _employeeBioDataAppService.ExportSsnit();
                worksheet.ImportData(results, 1, 1, true);

                //Saving the workbook as stream
                FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite);
                workbook.SaveAs(stream);
                await stream.DisposeAsync();
            }


            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(fileBytes, "application/ms-excel", "EmployeeSsnit_Sample.xlsx");

        }
    
    }
}
