using System.Threading.Tasks;
using Abp.Application.Services;
using RhyoliteERP.Sessions.Dto;

namespace RhyoliteERP.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
