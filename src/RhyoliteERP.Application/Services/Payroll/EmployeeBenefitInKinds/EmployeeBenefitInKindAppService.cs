using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeBenefitInKinds;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeBenefitInKinds.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeBenefitInKinds
{
   public class EmployeeBenefitInKindAppService : RhyoliteERPAppServiceBase, IEmployeeBenefitInKindAppService
    {
        private readonly IEmployeeBenefitInKindManager _employeeBenefitInKindManager;
        private readonly IMapper _mapper;

        public EmployeeBenefitInKindAppService(IEmployeeBenefitInKindManager employeeBenefitInKindManager, IMapper mapper)
        {
            _employeeBenefitInKindManager = employeeBenefitInKindManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeBenefitInKindManager.ListAll();
        }

        public async Task<object> ListAll(Guid employeeId)
        {
            return await _employeeBenefitInKindManager.ListAll(employeeId);
        }

        public async Task<object> Create(CreateEmployeeBenefitInKindInput input)
        {
            var obj = _mapper.Map<EmployeeBenefitInKind>(input);
            return await _employeeBenefitInKindManager.Create(obj);
        }

        public async Task Update(UpdateEmployeeBenefitInKindInput input)
        {
            var obj = _mapper.Map<EmployeeBenefitInKind>(input);
            await _employeeBenefitInKindManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeBenefitInKindManager.Delete(Id);
        }
    }
}
