using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Payroll.PayCalendars.Dto;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.PayCalendars
{
   public class PayCalendarManager : Abp.Domain.Services.DomainService, IPayCalendarManager
    {
        private readonly IRepository<PayCalendar, Guid> _repositoryPayCalendar;

        public PayCalendarManager(IRepository<PayCalendar, Guid> repositoryPayCalendar)
        {
            _repositoryPayCalendar = repositoryPayCalendar;
        }

        public async Task Create(PayCalendar entity)
        {
            await _repositoryPayCalendar.InsertAsync(entity);

        }

        public async Task<object> CreatePayCalendarDetails(PayCalendarDetailInput input)
        {
            var payCalendarData = await _repositoryPayCalendar.GetAsync(input.PayCalendarId);
            var calendarList = payCalendarData.PayCalendarDetails;
            var calendarInfo = calendarList.FirstOrDefault(x => x.Id == input.Id);

            if (calendarInfo == null)
            {
                payCalendarData.PayCalendarDetails.Add(new PayCalendarDetail
                {
                    Id = Guid.NewGuid(),
                    PayCalendarId= input.PayCalendarId,
                    Year= payCalendarData.Year, 
                    Days = input.Days, 
                    Month= input.Month,

                });

                await _repositoryPayCalendar.UpdateAsync(payCalendarData);

                return new
                {
                    code = 200,
                    message = "successful"
                };
            }
            else
            {
                return new
                {
                    code = 400,
                    message = "Duplicate records are not allowed."
                };
            }
        }


        public async Task Update(PayCalendar entity)
        {
            await _repositoryPayCalendar.UpdateAsync(entity);
        }

        public async Task UpdatePayCalendarDetails(PayCalendarDetailInput input)
        {
            var payCalendarData = await _repositoryPayCalendar.GetAsync(input.PayCalendarId);
            var calendarList = payCalendarData.PayCalendarDetails;
            var calendarInfo = calendarList.FirstOrDefault(x => x.Id == input.Id);

            calendarList.Remove(calendarInfo);

            calendarInfo.Year = payCalendarData.Year;
            calendarInfo.Days = input.Days;
            calendarInfo.Month = input.Month;

            calendarList.Add(calendarInfo);

            payCalendarData.PayCalendarDetails = calendarList;

            await _repositoryPayCalendar.UpdateAsync(payCalendarData);
        }


        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryPayCalendar.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryPayCalendar.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryPayCalendar.DeleteAsync(id);

        }

        //
        public async Task DeletePayCalendarDetails(PayCalendarDetailInput input)
        {
            var payCalendarData = await _repositoryPayCalendar.GetAsync(input.PayCalendarId);
            var calendarList = payCalendarData.PayCalendarDetails;
            var calendarInfo = calendarList.FirstOrDefault(x => x.Id == input.Id);

            calendarList.Remove(calendarInfo);

            payCalendarData.PayCalendarDetails = calendarList;

            await _repositoryPayCalendar.UpdateAsync(payCalendarData);

        }

    }
}
