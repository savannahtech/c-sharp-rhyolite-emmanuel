using RhyoliteERP.Services.PropertyRental.MeterTypes.Dto;
using RhyoliteERP.Services.PropertyRental.Properties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Properties
{
    public interface IPropertyAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListAll(bool isRented);
        Task<object> Create(CreatePropertyInput input);
        Task Update(UpdatePropertyInput input);
        Task Delete(Guid Id);
    }
}
