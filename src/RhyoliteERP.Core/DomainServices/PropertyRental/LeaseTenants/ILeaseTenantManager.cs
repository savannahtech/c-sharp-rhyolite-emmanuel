using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.LeaseTenants
{
    public interface ILeaseTenantManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> GetUnitTenants(Guid propertyId, Guid propertyUnitId);
        Task Create(LeaseTenant input);
        Task Update(LeaseTenant input);
        Task Delete(Guid Id);
    }
}
