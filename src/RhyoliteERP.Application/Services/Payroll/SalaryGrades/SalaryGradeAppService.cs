using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.SalaryGrades;
using RhyoliteERP.DomainServices.Payroll.SalaryGrades.Dto;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.SalaryGrades.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.SalaryGrades
{
   public class SalaryGradeAppService: RhyoliteERPAppServiceBase, ISalaryGradeAppService
    {
        private readonly ISalaryGradeManager _salaryGradeManager;
        private readonly IMapper _mapper;

        public SalaryGradeAppService(ISalaryGradeManager salaryGradeManager, IMapper mapper)
        {
            _salaryGradeManager = salaryGradeManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _salaryGradeManager.ListAll();
        }

        public async Task<object> Create(CreateSalaryGradeInput input)
        {
            var obj = _mapper.Map<SalaryGrade>(input);
            return await _salaryGradeManager.Create(obj);
        }

        public async Task<object> CreateSalaryNotch(SalaryNotchInput input)
        {
            return await _salaryGradeManager.CreateSalaryNotch(input);
        }

        public async Task Update(UpdateSalaryGradeInput input)
        {
            var obj = _mapper.Map<SalaryGrade>(input);
            await _salaryGradeManager.Update(obj);
        }

        public async Task UpdateSalaryNotch(SalaryNotchInput input)
        {
            await _salaryGradeManager.UpdateSalaryNotch(input);
        }
        public async Task Delete(Guid Id)
        {
            await _salaryGradeManager.Delete(Id);

        }

        public async Task DeleteSalaryNotch(SalaryNotchInput input)
        {
            await _salaryGradeManager.DeleteSalaryNotch(input);
        }
    }
}
