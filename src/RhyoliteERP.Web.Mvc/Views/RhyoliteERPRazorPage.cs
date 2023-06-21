using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace RhyoliteERP.Web.Views
{
    public abstract class RhyoliteERPRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected RhyoliteERPRazorPage()
        {
            LocalizationSourceName = RhyoliteERPConsts.LocalizationSourceName;
        }
    }
}
