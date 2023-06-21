using System.Collections.Generic;
using RhyoliteERP.Roles.Dto;

namespace RhyoliteERP.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
