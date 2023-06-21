using System.Threading.Tasks;
using Abp.Application.Services;
using RhyoliteERP.Authorization.Accounts.Dto;

namespace RhyoliteERP.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
