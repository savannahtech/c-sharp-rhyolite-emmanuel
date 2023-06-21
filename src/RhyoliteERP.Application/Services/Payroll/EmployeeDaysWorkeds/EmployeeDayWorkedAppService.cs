using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeDaysWorkeds;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeDaysWorkeds.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeDaysWorkeds
{
   public class EmployeeDayWorkedAppService: RhyoliteERPAppServiceBase, IEmployeeDayWorkedAppService
    {

        private readonly IEmployeeDaysWorkedManager _employeeDaysWorkedManager;
        private readonly IMapper _mapper;

        public EmployeeDayWorkedAppService(IEmployeeDaysWorkedManager employeeDaysWorkedManager, IMapper mapper)
        {
            _employeeDaysWorkedManager = employeeDaysWorkedManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeDaysWorkedManager.ListAll();
        }

        public async Task<object> ListAll(int month, int year, string salaryType)
        {
            return await _employeeDaysWorkedManager.ListAll(month, year, salaryType);
        }

        public async Task Create(CreateEmployeeDaysWorkedInput input)
        {
            var obj = _mapper.Map<EmployeeDaysWorked>(input);
            await _employeeDaysWorkedManager.Create(obj);
        }

        public async Task Update(UpdateEmployeeDaysWorkedInput input)
        {
            var obj = _mapper.Map<EmployeeDaysWorked>(input);
            await _employeeDaysWorkedManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeDaysWorkedManager.Delete(Id);

        }
    }
}
