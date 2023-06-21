using RhyoliteERP.Services.PropertyRental.ScheduledTours.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.ScheduledTours
{
    public interface IScheduledTourAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateScheduledTourInput entity);
        Task Delete(Guid Id);
    }
}
