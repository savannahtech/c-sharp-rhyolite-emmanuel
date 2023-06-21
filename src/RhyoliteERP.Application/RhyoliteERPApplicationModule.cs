using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RhyoliteERP.Authorization;

namespace RhyoliteERP
{
    [DependsOn(
        typeof(RhyoliteERPCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class RhyoliteERPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<RhyoliteERPAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(RhyoliteERPApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
