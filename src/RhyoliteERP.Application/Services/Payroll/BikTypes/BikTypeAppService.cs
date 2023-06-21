﻿using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.BikTypes;
using RhyoliteERP.DomainServices.Payroll.BikTypes.Dto;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.BikTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.BikTypes
{
   public class BikTypeAppService : RhyoliteERPAppServiceBase, IBikTypeAppService
    {
        private readonly IBikTypeManager _bikTypeManager;
        private readonly IMapper _mapper;

        public BikTypeAppService(IBikTypeManager bikTypeManager, IMapper mapper)
        {
            _bikTypeManager = bikTypeManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _bikTypeManager.ListAll();
        }

        public async Task<object> ListAll(Guid bikTypeId, Guid employeeId, Guid categoryId)
        {
            return await _bikTypeManager.ListAll(bikTypeId, employeeId, categoryId);

        }
        public async Task<object> Create(CreateBikTypeInput input)
        {
            var obj = _mapper.Map<BikType>(input);
            return await _bikTypeManager.Create(obj);
        }

        public async Task Update(UpdateBikTypeInput input)
        {
            var obj = _mapper.Map<BikType>(input);
            await _bikTypeManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _bikTypeManager.Delete(Id);
        }

        //
        public async Task<object> CreateBikRate(BikRateInput input)
        {
            return await _bikTypeManager.CreateBikRate(input);
        }

        public async Task UpdateBikRate(BikRateInput input)
        {
            await _bikTypeManager.UpdateBikRate(input);
        }

        public async Task DeleteBikRate(BikRateInput input)
        {
            await _bikTypeManager.DeleteBikRate(input);
        }

    }
}
