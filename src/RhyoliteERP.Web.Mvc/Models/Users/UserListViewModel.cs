using System.Collections.Generic;
using RhyoliteERP.Roles.Dto;

namespace RhyoliteERP.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
