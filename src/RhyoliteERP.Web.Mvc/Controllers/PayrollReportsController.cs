using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.Payroll.Reports;
using System;
using System.Threading.Tasks;


namespace RhyoliteERP.Web.Controllers
{
    public class PayrollReportsController : RhyoliteERPControllerBase
    {
        private readonly IPayrollReportsAppService _payrollReportsAppService;

        public PayrollReportsController(IPayrollReportsAppService payrollReportsAppService)
        {
            _payrollReportsAppService = payrollReportsAppService;
        }

        public IActionResult MrLoanDeductions()
        {
            return View();
        }

        public IActionResult MrEmployeeLoans()
        {
            return View();
        }

        public IActionResult MrMonthlyLoanPayout()
        {
            return View();
        }

        public IActionResult MrEmployeeOutStandingLoans()
        {
            return View();
        }

        public IActionResult MrAllowances()
        {
            return View();
        }

        public IActionResult MrPayRegister()
        {
            return View();
        }

        public IActionResult MrUnpaidSalaries()
        {
            return View();
        }

        public IActionResult MrPayslip()
        {
            return View();
        }


        public IActionResult MrSalaryList()
        {
            return View();
        }

        public IActionResult MrPensionScheme()
        {
            return View();
        }


        public IActionResult MrPensionSchemePayeReturns()
        {
            return View();
        }

        public IActionResult MrDeductions()
        {
            return View();
        }

        public IActionResult MrSalaryAdvance()
        {
            return View();
        }

        public IActionResult MrPayeReturns()
        {
            return View();
        }

        public IActionResult MrPensionDeductions()
        {
            return View();
        }



        public IActionResult MrPensionDeduction()
        {
            return View();
        }

        public IActionResult MrSuperAnnuation()
        {
            return View();
        }


        public IActionResult MrProvidentFund()
        {
            return View();
        }

        
        public IActionResult MrSecondProvidentFund()
        {
            return View();
        }

        
        public IActionResult MrBankPayout()
        {
            return View();
        }

        public IActionResult MrCashPayment()
        {
            return View();
        }

        public IActionResult MrTaxRelief()
        {
            return View();
        }

        public IActionResult MrDaysWorked()
        {
            return View();
        }


        //historical reports views
        public IActionResult HrLoanDeductions()
        {
            return View();
        }

        public IActionResult HrEmployeeLoans()
        {
            return View();
        }

        public IActionResult HrMonthlyLoanPayout()
        {
            return View();
        }

        public IActionResult HrEmployeeOutStandingLoans()
        {
            return View();
        }

        public IActionResult HrAllowances()
        {
            return View();
        }

        public IActionResult HrPayRegister()
        {
            return View();
        }


        public IActionResult HrUnpaidSalaries()
        {
            return View();
        }

        public IActionResult HrPayslip()
        {
            return View();
        }


        public IActionResult HrSalaryList()
        {
            return View();
        }

        public IActionResult HrPensionScheme()
        {
            return View();
        }

        public IActionResult HrPayeReturns()
        {
            return View();
        }

        

        public IActionResult HrPensionSchemePayeReturns()
        {
            return View();
        }

        public IActionResult HrDeductions()
        {
            return View();
        }

        public IActionResult HrSalaryAdvance()
        {
            return View();
        }

        public IActionResult HrPensionDeductions()
        {
            return View();
        }

        public IActionResult HrSuperAnnuation()
        {
            return View();
        }

        public IActionResult HrProvidentFund()
        {
            return View();
        }


        public IActionResult HrSecondProvidentFund()
        {
            return View();
        }


        public IActionResult HrBankPayout()
        {
            return View();
        }

        public IActionResult HrCashPayment()
        {
            return View();
        }

        public IActionResult HrTaxRelief()
        {
            return View();
        }

        public IActionResult HrDaysWorked()
        {
            return View();
        }

        //monthly reports api...

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPaySlip([FromQuery]Guid employeeId, string payslipType)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payslip : ");

            var reportDetails = await _payrollReportsAppService.GetPaySlip(employeeId, payslipType);

