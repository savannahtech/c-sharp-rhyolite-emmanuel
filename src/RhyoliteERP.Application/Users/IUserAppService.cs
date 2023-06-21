using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using RhyoliteERP.Authorization.Users;
using RhyoliteERP.Roles.Dto;
using RhyoliteERP.Users.Dto;

namespace RhyoliteERP.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);
        Task<bool> ChangePassword(ChangePasswordDto input);
        Task<User> GetUser(long Id);
        Task<object> ListAll(int pageNo, int pageSize);

    }
}
