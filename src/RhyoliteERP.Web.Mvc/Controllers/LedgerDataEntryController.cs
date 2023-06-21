using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;

namespace RhyoliteERP.Web.Controllers
{
    public class LedgerDataEntryController : RhyoliteERPControllerBase
    {
        public IActionResult Budget()
        {
            return View();
        }
        public IActionResult JournalEntry()
        {
            return View();
        }

        public IActionResult JournalEntryMt()
        {
            return View();
        }

        public IActionResult PostJournalEntry()
        {
            return View();
        }

        public IActionResult DeleteTransactions()
        {
            return View();
        }

        public IActionResult PayVoucherSuppliers()
        {
            return View();
        }

        public IActionResult PayVoucherEmployee()
        {
            return View();
        }

        public IActionResult ReceiptVoucherCustomer()
        {
            return View();
        }

        public IActionResult VoucherHistory()
        {
            return View();
        }


        //api
    }
}
