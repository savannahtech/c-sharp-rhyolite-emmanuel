﻿using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.BonusAndOnetimeAllowances;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.BonusAndOnetimeAllowances.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.BonusAndOnetimeAllowances
{
    public class BonusAndOnetimeAllowanceAppService:RhyoliteERPAppServiceBase, IBonusAndOnetimeAllowanceAppService
    {
        private readonly IBonusAndOnetimeAllowanceManager _bonusAndOnetimeAllowanceManager;
        private readonly IMapper _mapper;

        public BonusAndOnetimeAllowanceAppService(IBonusAndOnetimeAllowanceManager bonusAndOnetimeAllowanceManager, IMapper mapper)
        {
            _bonusAndOnetimeAllowanceManager = bonusAndOnetimeAllowanceManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _bonusAndOnetimeAllowanceManager.ListAll();
        }


        public async Task<object> ListAll(int month, int year)
        {
            return await _bonusAndOnetimeAllowanceManager.ListAll(month , year);
        }


        public async Task Create(CreateBonusAndOnetimeAllowanceInput input)
        {
            var obj = _mapper.Map<BonusAndOnetimeAllowance>(input);
            await _bonusAndOnetimeAllowanceManager.Create(obj);
        }

        public async Task Update(UpdateBonusAndOnetimeAllowanceInput input)
        {
            var obj = _mapper.Map<BonusAndOnetimeAllowance>(input);
            await _bonusAndOnetimeAllowanceManager.Update(obj);
        }


        public async Task Delete(Guid Id)
        {
            await _bonusAndOnetimeAllowanceManager.Delete(Id);
        }
    }
}
