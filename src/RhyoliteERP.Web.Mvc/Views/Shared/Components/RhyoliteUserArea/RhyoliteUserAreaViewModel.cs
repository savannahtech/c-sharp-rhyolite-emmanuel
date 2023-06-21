using RhyoliteERP.Sessions.Dto;

namespace RhyoliteERP.Web.Views.Shared.Components.RhyoliteUserArea
{
    public class RhyoliteUserAreaViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public bool IsMultiTenancyEnabled { get; set; }

        public string GetShownRoleName()
        {
            return LoginInformations.User.EmailAddress;
        }
        public string GetShownLoginName()
        {
            var userName = LoginInformations.User.UserName;

            if (!IsMultiTenancyEnabled)
            {
                return userName;
            }

            return LoginInformations.Tenant == null
                ? ".\\" + userName
                : LoginInformations.Tenant.TenancyName + "\\" + userName;
        }
    }
}
