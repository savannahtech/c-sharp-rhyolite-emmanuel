using Abp.MultiTenancy;
using RhyoliteERP.Authorization.Users;

namespace RhyoliteERP.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
