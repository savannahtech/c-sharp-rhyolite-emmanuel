﻿using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeReliefs;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeReliefs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeReliefs
{
   public class EmployeeReliefAppService: RhyoliteERPAppServiceBase, IEmployeeReliefAppService
    {
        private readonly IEmployeeReliefManager _employeeReliefManager;
        private readonly IMapper _mapper;

        public EmployeeReliefAppService(IEmployeeReliefManager employeeReliefManager, IMapper mapper)
        {
            _employeeReliefManager = employeeReliefManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeReliefManager.ListAll();
        }

        
        public async Task<object> ListAll(Guid employeeId)
        {
            return await _employeeReliefManager.ListAll(employeeId);
        }

        public async Task<object> Create(CreateEmployeeReliefInput input)
        {
            var obj = _mapper.Map<EmployeeRelief>(input);
            return await _employeeReliefManager.Create(obj);
        }

        public async Task Update(UpdateEmployeeReliefInput input)
        {
            var obj = _mapper.Map<EmployeeRelief>(input);
            await _employeeReliefManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeReliefManager.Delete(Id);

        }
    }
}
