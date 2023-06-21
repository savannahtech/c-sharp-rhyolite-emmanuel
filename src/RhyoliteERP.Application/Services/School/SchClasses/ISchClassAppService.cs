using Abp.Application.Services;
using RhyoliteERP.Services.School.SchClasses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SchClasses
{
   public interface ISchClassAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid levelId);
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CreateClassInput input);
        Task<object> GetAsync(Guid id);
        Task Update(UpdateClassInput input);
        Task Delete(Guid Id);
    }
}
