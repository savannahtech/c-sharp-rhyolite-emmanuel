using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using RhyoliteERP.Controllers;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [Abp.Auditing.DisableAuditing]
    public class HomeController : RhyoliteERPControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportAnIssue()
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        //api...
        //error messenger...
        public async Task<ActionResult> DispatchError([FromBody] object input)
        {
            
            return Json(200);
        }
    }
}
