using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace RhyoliteERP.Controllers
{
    public abstract class RhyoliteERPControllerBase: AbpController
    {
        protected RhyoliteERPControllerBase()
        {
            LocalizationSourceName = RhyoliteERPConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
