using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.MultiTenancy;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RhyoliteERP.Authorization;
using RhyoliteERP.Authorization.Users;
using RhyoliteERP.Controllers;
using RhyoliteERP.DomainServices.Shared;
using RhyoliteERP.Subscription;
using RhyoliteERP.PaymentGateways.PayStackApi;
using RhyoliteERP.Roles;
using RhyoliteERP.Roles.Dto;
using RhyoliteERP.Services.Shared.ReportDownloads;
using RhyoliteERP.Users;
using RhyoliteERP.Users.Dto;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace RhyoliteERP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ManageController : RhyoliteERPControllerBase
    {
        private readonly UserManager _userManager;
        private readonly ISubscriptionAppService _subscriptionAppService;
        private readonly IUserAppService _userAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly IPayStackGateway _payStackGateway;
        private readonly IConfiguration _configuration;
        private readonly IReportDownloadAppService _reportDownloadAppService;
        private readonly IFileManager _fileManager;
        public ManageController(UserManager userManager, ISubscriptionAppService subscriptionAppService, IUserAppService userAppService, IRoleAppService roleAppService, IPayStackGateway payStackGateway, IConfiguration configuration, IReportDownloadAppService reportDownloadAppService, IFileManager fileManager)
        {
            _userManager = userManager;
            _subscriptionAppService = subscriptionAppService;
            _userAppService = userAppService;
            _roleAppService = roleAppService;
            _payStackGateway = payStackGateway;
            _configuration = configuration;
            _reportDownloadAppService = reportDownloadAppService;
            _fileManager = fileManager;
        }

        public IActionResult Profile()
        {
            
            return View();
        }

        public IActionResult Subscription()
        {
            return View();
        }

        public IActionResult Billing()
        {
            return View();
        }

        
        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult UpdatePassword()
        {
            return View();
        }

        public IActionResult AuditLogs()
        {
            return View();
        }


        public IActionResult Tickets()
        {
            return View();
        }

        public IActionResult CancelSubscription()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public async Task<IActionResult> Modules()
        {

            //var tentId = AbpSession.TenantId;
            //var iuserId = AbpSession.UserId.ToString();
            //var userId = int.Parse(iuserId);

            if (AbpSession.MultiTenancySide == MultiTenancySides.Host)
            {
                if (await IsGrantedAsync(PermissionNames.Pages_Tenants))
                {
                    return RedirectToAction("Index", "Tenants");
                }
                return RedirectToAction("Index", "HostDashboard");

            }
            else if (AbpSession.MultiTenancySide == MultiTenancySides.Tenant)
            {
                //var currentUser = await _userManager.GetUserByIdAsync(userId);

                //if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
                //{

                //}
                return View();

            }

            return View();

        }

        public async Task<IActionResult> Downloads(int pageNo = 1, int pageSize = 10)
        {
            dynamic viewmodel = new ExpandoObject();
            var reportData = await _reportDownloadAppService.ListAll(pageNo, pageSize);

            viewmodel.PaginatedDownload = reportData;
            return View(viewmodel);
        }

        //api...

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> ValidateModuleAccess([FromQuery]int id)
        {
            string redirectUrl = string.Empty;

            var isValidLicense = await _subscriptionAppService.ValidateModuleLicense(id);

            switch (id)
            {
                case RhyoliteERPConsts.SchoolManager:
                    redirectUrl = "/dashboard/schoolmanager";
                    break;
                case RhyoliteERPConsts.GeneralLedger:
                    redirectUrl = "/dashboard/generalledger";
                    break;
                case RhyoliteERPConsts.Banking:
                    redirectUrl = "/dashboard/banking";
                    break;
                case RhyoliteERPConsts.Payroll:
                    redirectUrl = "/dashboard/Payroll";
                    break;
                case RhyoliteERPConsts.StockManager:
                    redirectUrl = "/dashboard/stockmanager";
                    break;
                case RhyoliteERPConsts.AssetManager:
                    redirectUrl = "/dashboard/assetmsanager";
                    break;
                case RhyoliteERPConsts.PropertyRentals:
                    //redirectUrl = "/dashboard/assetmsanager";
                    redirectUrl = "/RentalSetups/PropertyTypes";
                    break;
                default:
                    break;
            }

            return Json(new { isValidLicense , redirectUrl });
        }
    
    
        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<IActionResult> GetUserDetails()
        {
            long currentuser = Convert.ToInt64(AbpSession.UserId);

            var user = await _userAppService.GetUser(currentuser);
            var customer = new 
            {
                 user.PhoneNumber,
                user.UserName,
                user.EmailAddress,
                 user.Name,
                 user.Surname,
                user.IsEmailConfirmed,
                 user.IsPhoneNumberConfirmed,
                 user.Id, user.Roles

            };

            return Json(customer);

        }


        public async Task<IActionResult> UpdateUserDetails([FromBody] UserDto dto)
        {
             await _userAppService.UpdateAsync(dto);

            return Json(new {code = 200, message = "User profile updated successfully."});
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> ChangePassword([FromBody] ChangePasswordDto input)
        {
            var result = await _userAppService.ChangePassword(input);
            return Json(result);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetRoles([FromQuery] int pageNo, int pageSize)
        {

            var result = await _roleAppService.ListAll(pageNo, pageSize);
            return Json(result);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> AddRole([FromBody] CreateRoleDto dto)
        {
            var role = await _roleAppService.CreateAsync(dto) ;
            return Json(role);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> DelRole([FromQuery] int id)
        {
              await _roleAppService.DeleteAsync(new EntityDto<int>(id));
              return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetUsers([FromQuery] int pageNo, int pageSize)
        {
            var result = await _userAppService.ListAll(pageNo, pageSize);
            return Json(result);
        }

        public async Task<JsonResult> AddUser([FromBody] CreateUserDto input)
        {
            await _userAppService.CreateAsync(input);
            return Json(200);
        }

        public async Task<JsonResult> EditUser([FromBody] UserDto input)
        {
            await _userAppService.UpdateAsync(input);
            return Json(200);
        }


        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> DelUser([FromQuery] long id)
        {
            var user = await _userAppService.GetAsync(new EntityDto<long>(id));

            await _userAppService.DeleteAsync(user);
            return Json(200);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetPermissions()
        {
            var permissions = (await _roleAppService.GetAllPermissions()).Items;

            return Json(permissions);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetSubscriptionInfo()
        {
            var response = await _subscriptionAppService.ListSubscribedModules();

            return Json(response);
        }

        [WrapResult(WrapOnSuccess = false, WrapOnError = false)]
        public async Task<JsonResult> GetInvoices([FromQuery]int pageNo, int pageSize)
        {
            var response = await _subscriptionAppService.ListInvoices(pageNo, pageSize);

            return Json(response);
        }

        

        public async Task<IActionResult> DownloadReportFile(string fileUrl)
        {
            byte[] fileBytes = await _fileManager.DownloadFileAsBytes(fileUrl);

            return File(fileBytes, "application/pdf", $"{Guid.NewGuid():N}.pdf");

        }
    }
}
