using System.Threading.Tasks;
using Abp.Configuration.Startup;
using RhyoliteERP.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace RhyoliteERP.Web.Views.Shared.Components.RhyoliteSideBarUserArea
{
    public class RhyoliteSideBarUserAreaViewComponent : RhyoliteERPViewComponent
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public RhyoliteSideBarUserAreaViewComponent(
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig)
        {
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new RhyoliteSideBarUserAreaViewModel
            {
                LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
            };

            return View(model);
        }
    }
}
