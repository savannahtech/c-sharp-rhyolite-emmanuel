using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using RhyoliteERP.Controllers;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : RhyoliteERPControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
