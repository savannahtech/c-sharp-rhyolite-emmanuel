using RhyoliteERP.Services.PropertyRental.LeaseTenants.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeaseTenants
{
    public interface ILeaseTenantAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListUnitTenants(Guid propertyId, Guid propertyUnitId);
        Task Create(CreateLeaseTenantInput input);
        Task Update(UpdateLeaseTenantInput input);
        Task Delete(Guid Id);

    }
}
