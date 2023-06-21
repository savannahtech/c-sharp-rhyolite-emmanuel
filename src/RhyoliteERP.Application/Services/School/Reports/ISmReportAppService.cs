using Abp.Application.Services;
using RhyoliteERP.DomainServices.School.Reports.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Reports
{
   public interface ISmReportAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListSchProfile(string title);
        Task<IEnumerable<object>> ListStudsByClass(Guid classId);
        Task<IEnumerable<object>> ListStudsByNationality(Guid nationalityId);
        Task<IEnumerable<object>> ListStudsByReligion(Guid religionId);
        Task<IEnumerable<object>> ListAttendanceSummary(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListEnrollmentByGender();
        Task<IEnumerable<object>> ExportStudentExams();
        Task<IEnumerable<object>> ExportStudentOpeningBalance();
        Task<IEnumerable<object>> ListBillSetups(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<IEnumerable<object>> ListPaidupStudents(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListStudentsPayments(Guid academicYearId, Guid termId, Guid classId, Guid studentId);
        Task<IEnumerable<object>> ListStudentDebtors(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListDailyPayments(DateTime paymentDate);
        Task<object> GetReceipt(Guid academicYearId, Guid termId, Guid classId, Guid studentId, string receiptNo);
        Task<IEnumerable<object>> ListCreditMemos(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListStudentBalances(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListCancelledBills(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId);
        Task<IEnumerable<object>> ListCancelledPayments(Guid academicYearId, Guid termId, Guid classId);
        Task<IEnumerable<object>> ListTerminalSubjectResults(Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId);
        Task<object> GetTerminalReport(Guid academicYearId, Guid termId, Guid classId, Guid studentId);
        Task<object> GetTerminalReportSummary(Guid academicYearId, Guid termId, Guid classId, Guid studentId);
        Task<IEnumerable<object>> ListAllAlumni(Guid academicYearId);
        Task<IEnumerable<object>> ListAlumniBalances(Guid academicYearId);
        Task<IEnumerable<object>> ListResultUploads(Guid academicYearId, Guid termId, Guid classId, int tenantId);
        Task<object> GetReportHeader(string title, int tenantId);
        Task UpdateTerminalReport(TerminalReportDto dto);
    }
}
