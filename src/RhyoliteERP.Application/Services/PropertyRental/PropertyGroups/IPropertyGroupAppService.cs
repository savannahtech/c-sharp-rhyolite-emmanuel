using RhyoliteERP.Services.PropertyRental.PropertyGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyGroups
{
    public interface IPropertyGroupAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreatePropertyGroupInput input);
        Task Update(UpdatePropertyGroupInput input);
        Task Delete(Guid Id);
    }
}
