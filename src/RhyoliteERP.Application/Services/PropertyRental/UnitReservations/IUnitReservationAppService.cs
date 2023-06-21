using RhyoliteERP.Services.PropertyRental.UnitReservations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.UnitReservations
{
    public interface IUnitReservationAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateUnitReservationInput input);
        Task Delete(Guid Id);
    }
}
