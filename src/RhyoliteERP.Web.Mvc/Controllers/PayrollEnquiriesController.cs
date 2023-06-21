using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using System.Threading.Tasks;
using System;
using RhyoliteERP.Services.Payroll.EmployeeAllowances;
using RhyoliteERP.Services.Payroll.EmployeeDeductions;
using RhyoliteERP.Services.Payroll.EmployeeBioDatas;
using RhyoliteERP.Services.Payroll.EmployeeOnetimeDeductions;
using RhyoliteERP.Services.Payroll.EmployeeSalaryInfos;
using RhyoliteERP.Services.Payroll.SalaryIncrements;
using RhyoliteERP.Services.Payroll.EmployeeBenefitInKinds;
using RhyoliteERP.Services.Payroll.EmployeeReliefs;
using RhyoliteERP.Services.Payroll.EmployeeLoans;
using RhyoliteERP.Services.Payroll.EmployeeLoanRepaymentSchedules;
using RhyoliteERP.Services.Payroll.EmployeeSalaryAdvances;
using RhyoliteERP.Services.Payroll.EmployeeSnits;
using RhyoliteERP.Services.Payroll.MonthlySsnitDeductionHistory;

namespace RhyoliteERP.Web.Controllers
{
    public class PayrollEnquiriesController : RhyoliteERPControllerBase
    {
        private readonly IEmployeeAllowanceAppService _employeeAllowanceAppService;
        private readonly IEmployeeDeductionAppService _employeeDeductionAppService;
        private readonly IEmployeeBioDataAppService _employeeBioDataAppService;
        private readonly IEmployeeOnetimeDeductionAppService _employeeOnetimeDeductionAppService;
        private readonly IEmployeeSalaryInfoAppService _employeeSalaryInfoAppService;
        private readonly ISalaryIncrementAppService _salaryIncrementAppService;
        private readonly IEmployeeBenefitInKindAppService _employeeBenefitInKindAppService;
        private readonly IEmployeeReliefAppService _employeeReliefAppService;
        private readonly IEmployeeLoanAppService _employeeLoanAppService;
        private readonly IEmployeeLoanRepaymentScheduleAppService _employeeLoanRepaymentScheduleAppService;
        private readonly IEmployeeSalaryAdvanceAppService _employeeSalaryAdvanceAppService;
        private readonly IEmployeeSnitAppService _employeeSnitAppService;
        private readonly IMonthlySsnitDeductionHistAppService _monthlySsnitDeductionHistAppService;
        public PayrollEnquiriesController(IEmployeeAllowanceAppService employeeAllowanceAppService, IEmployeeDeductionAppService employeeDeductionAppService, IEmployeeBioDataAppService employeeBioDataAppService, IEmployeeOnetimeDeductionAppService employeeOnetimeDeductionAppService, IEmployeeSalaryInfoAppService employeeSalaryInfoAppService, ISalaryIncrementAppService salaryIncrementAppService, IEmployeeBenefitInKindAppService employeeBenefitInKindAppService, IEmployeeReliefAppService employeeReliefAppService, IEmployeeLoanAppService employeeLoanAppService, IEmployeeLoanRepaymentScheduleAppService employeeLoanRepaymentScheduleAppService, IEmployeeSalaryAdvanceAppService employeeSalaryAdvanceAppService, IEmployeeSnitAppService employeeSnitAppService, IMonthlySsnitDeductionHistAppService monthlySsnitDeductionHistAppService)
        {
            _employeeAllowanceAppService = employeeAllowanceAppService;
            _employeeDeductionAppService = employeeDeductionAppService;
            _employeeBioDataAppService = employeeBioDataAppService;
            _employeeOnetimeDeductionAppService = employeeOnetimeDeductionAppService;
            _employeeSalaryInfoAppService = employeeSalaryInfoAppService;
            _salaryIncrementAppService = salaryIncrementAppService;
            _employeeBenefitInKindAppService = employeeBenefitInKindAppService;
            _employeeReliefAppService = employeeReliefAppService;
            _employeeLoanAppService = employeeLoanAppService;
            _employeeLoanRepaymentScheduleAppService = employeeLoanRepaymentScheduleAppService;
            _employeeSalaryAdvanceAppService = employeeSalaryAdvanceAppService;
            _employeeSnitAppService = employeeSnitAppService;
            _monthlySsnitDeductionHistAppService = monthlySsnitDeductionHistAppService;
        }

        public IActionResult EmployeesDirectory()
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

        public IActionResult EmployeeOnetimeDeductions()
        {
            return View();
        }

        public IActionResult EmployeeSalaryInfo()
        {
            return View();
        }

        public IActionResult SalaryIncrementHististory()
        {
            return View();
        }


        public IActionResult EmployeeBenefitsInKind()
        {
            return View();
        }

        public IActionResult EmployeeTaxReliefs()
        {
            return View();
        }


        public IActionResult OutstandingLoans()
        {
            return View();
        }


        public IActionResult EmployeeSalaryAdvance()
        {
            return View();
        }

        public IActionResult PastLoans()
        {
            return View();
        }

        public IActionResult EmployeeProvidentFund()
        {
            return View();
        }

        public IActionResult SsfContributions()
        {
            return View();
        }

        public IActionResult PastSsfContributions()
        {
            return View();
        }

        public IActionResult PayrollTransactions()
        {
            return View();
        }

        //api..
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeAllowances()
        {
            var results = await _employeeAllowanceAppService.ListAll();

            return Json(results);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeDeductions()
        {
            var results = await _employeeDeductionAppService.ListAll();

            return Json(results);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetOnetimeDeductions()
        {
            var employees = await _employeeBioDataAppService.ListAll();

            var onetimeDeduction = await _employeeOnetimeDeductionAppService.ListAll();

            return Json(new { employees, onetimeDeduction });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeesSalaryInfo()
        {
            var results = await _employeeSalaryInfoAppService.ListAll();
            return Json(results);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetSalaryIncrementHistory()
        {
            var results = await _salaryIncrementAppService.ListAll();

            return Json(results);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeesBik()
        {
            var employeeBik = await _employeeBenefitInKindAppService.ListAll();

            var employees = await _employeeBioDataAppService.ListAll();

            return Json(new { employeeBik, employees });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeTaxReliefs()
        {
            var employeeTaxReliefs = await _employeeReliefAppService.ListAll();

            var employees = await _employeeBioDataAppService.ListAll();

            return Json(new { employeeTaxReliefs, employees });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetOutstandingLoans()
        {
            var results = await _employeeLoanAppService.ListOutStandingLoans();

            return Json(results);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPastLoans()
        {
            var loans = await _employeeLoanAppService.ListPastLoans();

            var repaymentSchedule = await _employeeLoanRepaymentScheduleAppService.ListAll();

            return Json(new { loans, repaymentSchedule });
        }
        

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeSalaryAdvances()
        {
            var results = await _employeeSalaryAdvanceAppService.ListAll();

            return Json(results);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeSsnitInfo()
        {
            var results = await _employeeSnitAppService.ListAll();

            return Json(results);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPastSsfContributions([FromQuery]int month, int year)
        {
            var results = await _monthlySsnitDeductionHistAppService.ListAll(month, year);

            return Json(results);
        }

    }
}
