using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RhyoliteERP.Configuration;

namespace RhyoliteERP.Web.Startup
{
    [DependsOn(typeof(RhyoliteERPWebCoreModule))]
    public class RhyoliteERPWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public RhyoliteERPWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<RhyoliteERPNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RhyoliteERPWebMvcModule).GetAssembly());
        }
    }
}
