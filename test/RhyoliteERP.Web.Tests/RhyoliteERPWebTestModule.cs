using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RhyoliteERP.EntityFrameworkCore;
using RhyoliteERP.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace RhyoliteERP.Web.Tests
{
    [DependsOn(
        typeof(RhyoliteERPWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class RhyoliteERPWebTestModule : AbpModule
    {
        public RhyoliteERPWebTestModule(RhyoliteERPEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RhyoliteERPWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(RhyoliteERPWebMvcModule).Assembly);
        }
    }
}