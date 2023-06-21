using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AssignedDesignations
{
   public interface IAssignedDesignationManager : Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(AssignedDesignation input);
        Task Update(AssignedDesignation input);
        Task Delete(Guid Id);
    }
}
