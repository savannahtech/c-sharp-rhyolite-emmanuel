using RhyoliteERP.DomainServices.Payroll.SalaryGrades.Dto;
using RhyoliteERP.Services.Payroll.SalaryGrades.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.SalaryGrades
{
   public interface ISalaryGradeAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateSalaryGradeInput input);
        Task Update(UpdateSalaryGradeInput input);
        Task Delete(Guid Id);
        Task<object> CreateSalaryNotch(SalaryNotchInput input);
        Task UpdateSalaryNotch(SalaryNotchInput input);
        Task DeleteSalaryNotch(SalaryNotchInput input);
    }
}
