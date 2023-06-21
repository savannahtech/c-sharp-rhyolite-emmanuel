using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentBills
{
   public interface IStudentBillManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<IEnumerable<object>> ListBillsToCancel(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task Delete(Guid id);
        Task CreateBatch(List<StudentBill> BillHeaders);
        Task Create(StudentBill studentBill);
        Task<IEnumerable<object>> ListAllBalances(Guid academicYearId, Guid termId, Guid classId);
        Task<object> GetCurrentBillDetails(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<IEnumerable<object>> ListOpeningBalancesToCancel(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListStudentBills(Guid studentId);
        Task<object> GetBill(Guid id);
        //Enquiry
        Task<IEnumerable<object>> GetPaidUpStudents(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> GetStudentDebtors(Guid academicYearId, Guid termId, Guid classId);
        Task CancelBill(StudentBill bill);
        Task CreateOpeningBalance(StudentBill studentBill);
    }
}
