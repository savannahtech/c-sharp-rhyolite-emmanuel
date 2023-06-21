using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RhyoliteERP.DomainServices.School.Reports;
using RhyoliteERP.DomainServices.School.Reports.Dto;
using RhyoliteERP.DomainServices.School.ResultsUploads;
using RhyoliteERP.DomainServices.School.ResultTypes;
using RhyoliteERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Reports
{
   public class SmReportAppService: RhyoliteERPAppServiceBase, ISmReportAppService
    {
        private readonly IReportManager _reportManager;
        private readonly IResultTypeManager _resultTypeManager;
        private readonly IMapper _mapper;
        public SmReportAppService(IReportManager reportManager, IResultTypeManager resultTypeManager, IMapper mapper)
        {
            _reportManager = reportManager;
            _resultTypeManager = resultTypeManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListSchProfile(string title)
        {
            return await _reportManager.ListSchProfile(title);
        }

        public async Task<IEnumerable<object>> ListStudsByClass(Guid classId)
        {
            return await _reportManager.ListStudsByClass(classId);
        }

        public async Task<IEnumerable<object>> ListStudsByNationality(Guid nationalityId)
        {
            return await _reportManager.ListStudsByNationality(nationalityId);

        }


        public async Task<IEnumerable<object>> ListBillSetups(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _reportManager.ListBillSetups(academicYearId, termId, classId, billTypeId);
        }

        public async Task<IEnumerable<object>> ListStudsByReligion(Guid religionId)
        {
            return await _reportManager.ListStudsByReligion(religionId);
        }

        public async Task<IEnumerable<object>> ListStudentsPayments(Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {
            return await _reportManager.ListStudentsPayments(academicYearId, termId, classId, studentId);
        }
        public async Task<IEnumerable<object>> ListAttendanceSummary(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _reportManager.ListAttendanceSummary(academicYearId, termId, classId);
        }

        public async Task<IEnumerable<object>> ListEnrollmentByGender()
        {
            return await _reportManager.ListEnrollmentByGender();
        }

        public async Task<IEnumerable<object>> ExportStudentExams()
        {
            return await _reportManager.ExportStudentExams();
        }

        public async Task<IEnumerable<object>> ExportStudentOpeningBalance()
        {
            return await _reportManager.ExportStudentOpeningBalance();
        }

        public async Task<IEnumerable<object>> ListPaidupStudents(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _reportManager.ListPaidupStudents(academicYearId, termId, classId);
        }

        public async Task<IEnumerable<object>> ListCancelledBills(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _reportManager.ListCancelledBills(academicYearId, termId, classId, billTypeId);
        }

        public async Task<IEnumerable<object>> ListCancelledPayments(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _reportManager.ListCancelledPayments(academicYearId, termId, classId);
        }
        public async Task<IEnumerable<object>> ListStudentDebtors(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _reportManager.ListStudentDebtors(academicYearId, termId, classId);
        }

        public async Task<IEnumerable<object>> ListStudentBalances(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _reportManager.ListStudentBalances(academicYearId, termId, classId);
        }

        public async Task<IEnumerable<object>> ListDailyPayments(DateTime paymentDate)
        {
            return await _reportManager.ListDailyPayments(paymentDate);
        }

        public async Task<object> GetReceipt(Guid academicYearId, Guid termId, Guid classId, Guid studentId, string receiptNo)
        {
            return await _reportManager.GetReceipt(academicYearId, termId, classId, studentId, receiptNo);
        }

        public async Task<IEnumerable<object>> ListCreditMemos(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _reportManager.ListCreditMemos(academicYearId, termId, classId);
        }

        public async Task<IEnumerable<object>> ListTerminalSubjectResults(Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId)
        {
            return await _reportManager.ListTerminalSubjectResults(academicYearId, termId, classId, subjectId, resultTypeId);
        }
        public async Task<IEnumerable<object>> ListAllAlumni(Guid academicYearId)
        {
            return await _reportManager.ListAllAlumni(academicYearId);
        }

        public async Task<IEnumerable<object>> ListAlumniBalances(Guid academicYearId)
        {
            return await _reportManager.ListAlumniBalances(academicYearId);
        }

        public async Task<object> GetTerminalReport(Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {
            return await _reportManager.GetTerminalReport(academicYearId, termId, classId, studentId);
        }

        public async Task<object> GetTerminalReportSummary(Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {
            return await _reportManager.GetTerminalReportSummary(academicYearId, termId, classId, studentId);
        }

        [HttpGet]
        public async Task<object> GetReportHeader(string title, int tenantId)
        {
            return await _reportManager.GetReportHeader(title, tenantId);
        }


        [HttpGet]
        public async Task<IEnumerable<object>> GetStudsByClass(Guid classId, int tenantId)
        {
            return await _reportManager.GetStudsByClass(classId, tenantId);
        }

        [HttpGet]
        public async Task<object> FindStudentBillByTenant(Guid academicYearId, Guid termId, Guid classId, Guid studentId, int tenantId)
        {
            return await _reportManager.FindStudentBillByTenant(academicYearId, termId, classId, studentId, tenantId);

        }
        

        [HttpGet]
        public async Task<object> GetResultTypes(int tenantId)
        {
            return await _resultTypeManager.ListAll(tenantId);
        }

        [HttpGet]
        public async Task<IEnumerable<object>> ListResultUploads(Guid academicYearId, Guid termId, Guid classId, int tenantId)
        {
            return await _reportManager.GetResultUploads(academicYearId, termId, classId,tenantId);
        }

        [HttpPost]
        public async Task UpdateTerminalReport([FromBody] TerminalReportDto dto)
        {
             await _reportManager.UpdateTerminalReport(dto);
        }


    }
}
 