using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.ResultTypes
{
   public interface IResultTypeManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid levelId);
        Task<object> Create(ResultType input);
        Task Update(ResultType input);
        Task Delete(Guid id);
        Task<IEnumerable<object>> ListByClass(Guid classId);
        Task<IEnumerable<object>> ListAll(int tenantId);
    }
}
