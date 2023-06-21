using System.Collections.Generic;
using RhyoliteERP.Roles.Dto;

namespace RhyoliteERP.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}