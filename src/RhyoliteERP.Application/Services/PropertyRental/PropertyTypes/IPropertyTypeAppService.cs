using RhyoliteERP.Services.PropertyRental.PropertyTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyTypes
{
    public interface IPropertyTypeAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreatePropertyTypeInput input);
        Task Update(UpdatePropertyTypeInput input);
        Task Delete(Guid Id);
    }
}
