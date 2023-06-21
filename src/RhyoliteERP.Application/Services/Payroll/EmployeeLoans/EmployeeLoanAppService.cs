using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeLoans;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeLoans.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeLoans
{
   public class EmployeeLoanAppService: RhyoliteERPAppServiceBase, IEmployeeLoanAppService
    {
        private readonly IEmployeeLoanManager _employeeLoanManager;
        private readonly IMapper _mapper;
       
        public EmployeeLoanAppService(IEmployeeLoanManager employeeLoanManager, IMapper mapper)
        {
            _employeeLoanManager = employeeLoanManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeLoanManager.ListAll();
        }

        public async Task<object> ListPastLoans()
        {
            return await _employeeLoanManager.ListPastLoans();
        }

        public async Task<object> ListAll(Guid employeeId)
        {
            return await _employeeLoanManager.ListAll(employeeId);
        }

        public async Task<object> ListOutStandingLoans()
        {
            return await _employeeLoanManager.ListOutStandingLoans();
        }

        public async Task<object> ListPendingLoans()
        {
            return await _employeeLoanManager.ListPendingLoans();
        }

        public async Task Create(CreateEmployeeLoanInput input)
        {
            var obj = _mapper.Map<EmployeeLoan>(input);
            await _employeeLoanManager.Create(obj);
        }

        public async Task Update(UpdateEmployeeLoanInput input)
        {
            var obj = _mapper.Map<EmployeeLoan>(input);
            await _employeeLoanManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeLoanManager.Delete(Id);
        }

       public async Task Approve(List<Guid> ids, string approvalType)
        {
            await _employeeLoanManager.Approve(ids, approvalType);
        }

    }
}
