using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentParents
{
   public interface IStudentParentManager: Abp.Domain.Services.IDomainService
    {
        Task<object> GetStudentParent(Guid id);
        Task Create(StudentParent entity);
        Task Update(StudentParent entity);
        Task Delete(Guid Id);

        //list parent's children
        Task<IEnumerable<Guid>> ListChildren(Guid parentId);
    }
}
