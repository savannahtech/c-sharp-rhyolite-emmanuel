using RhyoliteERP.Services.School.StudentAttendances.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentAttendances
{
   public interface IStudentAttendanceAppService: Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(CreateStudentAttendanceInput input);
        Task MarkSingle(int tenantId, string biometricId);
        Task Delete(Guid id);

        //enquiry
        Task<IEnumerable<object>> ListAll(Guid classId, DateTime startDate, DateTime endDate);

    }
}
