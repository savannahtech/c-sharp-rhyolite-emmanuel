using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeOnetimeDeductions;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeOnetimeDeductions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeOnetimeDeductions
{
   public class EmployeeOnetimeDeductionAppService: RhyoliteERPAppServiceBase, IEmployeeOnetimeDeductionAppService
    {
        private readonly IEmployeeOnetimeDeductionManager _employeeOnetimeDeductionManager;
        private readonly IMapper _mapper;

        public EmployeeOnetimeDeductionAppService(IEmployeeOnetimeDeductionManager employeeOnetimeDeductionManager, IMapper mapper)
        {
            _employeeOnetimeDeductionManager = employeeOnetimeDeductionManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeOnetimeDeductionManager.ListAll();
        }

        public async Task<object> ListAll(Guid employeeId)
        {
            return await _employeeOnetimeDeductionManager.ListAll(employeeId);
        }

        public async Task<object> ListAll(int month, int year)
        {
            return await _employeeOnetimeDeductionManager.ListAll(month,year);

        }

        public async Task Create(CreateEmployeeOnetimeDeductionInput input)
        {
            var obj = _mapper.Map<EmployeeOnetimeDeduction>(input);
            await _employeeOnetimeDeductionManager.Create(obj);
        }

        public async Task Update(UpdateEmployeeOnetimeDeductionInput input)
        {
            var obj = _mapper.Map<EmployeeOnetimeDeduction>(input);
            await _employeeOnetimeDeductionManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeOnetimeDeductionManager.Delete(Id);

        }

    }
}
