using RhyoliteERP.Services.Payroll.DeductionTypes.Dto;
using RhyoliteERP.Services.Payroll.Gratuities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.Gratuities
{
    public interface IGratuityAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateGratuityInput input);
        Task Update(UpdateGratuityInput input);
        Task Delete(Guid Id);
    }
}
