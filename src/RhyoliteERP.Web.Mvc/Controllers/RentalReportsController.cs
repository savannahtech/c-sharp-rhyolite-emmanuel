using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;

namespace RhyoliteERP.Web.Controllers
{
    public class RentalReportsController : RhyoliteERPControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
