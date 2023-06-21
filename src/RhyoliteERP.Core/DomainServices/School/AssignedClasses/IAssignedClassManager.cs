using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AssignedClasses
{
   public interface IAssignedClassManager : Abp.Domain.Services.IDomainService
    {
        Task<object> Create(AssignedClass input);
        Task Delete(Guid Id);
        Task<IEnumerable<object>> ListAll();
    }
}
