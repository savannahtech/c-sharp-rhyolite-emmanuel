using Abp.Application.Services;
using RhyoliteERP.Services.School.Conducts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Conducts
{
   public interface IConductAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(CreateConductInput input);
        Task Update(UpdateConductInput input);
        Task Delete(Guid Id);
    }
}
