using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using System.Threading.Tasks;
using System;
using RhyoliteERP.Services.Payroll.EmployeeSalaryInfos;
using RhyoliteERP.DomainServices.Payroll.EmployeeSalaryInfos.Dto;

namespace RhyoliteERP.Web.Controllers
{
    [Abp.AspNetCore.Mvc.Authorization.AbpMvcAuthorize]
    public class SalaryIncreaseController : RhyoliteERPControllerBase
    {
        private readonly IEmployeeSalaryInfoAppService _employeeSalaryInfoAppService;

        public SalaryIncreaseController(IEmployeeSalaryInfoAppService employeeSalaryInfoAppService)
        {
            _employeeSalaryInfoAppService = employeeSalaryInfoAppService;
        }

        public IActionResult IncreaseAcrossBoard()
        {
            return View();
        }

        public IActionResult IncreaseByCategory()
        {
            return View();
        }

        public IActionResult IncreaseByGradeNotch()
        {
            return View();
        }

        // => api

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeBySalaryType([FromQuery] string salaryType)
        {
            var results = await _employeeSalaryInfoAppService.GetAllBySalaryType(salaryType);

            return Json(results);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeByCategory([FromQuery] string salaryType, Guid categoryId)
        {
            var results = await _employeeSalaryInfoAppService.GetAllBySalaryType(salaryType, categoryId);

            return Json(results);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetEmployeeBySalaryGrade([FromQuery] string salaryType, Guid salaryGradeId, Guid salaryNotchId)
        {
            var results = await _employeeSalaryInfoAppService.GetAllBySalaryGrade(salaryType, salaryGradeId, salaryNotchId);

            return Json(results);
        }

        public async Task<ActionResult> ProcessSalaryIncrement([FromBody] SalaryIncrementDto input)
        {
            await _employeeSalaryInfoAppService.ProcessSalaryIncrement(input.SalaryData);
            return Json(200);

        }
    }
}
