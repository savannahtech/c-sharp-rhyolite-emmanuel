using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentStatuses
{
   public interface IStudentStatusManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(StudentStatus entity);
        Task<object> Update(StudentStatus entity);
        Task Delete(Guid Id);
    }
}
