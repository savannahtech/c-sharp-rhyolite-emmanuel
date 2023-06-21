using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.CancelledBills
{
   public interface ICancelledBillAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);

    }
}
