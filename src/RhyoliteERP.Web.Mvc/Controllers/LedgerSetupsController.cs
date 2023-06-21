﻿using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.Shared.Departments.Dto;
using System.Threading.Tasks;
using System;
using RhyoliteERP.Services.Ledger.CoaHierachies;
using RhyoliteERP.Services.Ledger.CoaHierachies.Dto;
using RhyoliteERP.Services.Ledger.CoaControls;
using RhyoliteERP.Services.Ledger.CoaControls.Dto;
using RhyoliteERP.Services.Ledger.CoaDetails;
using RhyoliteERP.Services.Ledger.CoaDetails.Dto;
using RhyoliteERP.Services.Shared.Currencies;
using RhyoliteERP.Services.Ledger.ImprestCategories;
using RhyoliteERP.Services.Ledger.ImprestCategories.Dto;
using RhyoliteERP.Services.Ledger.PettyCashRecipients;
using RhyoliteERP.Services.Ledger.PettyCashRecipients.Dto;
using RhyoliteERP.Services.Ledger.BankAccounts;
using RhyoliteERP.Services.Ledger.BankAccounts.Dto;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class LedgerSetupsController : RhyoliteERPControllerBase
    {
        private readonly ICoaHierachyAppService _coaHierachyAppService;
        private readonly ICoaControlAppService _coaControlAppService;
        private readonly ICoaDetailAppService _coaDetailAppService;
        private readonly ICurrencyAppService _currencyAppService;
        private readonly IImprestCategoryAppService _imprestCategoryAppService;
        private readonly IPettyCashRecipientAppService _pettyCashRecipientAppService;
        private readonly IBankAccountAppService _bankAccountAppService;

        public LedgerSetupsController(ICoaHierachyAppService coaHierachyAppService, ICoaControlAppService coaControlAppService, ICoaDetailAppService coaDetailAppService, ICurrencyAppService currencyAppService, IImprestCategoryAppService imprestCategoryAppService, IPettyCashRecipientAppService pettyCashRecipientAppService, IBankAccountAppService bankAccountAppService)
        {
            _coaHierachyAppService = coaHierachyAppService;
            _coaControlAppService = coaControlAppService;
            _coaDetailAppService = coaDetailAppService;
            _currencyAppService = currencyAppService;
            _imprestCategoryAppService = imprestCategoryAppService;
            _pettyCashRecipientAppService = pettyCashRecipientAppService;
            _bankAccountAppService = bankAccountAppService;
        }

        public IActionResult BankAccounts()
        {
            return View();
        }

        public IActionResult ChartOfAccounts()
        {
            return View();
        }

        public IActionResult ChartOfAccountControl()
        {
            return View();
        }

        public IActionResult ChartOfAccountsDetail()
        {
            return View();
        }

        public IActionResult ImprestCategories()
        {
            return View();
        }

        public IActionResult PettyCashReciepients()
        {
            return View();
        }

        public IActionResult DefaultGlNumbers()
        {
            return View();
        }

        //api
        // imprest categories...
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetBankAccounts()
        {
            var result = await _bankAccountAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateBankAccount([FromBody] CreateBankAccountInput input)
        {
            var result = await _bankAccountAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateBankAccount([FromBody] UpdateBankAccountInput input)
        {
            await _bankAccountAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelBankAccount([FromQuery] Guid id)
        {
            await _bankAccountAppService.Delete(id);
            return Json(200);
        }

        //chart of accounts...
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetChartOfAccounts()
        {
            var result = await _coaHierachyAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateChartOfAccount([FromBody] CreateCoaHierachyInput input)
        {
            var result = await _coaHierachyAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateChartOfAccount([FromBody] UpdateCoaHierachyInput input)
        {
            await _coaHierachyAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelChartOfAccount([FromQuery] Guid id)
        {
            await _coaHierachyAppService.Delete(id);
            return Json(200);
        }


        //chart of accounts control...

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetChartOfAccountContols()
        {
            var result = await _coaControlAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetChartOfAccountContolsByAccountGroup([FromQuery]Guid id)
        {
            var result = await _coaControlAppService.ListAll(id);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateChartOfAccountContol([FromBody] CreateCoaControlInput input)
        {
            var result = await _coaControlAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateChartOfAccountContol([FromBody] UpdateCoaControlInput input)
        {
            await _coaControlAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelChartOfAccountContol([FromQuery] Guid id)
        {
            await _coaControlAppService.Delete(id);
            return Json(200);
        }

        //
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetChartOfAccountDetails()
        {
            var accountDetails = await _coaDetailAppService.ListAll();
            var accountControls = await _coaControlAppService.ListAll();
            var currency = await _currencyAppService.ListAll();

            return Json(new { accountDetails, currency, accountControls });
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetLedgerAccounts()
        {
            var accounts = await _coaDetailAppService.ListAll();

            return Json(accounts);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateChartOfAccountDetail([FromBody] CreateCoaDetailInput input)
        {
            input.CurrentForeignBalance = 0;
            input.CurrentBalance = 0;

            var result = await _coaDetailAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateChartOfAccountDetail([FromBody] UpdateCoaDetailInput input)
        {
            input.CurrentForeignBalance = 0;
            input.CurrentBalance = 0;

            await _coaDetailAppService.Update(input);
            return Json(200);
        }

        public async Task<ActionResult> DelChartOfAccountDetail([FromQuery] Guid id)
        {
            await _coaDetailAppService.Delete(id);
            return Json(200);
        }

        // imprest categories...
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetImprestCategories()
        {
            var result = await _imprestCategoryAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateImprestCategory([FromBody] CreateImprestCategoryInput input)
        {
            var result = await _imprestCategoryAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdateImprestCategory([FromBody] UpdateImprestCategoryInput input)
        {
            await _imprestCategoryAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelImprestCategory([FromQuery] Guid id)
        {
            await _imprestCategoryAppService.Delete(id);
            return Json(200);
        }


        // imprest categories...
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPettyCashRecipients()
        {
            var result = await _pettyCashRecipientAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreatePettyCashRecipient([FromBody] CreatePettyCashRecipientInput input)
        {
            var result = await _pettyCashRecipientAppService.Create(input);
            return Json(result);
        }

        public async Task<ActionResult> UpdatePettyCashRecipient([FromBody] UpdatePettyCashRecipientInput input)
        {
            await _pettyCashRecipientAppService.Update(input);
            return Json(200);

        }

        public async Task<ActionResult> DelPettyCashRecipient([FromQuery] Guid id)
        {
            await _pettyCashRecipientAppService.Delete(id);
            return Json(200);
        }

    }
}
