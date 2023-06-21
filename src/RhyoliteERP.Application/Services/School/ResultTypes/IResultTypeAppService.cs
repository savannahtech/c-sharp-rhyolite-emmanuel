using Abp.Application.Services;
using RhyoliteERP.Services.School.ResultTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.ResultTypes
{
   public interface IResultTypeAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid levelId);
        Task<object> Create(CreateResultTypeInput input);
        Task Update(UpdateResultTypeInput input);
        Task Delete(Guid id);
        Task<IEnumerable<object>> ListByClass(Guid classId);
    }
}
