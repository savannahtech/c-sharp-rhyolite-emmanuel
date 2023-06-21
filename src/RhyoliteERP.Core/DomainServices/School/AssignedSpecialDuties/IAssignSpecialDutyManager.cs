using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AssignedSpecialDuties
{
   public interface IAssignSpecialDutyManager: Abp.Domain.Services.IDomainService
    {
        Task<object> Create(AssignSpecialDuty input);
        Task Delete(Guid Id);
        Task<IEnumerable<object>> ListAll();
    }
}
