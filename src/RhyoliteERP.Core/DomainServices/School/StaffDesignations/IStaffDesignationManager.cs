using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StaffDesignations
{
   public interface IStaffDesignationManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(StaffDesignation entity);
        Task Update(StaffDesignation entity);
        Task Delete(Guid Id);
    }
}
