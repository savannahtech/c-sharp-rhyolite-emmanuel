using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.BillSetups
{
   public interface IBillSetupManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<object> GetAsync(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<object> Create(BillSetup entity);
        //Task UpdateDetail(Guid Id);
        Task DeleteDetail(Guid Id, Guid headerId);
        Task Delete(Guid Id);
    }
}
