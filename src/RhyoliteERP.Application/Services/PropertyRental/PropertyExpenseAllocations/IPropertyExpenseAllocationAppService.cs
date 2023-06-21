using RhyoliteERP.Services.PropertyRental.MeterTypes.Dto;
using RhyoliteERP.Services.PropertyRental.PropertyExpenseAllocations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyExpenseAllocations
{
    public interface IPropertyExpenseAllocationAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreatePropertyExpenseAllocationInput input);
        Task Update(UpdatePropertyExpenseAllocationInput input);
        Task Delete(Guid Id);
    }
}
