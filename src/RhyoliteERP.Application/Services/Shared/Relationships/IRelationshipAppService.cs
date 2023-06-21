using Abp.Application.Services;
using RhyoliteERP.Services.Shared.Relationships.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Relationships
{
   public interface IRelationshipAppService : IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CreateRelationshipInput input);
        Task Update(UpdateRelationshipInput input);
        Task Delete(Guid Id);
    }
}
