using Abp.Application.Services;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.StudentBills.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentBills
{
   public interface IStudentBillAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<IEnumerable<object>> ListBillsToCancel(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task Delete(Guid id);
        Task CreateBatch(List<CreateStudentBillInput> billList);
        Task CancelBatch(List<CancelStudentBillInput> billList);
        Task<IEnumerable<object>> ListAllBalances(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListOpeningBalancesToCancel(Guid academicYearId, Guid termId, Guid classId);
        Task<object> GetCurrentBillDetails(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<IEnumerable<object>> ListStudentBills(Guid studentId);
        Task<object> GetBill(Guid id);
        //Enquiry
        Task<IEnumerable<object>> GetPaidUpStudents(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> GetStudentDebtors(Guid academicYearId, Guid termId, Guid classId);
        Task CreateOpeningBalance(CreateStudentBillInput studentBill);
    }
}
