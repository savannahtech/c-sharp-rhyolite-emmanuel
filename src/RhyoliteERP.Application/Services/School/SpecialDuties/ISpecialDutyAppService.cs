using Abp.Application.Services;
using RhyoliteERP.Services.School.SpecialDuties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SpecialDuties
{
   public interface ISpecialDutyAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CreateSpecialDutyInput input);
        Task Update(UpdateSpecialDutyInput input);
        Task Delete(Guid Id);
    }
}
