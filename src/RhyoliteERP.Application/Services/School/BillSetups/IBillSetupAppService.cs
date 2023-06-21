using Abp.Application.Services;
using RhyoliteERP.Services.School.BillSetups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.BillSetups
{
   public interface IBillSetupAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<object> GetAsync(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<object> Create(CreateBillSetupInput entity);
        Task DeleteDetail(Guid Id, Guid headerId);
        Task Delete(Guid id);
    }
}
