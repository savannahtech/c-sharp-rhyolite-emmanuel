using RhyoliteERP.Services.PropertyRental.Leases.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Leases
{
    public interface ILeaseAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateLeaseInput input);
        Task Update(UpdateLeaseInput input);
        Task Delete(Guid Id);
    }
}
