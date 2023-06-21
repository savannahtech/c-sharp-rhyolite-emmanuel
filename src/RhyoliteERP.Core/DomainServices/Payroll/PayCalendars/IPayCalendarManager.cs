using RhyoliteERP.DomainServices.Payroll.PayCalendars.Dto;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.PayCalendars
{
   public interface IPayCalendarManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(PayCalendar input);
        Task Update(PayCalendar input);
        Task Delete(Guid Id);
        Task<object> CreatePayCalendarDetails(PayCalendarDetailInput input);
        Task UpdatePayCalendarDetails(PayCalendarDetailInput input);
        Task DeletePayCalendarDetails(PayCalendarDetailInput input);
    }
}
