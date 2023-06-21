using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentAttendances
{
   public interface IStudentAttendanceManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(StudentAttendance entity);
        Task MarkSingle(int tenantId, string biometricId);
        Task Delete(Guid id);

        //enquiry
        Task<IEnumerable<object>> ListAll(Guid classId, DateTime startDate, DateTime endDate);
    }
}
