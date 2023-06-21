using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class RentalLedgerController : RhyoliteERPControllerBase
    {
        public IActionResult DefaultGlNumbers()
        {
            return View();
        }

        public IActionResult RentalTransactions()
        {
            return View();
        }

        public IActionResult LeaseTransactions()
        {
            return View();
        }
    }
}
