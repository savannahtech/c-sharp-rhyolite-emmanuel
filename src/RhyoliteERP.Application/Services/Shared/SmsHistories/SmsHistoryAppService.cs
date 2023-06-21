using Abp.Domain.Uow;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.DomainServices.Shared.SmsHistories;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.SmsHistories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.SmsHistories
{
   public class SmsHistoryAppService: RhyoliteERPAppServiceBase, ISmsHistoryAppService
    {

        private readonly ISmsHistoryManager _smsHistoryManager;
        private readonly IMapper _mapper;

        public SmsHistoryAppService(ISmsHistoryManager smsHistoryManager, IMapper mapper)
        {
            _smsHistoryManager = smsHistoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task Create(SmsHistoryInput input)
        {
            var obj = _mapper.Map<SmsHistory>(input);
            await _smsHistoryManager.Create(obj);
        }

        public async Task<object> ListAll(DateTime startDate, DateTime endDate, int source)
        {
            return await _smsHistoryManager.ListAll(startDate,endDate, source);
        }
    }
}
