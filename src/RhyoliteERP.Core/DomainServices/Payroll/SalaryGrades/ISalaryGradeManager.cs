using RhyoliteERP.DomainServices.Payroll.SalaryGrades.Dto;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.SalaryGrades
{
   public interface ISalaryGradeManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(SalaryGrade input);
        Task Update(SalaryGrade input);
        Task Delete(Guid Id);
        Task<object> CreateSalaryNotch(SalaryNotchInput input);
        Task UpdateSalaryNotch(SalaryNotchInput input);
        Task DeleteSalaryNotch(SalaryNotchInput input);
    }
}
