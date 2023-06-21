using RhyoliteERP.Services.PropertyRental.LeasePayments.Dto;
using RhyoliteERP.Services.PropertyRental.MeterTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.MeterTypes
{
    public interface IMeterTypeAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateMeterTypeInput input);
        Task Update(UpdateMeterTypeInput input);
        Task Delete(Guid Id);

    }
}
