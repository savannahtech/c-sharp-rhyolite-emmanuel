using Abp.Authorization;
using RhyoliteERP.Authorization.Roles;
using RhyoliteERP.Authorization.Users;

namespace RhyoliteERP.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
