using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyUnits
{
    public interface IPropertyUnitManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> ListAll(bool isRented);
        Task<object> ListAll(Guid propertyId, bool isRented = false);
        Task<object> Create(PropertyUnit input);
        Task Update(PropertyUnit input);
        Task Delete(Guid Id);
    }
}
