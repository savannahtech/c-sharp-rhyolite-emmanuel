using AutoMapper;
using RhyoliteERP.DomainServices.School.StudentBills;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.StudentBills.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentBills
{
   public class StudentBillAppService: RhyoliteERPAppServiceBase, IStudentBillAppService
    {
        private readonly IStudentBillManager _studentBillManager;
        private readonly IMapper _mapper;

        public StudentBillAppService(IStudentBillManager studentBillManager, IMapper mapper)
        {
            _studentBillManager = studentBillManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _studentBillManager.ListAll(academicYearId, termId, classId, billTypeId);
        }

        public async Task<IEnumerable<object>> ListBillsToCancel(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _studentBillManager.ListBillsToCancel(academicYearId, termId, classId, billTypeId);
        }


        public async Task Delete(Guid id)
        {
            await _studentBillManager.Delete(id);
        }

        public async Task CreateBatch(List<CreateStudentBillInput> billList)
        {
            foreach (var studentBill in billList)
            {
                var obj = _mapper.Map<StudentBill>(studentBill);
                await _studentBillManager.Create(obj);
            }

        }

        public async Task CancelBatch(List<CancelStudentBillInput> billList)
        {
            foreach (var studentBill in billList)
            {
                var obj = _mapper.Map<StudentBill>(studentBill);
                await _studentBillManager.CancelBill(obj);
            }

        }

        
         
        public async Task<IEnumerable<object>> ListAllBalances(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _studentBillManager.ListAllBalances(academicYearId, termId, classId);
        }

        public async Task<object> GetCurrentBillDetails(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _studentBillManager.GetCurrentBillDetails(academicYearId, termId, classId, billTypeId);
        }

        public async Task<IEnumerable<object>> ListStudentBills(Guid studentId)
        {
            return await _studentBillManager.ListStudentBills(studentId);

        }

        public async Task<IEnumerable<object>> ListOpeningBalancesToCancel(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _studentBillManager.ListOpeningBalancesToCancel(academicYearId, termId, classId);
        }

        public async Task<object> GetBill(Guid id)
        {
            return await _studentBillManager.GetBill(id);
        }


        public async Task<IEnumerable<object>> GetPaidUpStudents(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _studentBillManager.GetPaidUpStudents(academicYearId, termId, classId);

        }

        public async Task<IEnumerable<object>> GetStudentDebtors(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _studentBillManager.GetStudentDebtors(academicYearId, termId, classId);

        }

        public async Task CreateOpeningBalance(CreateStudentBillInput studentBill)
        {
            var obj = _mapper.Map<StudentBill>(studentBill);

            await _studentBillManager.CreateOpeningBalance(obj);
        }
    }
}
