using Abp.Application.Services;
using RhyoliteERP.MultiTenancy.Dto;

namespace RhyoliteERP.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

