using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.UnitReservations
{
    public interface IUnitReservationManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(UnitReservation input);
        Task Delete(Guid Id);
    }
}
