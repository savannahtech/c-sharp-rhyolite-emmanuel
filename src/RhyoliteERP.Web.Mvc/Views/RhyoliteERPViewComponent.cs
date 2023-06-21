using Abp.AspNetCore.Mvc.ViewComponents;

namespace RhyoliteERP.Web.Views
{
    public abstract class RhyoliteERPViewComponent : AbpViewComponent
    {
        protected RhyoliteERPViewComponent()
        {
            LocalizationSourceName = RhyoliteERPConsts.LocalizationSourceName;
        }
    }
}
