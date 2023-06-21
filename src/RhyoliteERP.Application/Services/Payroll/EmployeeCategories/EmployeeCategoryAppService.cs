using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeCategories;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeCategories
{
   public class EmployeeCategoryAppService: RhyoliteERPAppServiceBase, IEmployeeCategoryAppService
    {
        private readonly IEmployeeCategoryManager _employeeCategoryManager;
        private readonly IMapper _mapper;

        public EmployeeCategoryAppService(IEmployeeCategoryManager employeeCategoryManager, IMapper mapper)
        {
            _employeeCategoryManager = employeeCategoryManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeCategoryManager.ListAll();
        }

        public async Task<object> Create(CreateEmployeeCategoryInput input)
        {
            var obj = _mapper.Map<EmployeeCategory>(input);
            return await _employeeCategoryManager.Create(obj);
        }

        public async Task Update(UpdateEmployeeCategoryInput input)
        {
            var obj = _mapper.Map<EmployeeCategory>(input);
            await _employeeCategoryManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeCategoryManager.Delete(Id);

        }
    }
}
