using RhyoliteERP.DomainServices.Payroll.PayCalendars.Dto;
using RhyoliteERP.Services.Payroll.PayCalendars.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.PayCalendars
{
   public interface IPayCalendarAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreatePayCalendarInput input);
        Task Update(UpdatePayCalendarInput input);
        Task Delete(Guid Id);

        //
        Task<object> CreatePayCalendarDetails(PayCalendarDetailInput input);
        Task UpdatePayCalendarDetails(PayCalendarDetailInput input);
        Task DeletePayCalendarDetails(PayCalendarDetailInput input);
    }
}
