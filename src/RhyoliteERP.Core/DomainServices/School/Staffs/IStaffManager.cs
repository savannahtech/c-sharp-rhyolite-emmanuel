using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Staffs
{
   public interface IStaffManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(bool isTeachingStaff);
        Task<object> GetAsync(Guid id);
        Task<object> Create(Staff input);
        Task Update(Staff input);
        Task Delete(Guid Id);
    }
}
