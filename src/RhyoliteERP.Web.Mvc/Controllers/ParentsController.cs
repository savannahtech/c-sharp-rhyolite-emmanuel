using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.School.Parents;
using RhyoliteERP.Services.School.Parents.Dto;
using RhyoliteERP.Services.School.StudentParents;
using RhyoliteERP.Services.School.StudentParents.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ParentsController : RhyoliteERPControllerBase
    {
        private readonly IParentAppService _parentAppService;
        private readonly IStudentParentAppService _studentParentAppService;

        public ParentsController(IParentAppService parentAppService, IStudentParentAppService studentParentAppService)
        {
            _parentAppService = parentAppService;
            _studentParentAppService = studentParentAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Wards()
        {
            return View();
        }

        //api 
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetParents()
        {
            var result = await _parentAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetParentAndWards()
        {
            var parents = await _parentAppService.ListAll();
            var wards = await _parentAppService.ListParentWards();
            return Json(new { parents, wards });
        }

        //parents

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetParentDetails([FromQuery]Guid id)
        {
            var result = await _studentParentAppService.GetStudentParent(id);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateParent([FromBody] CreateParentInput input)
        {
            var result = await _parentAppService.Create(input);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateOrUpdateParentStudentMapping([FromBody] CreateStudentParentInput input)
        {
            await _studentParentAppService.Create(input);
            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> UpdateParent([FromBody] UpdateParentInput input)
        {
            var result = await _parentAppService.Update(input);
            return Json(result);
        }

        public async Task<ActionResult> DelParent([FromQuery] Guid id)
        {
            await _parentAppService.Delete(id);
            return Json(200);
        }

    }
}
