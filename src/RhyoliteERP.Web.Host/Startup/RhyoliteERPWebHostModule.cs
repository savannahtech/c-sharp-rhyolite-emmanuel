using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RhyoliteERP.Configuration;

namespace RhyoliteERP.Web.Host.Startup
{
    [DependsOn(
       typeof(RhyoliteERPWebCoreModule))]
    public class RhyoliteERPWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public RhyoliteERPWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RhyoliteERPWebHostModule).GetAssembly());
        }
    }
}
