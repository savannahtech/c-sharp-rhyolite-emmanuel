using RhyoliteERP.Services.School.Attitudes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Attitudes
{
   public interface IAttitudeAppService: Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(CreateAttitudeInput input);
        Task Update(UpdateAttitudeInput input);
        Task Delete(Guid Id);
    }
}
