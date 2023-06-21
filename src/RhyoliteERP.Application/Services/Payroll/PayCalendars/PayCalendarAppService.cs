using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.PayCalendars;
using RhyoliteERP.DomainServices.Payroll.PayCalendars.Dto;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.PayCalendars.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.PayCalendars
{
   public class PayCalendarAppService: RhyoliteERPAppServiceBase, IPayCalendarAppService
    {
        private readonly IPayCalendarManager _payCalendarManager;
        private readonly IMapper _mapper;

        public PayCalendarAppService(IPayCalendarManager payCalendarManager, IMapper mapper)
        {
            _payCalendarManager = payCalendarManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _payCalendarManager.ListAll();
        }

        public async Task Create(CreatePayCalendarInput input)
        {
            var obj = _mapper.Map<PayCalendar>(input);
            await _payCalendarManager.Create(obj);
        }

        public async Task<object> CreatePayCalendarDetails(PayCalendarDetailInput input)
        {
            return await _payCalendarManager.CreatePayCalendarDetails(input);
        }

        public async Task Update(UpdatePayCalendarInput input)
        {
            var obj = _mapper.Map<PayCalendar>(input);
            await _payCalendarManager.Update(obj);
        }

        public async Task UpdatePayCalendarDetails(PayCalendarDetailInput input)
        {
            await _payCalendarManager.UpdatePayCalendarDetails(input);
        }

        public async Task Delete(Guid Id)
        {
            await _payCalendarManager.Delete(Id);
        }

        public async Task DeletePayCalendarDetails(PayCalendarDetailInput input)
        {
            await _payCalendarManager.DeletePayCalendarDetails(input);
        }
    }
}