            return Json(new { reportHeader , reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetLoanDeductions()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Loan Deductions : ");

            var reportDetails = await _payrollReportsAppService.GetLoanDeductions();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetLoanDeductionsByType([FromQuery] Guid loanTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Loan Deductions: ");

            var reportDetails = await _payrollReportsAppService.GetLoanDeductions(loanTypeId);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetAllEmployeeLoans([FromQuery] DateTime startDate, DateTime endDate)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Employee Loans : ");

            var reportDetails = await _payrollReportsAppService.GetEmployeeLoans(startDate, endDate);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeLoans([FromQuery] DateTime startDate, DateTime endDate, Guid employeeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Employee Loans : ");

            var reportDetails = await _payrollReportsAppService.GetEmployeeLoans(startDate, endDate, employeeId);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetMonthlyLoanPayout([FromQuery] Guid loanTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Monthly Loan Payment: ");

            var reportDetails = await _payrollReportsAppService.GetMonthlyLoanPayout(loanTypeId);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetOutstandingLoans()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Outstanding Loans: ");

            var reportDetails = await _payrollReportsAppService.GetOutstandingLoans();

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetOutstandingLoansByLoanType([FromQuery] Guid employeeId, Guid loanTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Outstanding Loans: ");

            var reportDetails = await _payrollReportsAppService.GetOutstandingLoans(employeeId , loanTypeId);

            return Json(new { reportHeader, reportDetails });
        }



        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetAllowances()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Allowances: ");
            var reportDetails = await _payrollReportsAppService.GetAllowances();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetAllowancesByType([FromQuery] Guid allowanceTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Allowances:");
            var reportDetails = await _payrollReportsAppService.GetAllowances(allowanceTypeId);

            return Json(new { reportHeader, reportDetails });
        }



        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPayRegister()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Register: ");

            var reportDetails = await _payrollReportsAppService.GetPayRegister();

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetSalaryAdvance()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Salary Advance: ");
            var reportDetails = await _payrollReportsAppService.GetSalaryAdvance();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetDeductions()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Deduction Register: ");

            var reportDetails = await _payrollReportsAppService.GetDeductions();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetDeductionsByType([FromQuery]Guid deductionTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Deduction Register: ");

            var reportDetails = await _payrollReportsAppService.GetDeductions(deductionTypeId);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetDaysWorked()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Employee Days Worked: ");
            var reportDetails = await _payrollReportsAppService.GetDaysWorked();

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeList()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("");
            var reportDetails = await _payrollReportsAppService.GetEmployeeList();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPensionScheme([FromQuery] string schemeType)
        {
            var reportTitle = schemeType == "tier-i" ? "Pension Scheme Tier I (13.5%) , " : "Pension Scheme Tier II (5%) , ";

            var reportHeader = await _payrollReportsAppService.GetReportHeader(reportTitle);
            
            var reportDetails = await _payrollReportsAppService.GetPensionScheme(schemeType);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetBankPayments()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Bank Payments: ");
            var reportDetails = await _payrollReportsAppService.GetBankPayments();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetBankPaymentsByBank(Guid bankId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Bank Payments: ");
            var reportDetails = await _payrollReportsAppService.GetBankPayments(bankId);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetCashPayments()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Salary Cash Payments: ");
            var reportDetails = await _payrollReportsAppService.GetCashPayments();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetTaxReliefs()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Tax Reliefs: ");
            var reportDetails = await _payrollReportsAppService.GetTaxReliefs();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPayeeReturns()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Monthly Paye Reports: ");
            var reportDetails = await _payrollReportsAppService.GetPayeeReturns();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPensionSchemeDeductions()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Pension Scheme Deductions: ");
            var reportDetails = await _payrollReportsAppService.GetPensionSchemeDeductions();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetProvidentFundContributions()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Provident Fund Contributions: ");
            var reportDetails = await _payrollReportsAppService.GetProvidentFundContributions();

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetSecondProvidentFundContributions()
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Second Provident Fund Contributions: ");
            var reportDetails = await _payrollReportsAppService.GetSecondProvidentFundContributions();

            return Json(new { reportHeader, reportDetails });
        }

        //historical reports api...

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetLoanDeductions([FromQuery]int month,int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Loan Deductions : ");

            var reportDetails = await _payrollReportsAppService.GetLoanDeductions(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetLoanDeductionsByType([FromQuery] int month, int year, Guid loanTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Loan Deductions: ");

            var reportDetails = await _payrollReportsAppService.GetLoanDeductions(month, year, loanTypeId);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetAllEmployeeLoans([FromQuery] int month, int year, DateTime startDate, DateTime endDate)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Employee Loans : ");

            var reportDetails = await _payrollReportsAppService.GetEmployeeLoans(month, year, startDate, endDate);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetEmployeeLoans([FromQuery] int month, int year, DateTime startDate, DateTime endDate, Guid employeeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Employee Loans : ");

            var reportDetails = await _payrollReportsAppService.GetEmployeeLoans(month, year, startDate, endDate, employeeId);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetMonthlyLoanPayout([FromQuery] int month, int year, Guid loanTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Monthly Loan Payment: ");

            var reportDetails = await _payrollReportsAppService.GetMonthlyLoanPayout(month, year, loanTypeId);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetOutstandingLoans([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Outstanding Loans: ");

            var reportDetails = await _payrollReportsAppService.GetOutstandingLoans(month, year);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetOutstandingLoansByLoanType([FromQuery] int month, int year, Guid employeeId, Guid loanTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Outstanding Loans: ");

            var reportDetails = await _payrollReportsAppService.GetOutstandingLoans(month, year, employeeId, loanTypeId);

            return Json(new { reportHeader, reportDetails });
        }



        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetAllowances([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Allowances: ");

            var reportDetails = await _payrollReportsAppService.GetAllowances(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetAllowancesByType([FromQuery] int month, int year, Guid allowanceTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Allowances:");
            var reportDetails = await _payrollReportsAppService.GetAllowances(month, year, allowanceTypeId);

            return Json(new { reportHeader, reportDetails });
        }



        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetPayRegister([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Register: ");

            var reportDetails = await _payrollReportsAppService.GetPayRegister(month, year);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetSalaryAdvance([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Salary Advance: ");
            var reportDetails = await _payrollReportsAppService.GetSalaryAdvance(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetDeductions([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Deduction Register: ");

            var reportDetails = await _payrollReportsAppService.GetDeductions(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetDeductionsByType([FromQuery] int month, int year, Guid deductionTypeId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payroll Deduction Register: ");

            var reportDetails = await _payrollReportsAppService.GetDeductions(month, year, deductionTypeId);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetDaysWorked([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Employee Days Worked: ");

            var reportDetails = await _payrollReportsAppService.GetDaysWorked(month, year);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetEmployeeList([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("");
            var reportDetails = await _payrollReportsAppService.GetEmployeeList(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetPensionScheme([FromQuery] string schemeType, int month, int year)
        {
            var reportTitle = schemeType == "tier-i" ? "Pension Scheme Tier I (13.5%) , " : "Pension Scheme Tier II (5%) , ";

            var reportHeader = await _payrollReportsAppService.GetReportHeader(reportTitle);

            var reportDetails = await _payrollReportsAppService.GetPensionScheme(schemeType, month, year);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetBankPayments([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Bank Payments: ");
            var reportDetails = await _payrollReportsAppService.GetBankPayments(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetBankPaymentsByBank([FromQuery] int month, int year, Guid bankId)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Bank Payments: ");
            var reportDetails = await _payrollReportsAppService.GetBankPayments(month, year, bankId);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetCashPayments([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Salary Cash Payments: ");
            var reportDetails = await _payrollReportsAppService.GetCashPayments(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetTaxReliefs([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Tax Reliefs: ");
            var reportDetails = await _payrollReportsAppService.GetTaxReliefs(month, year);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetPayeeReturns([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Monthly Paye Reports: ");
            var reportDetails = await _payrollReportsAppService.GetPayeeReturns(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetPensionSchemeDeductions([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Pension Scheme Deductions: ");
            var reportDetails = await _payrollReportsAppService.GetPensionSchemeDeductions(month, year);

            return Json(new { reportHeader, reportDetails });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetProvidentFundContributions([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Provident Fund Contributions: ");
            var reportDetails = await _payrollReportsAppService.GetProvidentFundContributions(month, year);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetSecondProvidentFundContributions([FromQuery] int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Second Provident Fund Contributions: ");
            var reportDetails = await _payrollReportsAppService.GetSecondProvidentFundContributions(month, year);

            return Json(new { reportHeader, reportDetails });
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> HrGetPaySlip([FromQuery] Guid employeeId, string payslipType,int month, int year)
        {
            var reportHeader = await _payrollReportsAppService.GetReportHeader("Payslip : ");

            var reportDetails = await _payrollReportsAppService.GetPaySlip(employeeId, payslipType, month, year);

            return Json(new { reportHeader, reportDetails });
        }
    }
}
