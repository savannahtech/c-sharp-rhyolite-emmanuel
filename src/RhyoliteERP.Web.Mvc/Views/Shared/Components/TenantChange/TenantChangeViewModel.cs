using Abp.AutoMapper;
using RhyoliteERP.Sessions.Dto;

namespace RhyoliteERP.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
