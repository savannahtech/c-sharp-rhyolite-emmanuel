using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.AlumniHistories
{
   public interface IAlumniHistoryAppService:IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId);
    }
}
