using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Parents
{
   public interface IParentManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid id);
        Task<IEnumerable<object>> ListAll();
        Task<IEnumerable<object>> ListParentWards();
        Task<object> GetParentInfo(Guid id);
        Task<object> Create(Parent input);
        Task<object> Update(Parent input);
        Task Delete(Guid Id);
        Task<IEnumerable<object>> ListParents();
    }
}
