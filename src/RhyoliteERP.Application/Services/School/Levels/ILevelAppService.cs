using Abp.Application.Services;
using RhyoliteERP.Services.School.Levels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Levels
{
   public interface ILevelAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CreateLevelInput entity);
        Task Update(UpdateLevelInput entity);
        Task Delete(Guid Id);
    }
}
