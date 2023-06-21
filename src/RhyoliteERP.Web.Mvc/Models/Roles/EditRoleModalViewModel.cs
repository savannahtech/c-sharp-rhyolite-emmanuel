using Abp.AutoMapper;
using RhyoliteERP.Roles.Dto;
using RhyoliteERP.Web.Models.Common;

namespace RhyoliteERP.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
