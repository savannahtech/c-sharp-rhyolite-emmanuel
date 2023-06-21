using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.Controllers;
using RhyoliteERP.Services.PropertyRental.BidOffers;
using RhyoliteERP.Services.PropertyRental.LeaseApplicants;
using RhyoliteERP.Services.PropertyRental.LeaseApplicants.Dto;
using RhyoliteERP.Services.PropertyRental.Leases;
using RhyoliteERP.Services.PropertyRental.Leases.Dto;
using RhyoliteERP.Services.PropertyRental.LeaseTenants;
using RhyoliteERP.Services.PropertyRental.Properties;
using RhyoliteERP.Services.PropertyRental.Properties.Dto;
using RhyoliteERP.Services.PropertyRental.PropertyUnits;
using RhyoliteERP.Services.PropertyRental.PropertyUnits.Dto;
using RhyoliteERP.Services.PropertyRental.ResidentAccounts;
using RhyoliteERP.Services.PropertyRental.ScheduledTours;
using System;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class RentalsController : RhyoliteERPControllerBase
    {
        private readonly IPropertyAppService _propertyAppService;
        private readonly ILeaseTenantAppService _leaseTenantAppService;
        private readonly IPropertyUnitAppService _propertyUnitAppService;
        private readonly ILeaseAppService _leaseAppService;
        private readonly ILeaseApplicantAppService _leaseApplicantAppService;
        private readonly IBidOfferAppService _bidOfferAppService;
        private readonly IScheduledTourAppService _scheduledTourAppService;
        private readonly IResidentAccountAppService _residentAccountAppService;
        public RentalsController(ILeaseTenantAppService leaseTenantAppService, IPropertyAppService propertyAppService, IPropertyUnitAppService propertyUnitAppService, ILeaseAppService leaseAppService, ILeaseApplicantAppService leaseApplicantAppService, IBidOfferAppService bidOfferAppService, IScheduledTourAppService scheduledTourAppService, IResidentAccountAppService residentAccountAppService)
        {
            _leaseTenantAppService = leaseTenantAppService;
            _propertyAppService = propertyAppService;
            _propertyUnitAppService = propertyUnitAppService;
            _leaseAppService = leaseAppService;
            _leaseApplicantAppService = leaseApplicantAppService;
            _bidOfferAppService = bidOfferAppService;
            _scheduledTourAppService = scheduledTourAppService;
            _residentAccountAppService = residentAccountAppService;
        }

        public IActionResult RegisterProperty()
        {
            return View();
        }

        public IActionResult PropertyUnits()
        {
            return View();
        }

        public IActionResult LeaseProperty()
        {
            return View();
        }

        public IActionResult LeasePropertyUnit()
        {
            return View();
        }

        public IActionResult Tenants()
        {
            return View();
        }

        public IActionResult MeterReadings()
        {
            return View();
        }


        public IActionResult RentalOwners()
        {
            return View();
        }

        public IActionResult OutstandingBalances()
        {
            return View();
        }


        public IActionResult UnitListings()
        {
            return View();
        }

        
        public IActionResult Applicants()
        {
            return View();
        }

        public IActionResult CreateApplicant()
        {
            return View();
        }

        
        public IActionResult DraftLeases()
        {
            return View();
        }

        public IActionResult RecievePayments()
        {
            return View();
        }

        public IActionResult LeaseRenewal()
        {
            return View();
        }

        public IActionResult BidOffers()
        {
            return View();
        }


        public IActionResult ScheduledTours()
        {
            return View();
        }

        //api

        

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetResidentAccounts()
        {
            var result = await _residentAccountAppService.ListAll();
            return Json(result);
        }

        //properties : not rented or soldout...

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetAvailableProperties()
        {
            var result = await _propertyAppService.ListAll(false);
            return Json(result);
        }

        //properties : all
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetProperties()
        {
            var result = await _propertyAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateProperty([FromBody] CreatePropertyInput input)
        {
            var result = await _propertyAppService.Create(input);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> UpdateProperty([FromBody] UpdatePropertyInput input)
        {
            await _propertyAppService.Update(input);
            return Json(200);
        }

        //property units
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetPropertyUnits([FromQuery]Guid propertyId)
        {
            var result = await _propertyUnitAppService.ListAll(propertyId);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreatePropertyUnit([FromBody] CreatePropertyUnitInput input)
        {
            var result = await _propertyUnitAppService.Create(input);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> UpdatePropertyUnit([FromBody] UpdatePropertyUnitInput input)
        {
            await _propertyUnitAppService.Update(input);
            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetLeaseTenants()
        {
            var result = await _leaseTenantAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreatePropertyLease([FromBody] CreateLeaseInput  input)
        {
            await _leaseAppService.Create(input);
            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> UpdatePropertyLease([FromBody] UpdateLeaseInput input)
        {
            await _leaseAppService.Update(input);
            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetUnitListings()
        {
            var result = await _propertyUnitAppService.ListAll();
            return Json(result);
        }


        //applicants
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetLeaseApplicants()
        {
            var result = await _leaseApplicantAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> CreateLeaseApplicants([FromBody] CreateLeaseApplicantInput input)
        {
            await _leaseApplicantAppService.Create(input);
            return Json(200);
        }

        public async Task<ActionResult> DelLeaseApplicant([FromQuery]Guid id)
        {
            await _leaseApplicantAppService.Delete(id);
            return Json(200);
        }


        //bid offers
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetBidOffers()
        {
            var result = await _bidOfferAppService.ListAll();
            return Json(result);
        }

        // scheduled tours
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetSheduledTours()
        {
            var result = await _scheduledTourAppService.ListAll();
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> GetUnitTenants([FromQuery] Guid propertyId, Guid propertyUnitId)
        {
            var result = await _leaseTenantAppService.ListUnitTenants(propertyId, propertyUnitId);
            return Json(result);
        }


        
        // payments...

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<ActionResult> RecieveLeasePayment([FromBody] CreatePropertyInput input)
        {
            var result = await _propertyAppService.Create(input);
            return Json(result);
        }

    }
}
