using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.ScheduledTours
{
    public interface IScheduledTourManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(ScheduledTour entity);
        Task Delete(Guid Id);
    }
}
