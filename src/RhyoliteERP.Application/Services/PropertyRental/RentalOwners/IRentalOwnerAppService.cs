using RhyoliteERP.Services.PropertyRental.RentalOwners.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.RentalOwners
{
    public interface IRentalOwnerAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateRentalOwnerInput input);
        Task Update(UpdateRentalOwnerInput input);
        Task Delete(Guid Id);
    }
}
