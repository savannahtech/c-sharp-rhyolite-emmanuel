using RhyoliteERP.Services.School.Parents.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Parents
{
   public interface IParentAppService: Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid id);
        Task<IEnumerable<object>> ListAll();
        Task<IEnumerable<object>> ListParentWards();
        Task<object> GetParentInfo(Guid id);
        Task<object> Create(CreateParentInput input);
        Task<object> Update(UpdateParentInput input);
        Task Delete(Guid Id);
        Task<IEnumerable<object>> ListParents();
    }
}
