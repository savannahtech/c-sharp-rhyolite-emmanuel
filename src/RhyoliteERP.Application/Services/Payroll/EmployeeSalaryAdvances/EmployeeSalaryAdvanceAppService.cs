using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeSalaryAdvances;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeSalaryAdvances.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeSalaryAdvances
{
   public class EmployeeSalaryAdvanceAppService: RhyoliteERPAppServiceBase, IEmployeeSalaryAdvanceAppService
    {
        private readonly IEmployeeSalaryAdvanceManager _employeeSalaryAdvanceManager;
        private readonly IMapper _mapper;

        public EmployeeSalaryAdvanceAppService(IEmployeeSalaryAdvanceManager employeeSalaryAdvanceManager, IMapper mapper)
        {
            _employeeSalaryAdvanceManager = employeeSalaryAdvanceManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeSalaryAdvanceManager.ListAll();
        }

        public async Task<object> ListAll(Guid employeeId)
        {
            return await _employeeSalaryAdvanceManager.ListAll(employeeId);
        }

        public async Task Create(CreateEmployeeSalaryAdvanceInput input)
        {
            var obj = _mapper.Map<EmployeeSalaryAdvance>(input);
            await _employeeSalaryAdvanceManager.Create(obj);
        }

        public async Task Delete(Guid id)
        {
            await _employeeSalaryAdvanceManager.Delete(id);

        }
    }
}
