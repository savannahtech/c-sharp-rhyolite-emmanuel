using RhyoliteERP.DomainServices.Payroll.DeductionTypes.Dto;
using RhyoliteERP.Services.Payroll.DeductionTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.DeductionTypes
{
   public interface IDeductionTypeAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> GetByCategory(Guid categoryId);
        Task<object> ListAll(Guid deductionTypeId,Guid employeeId, Guid categoryId);
        Task<object> Create(CreateDeductionTypeInput input);
        Task Update(UpdateDeductionTypeInput input);
        Task Delete(Guid Id);

        Task DeleteDeductionRate(DeductionRateInput input);
        Task UpdateDeductionRate(DeductionRateInput input);
        Task<object> CreateDeductionRate(DeductionRateInput input);

        //

    }
}
