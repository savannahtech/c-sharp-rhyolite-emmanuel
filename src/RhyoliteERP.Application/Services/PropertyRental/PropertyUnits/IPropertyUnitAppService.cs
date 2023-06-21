using RhyoliteERP.Services.PropertyRental.PropertyUnits.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyUnits
{
    public interface IPropertyUnitAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListAll(bool isRented = false);
        Task<object> ListAll(Guid propertyId, bool isRented = false);
        Task<object> Create(CreatePropertyUnitInput input);
        Task Update(UpdatePropertyUnitInput input);
        Task Delete(Guid Id);
    }
}
