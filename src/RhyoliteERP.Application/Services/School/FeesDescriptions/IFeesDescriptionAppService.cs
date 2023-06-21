using RhyoliteERP.Services.School.FeesDescriptions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.FeesDescriptions
{
   public interface IFeesDescriptionAppService : Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<IEnumerable<object>> ListAll(Guid id);
        Task<object> Create(CreateFeesDescriptionInput input);
        Task Update(UpdateFeesDescriptionInput input);
        Task Delete(Guid Id);
    }
}
