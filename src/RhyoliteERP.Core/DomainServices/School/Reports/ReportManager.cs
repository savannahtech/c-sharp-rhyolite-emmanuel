using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhyoliteERP.DomainServices.School.Reports.Dto;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Reports
{
   public class ReportManager: Abp.Domain.Services.DomainService, IReportManager
    {
        private readonly IRepository<SchoolProfile, Guid> _repositorySchoolProfile;
        private readonly IRepository<Student, Guid> _repositoryStudent;
        private readonly IRepository<StudentAttendance, Guid> _repositoryStudentAttendance;
        private readonly IRepository<StudentBill, Guid> _repositoryStudentBill;
        private readonly IRepository<BillPayment, Guid> _repositoryBillPayment;
        private readonly IRepository<BillSetup, Guid> _repositoryBillSetup;
        private readonly IRepository<CancelledBill, Guid> _repositoryCancelledBill;
        private readonly IRepository<CancelledPayment, Guid> _repositoryCancelledPayment;
        private readonly IRepository<ResultsUpload, Guid> _repositoryResultsUpload;
        private readonly IRepository<AlumniHistory, Guid> _repositoryAlumniHistory;
        private readonly IRepository<AcademicYear, Guid> _repositoryAcademicYear;
        private readonly IRepository<SchClass, Guid> _repositorySchClass;
        private readonly IRepository<ResultType, Guid> _repositoryResultType;
        private readonly IRepository<TerminalReport, Guid> _repositoryTerminalReport;
        private readonly IRepository<Conduct, Guid> _repositoryConduct;
        private readonly IRepository<TeacherRemark, Guid> _repositoryTeacherRemarks;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ReportManager(IRepository<SchoolProfile, Guid> repositorySchoolProfile, IRepository<Student, Guid> repositoryStudent, IRepository<StudentAttendance, Guid> repositoryStudentAttendance, IRepository<StudentBill, Guid> repositoryStudentBill, IRepository<BillPayment, Guid> repositoryBillPayment, IRepository<BillSetup, Guid> repositoryBillSetup, IRepository<CancelledBill, Guid> repositoryCancelledBill, IRepository<CancelledPayment, Guid> repositoryCancelledPayment, IRepository<ResultsUpload, Guid> repositoryResultsUpload, IRepository<AlumniHistory, Guid> repositoryAlumniHistory, IUnitOfWorkManager unitOfWorkManager, IRepository<AcademicYear, Guid> repositoryAcademicYear, IRepository<SchClass, Guid> repositorySchClass, IRepository<ResultType, Guid> repositoryResultType, IRepository<TerminalReport, Guid> repositoryTerminalReport, IBasicAcademicInfoManager basicAcademicInfoManager, IRepository<Conduct, Guid> repositoryConduct, IRepository<TeacherRemark, Guid> repositoryTeacherRemarks)
        {
            _repositorySchoolProfile = repositorySchoolProfile;
            _repositoryStudent = repositoryStudent;
            _repositoryStudentAttendance = repositoryStudentAttendance;
            _repositoryStudentBill = repositoryStudentBill;
            _repositoryBillPayment = repositoryBillPayment;
            _repositoryBillSetup = repositoryBillSetup;
            _repositoryCancelledBill = repositoryCancelledBill;
            _repositoryCancelledPayment = repositoryCancelledPayment;
            _repositoryResultsUpload = repositoryResultsUpload;
            _repositoryAlumniHistory = repositoryAlumniHistory;
            _unitOfWorkManager = unitOfWorkManager;
            _repositoryAcademicYear = repositoryAcademicYear;
            _repositorySchClass = repositorySchClass;
            _repositoryResultType = repositoryResultType;
            _repositoryTerminalReport = repositoryTerminalReport;
            _basicAcademicInfoManager = basicAcademicInfoManager;
            _repositoryConduct = repositoryConduct;
            _repositoryTeacherRemarks = repositoryTeacherRemarks;
        }

        public async Task<IEnumerable<object>> ListSchProfile(string title)
        {
            var datta = await _repositorySchoolProfile.GetAllListAsync();
            return datta.Select(a => new { a.SchoolName, PrimaryPhoneNo = $"Tel: {a.PrimaryPhoneNo}", SchAddress = a.PostalAddress, Email = $"Email: {a.PrimaryEmailAddress}", ReportTitle = title }).ToList();
        }

        public async Task<IEnumerable<object>> ListStudsByClass(Guid classId)
        {

            var studs = await _repositoryStudent.GetAllListAsync(x=> x.ClassId == classId);

            return (from u1 in studs
                    select new
                    {
                        FullName = u1.MiddleName == null ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.MiddleName}",
                        EnrollmentDate = u1.EnrollmentDate.ToString("dd-MMM-yyyy"),
                        u1.StudentIdentifier,
                        u1.Gender,
                        u1.ClassId,
                        DateOfBirth = u1.DateOfBirth.ToString("dd-MMM-yyyy"),

                    }).OrderBy(a => a.FullName).ToList();

        }

        public async Task<IEnumerable<object>> ListStudsByNationality(Guid nationalityId)
        {
            var studs = await _repositoryStudent.GetAllListAsync(x => x.NationalityId == nationalityId);

            var query = from u1 in studs
                        select new
                        {
                            u1.Gender,
                            u1.StudentIdentifier,
                            u1.ReligionName,u1.ClassName,
                             u1.AcademicYearName,
                            u1.StudentStatusName,
                            StudentName = u1.MiddleName == null ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.MiddleName}",
                        };

            return query.ToList();
        }

        public async Task<IEnumerable<object>> ListStudsByReligion(Guid religionId)
        {

            var studs = await _repositoryStudent.GetAllListAsync(x => x.ReligionId == religionId);

            var query = from u1 in studs
                        select new
                        {
                            u1.Gender,
                            u1.StudentIdentifier,
                            u1.ReligionName,
                            u1.ClassName,
                            u1.AcademicYearName,
                            u1.StudentStatusName,
                            StudentName = u1.MiddleName == null ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.MiddleName}",

                        };

            return query.ToList();
        }


        public async Task<IEnumerable<object>> ListAttendanceSummary(Guid academicYearId, Guid termId, Guid classId)
        {
            var studentAttendance = await _repositoryStudentAttendance.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId);
            
            return (from u1 in studentAttendance
                    select new
                    {
                        AttendanceDate = u1.AttendanceDate.ToString("dd-MMM-yyyy"),
                        u1.NoPresent,

                    }).OrderBy(a => a.AttendanceDate).ToList();

        }

        public async Task<IEnumerable<object>> ListEnrollmentByGender()
        {
            var students = await _repositoryStudent.GetAllListAsync();

            return (from u1 in students
                    group u1 by new { u1.ClassName, u1.Gender } into datta
                    select new
                    {
                        datta.Key.ClassName,
                        datta.Key.Gender,
                        Total = datta.Count()

                    }).ToList().OrderBy(a => a.ClassName);
        }

        public async Task<IEnumerable<object>> ExportStudentExams()
        {
            var students = await _repositoryStudent.GetAllListAsync();

            return (from u1 in students
                    select new
                    {
                        FullName = u1.MiddleName == null ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.MiddleName}",
                        u1.StudentIdentifier,
                        u1.ClassId,
                        Marks = 0,
                    }).OrderBy(a => a.FullName).ToList();

        }
        public async Task<IEnumerable<object>> ExportStudentOpeningBalance()
        {
            var students = await _repositoryStudent.GetAllListAsync();

            return (from u1 in students
                    select new
                    {
                        FullName = u1.MiddleName == null ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.MiddleName}",
                        u1.StudentIdentifier,
                        u1.ClassId,
                        Amount = 0,

                    }).OrderBy(a => a.FullName).ToList();

        }


        public async Task<IEnumerable<object>> ListPaidupStudents(Guid academicYearId, Guid termId, Guid classId)
        {
            var bills = await _repositoryStudentBill.GetAllListAsync(c => c.BillBalance <= 0 && c.AcademicYearId == academicYearId && c.TermId == termId && c.ClassId == classId);

            return (from u1 in bills
                    select new
                    {
                        u1.BillBalance, u1.StudentIdentifier, u1.StudentName,

                    }).ToList();


        }

        public async Task<IEnumerable<object>> ListStudentDebtors(Guid academicYearId, Guid termId, Guid classId)
        {
            var bills = await _repositoryStudentBill.GetAllListAsync(c => c.BillBalance > 0 && c.AcademicYearId == academicYearId && c.TermId == termId && c.ClassId == classId);

            return (from u1 in bills
                    select new
                    {
                        u1.BillBalance,
                        u1.StudentIdentifier,
                        u1.StudentName,
                    }).ToList();


        }

        public async Task<IEnumerable<object>> ListDailyPayments(DateTime paymentDate)
        {
            var payments = await _repositoryBillPayment.GetAllListAsync(x => x.PaymentDate.Date == paymentDate.Date);

            return (from u1 in payments
                    select new
                    {
                        u1.StudentIdentifier,u1.StudentName,
                        u1.AmountPaid,
                        PaymentMethod = u1.ModeOfPayment,u1.ClassName,
                        u1.ReceiptNo,

                    }).ToList();

        }

        public async Task<IEnumerable<object>> ListStudentsPayments(Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {
           return await _repositoryBillPayment.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.StudentId == studentId);

        }

        public async Task<object> GetReceipt(Guid academicYearId, Guid termId, Guid classId, Guid studentId, string receiptNo)
        {
            var bills = await _repositoryStudentBill.GetAllListAsync(x => x.StudentId == studentId);
            var balanceDue = bills.Sum(a=>a.BillBalance);
            var payment =  await _repositoryBillPayment.FirstOrDefaultAsync(c => c.ReceiptNo == receiptNo && c.AcademicYearId == academicYearId && c.TermId == termId && c.ClassId == classId);
            
            var receipt = new { balanceDue, payment };
            return receipt;
        }


        public async Task<IEnumerable<object>> ListCreditMemos(Guid academicYearId, Guid termId, Guid classId)
        {
            var payments = await _repositoryBillPayment.GetAllListAsync(c => c.IsCreditMemo && c.AcademicYearId == academicYearId && c.TermId == termId && c.ClassId == classId);
             
            var queryResult = from u1 in payments
                              select new
                              {
                                  u1.ReceiptNo,
                                  u1.StudentIdentifier,
                                  u1.StudentName,
                                  PaymentDate = u1.PaymentDate.ToString("dd-MMM-yyyy"),
                                  u1.ModeOfPayment,
                                  u1.CurrencyName,
                                  u1.AmountPaid,

                              };
            return queryResult.ToList();

        }

        public async Task<IEnumerable<object>> ListBillSetups(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            var billSetups = await _repositoryBillSetup.FirstOrDefaultAsync(c => c.AcademicYearId == academicYearId && c.TermId == termId && c.ClassId == classId && c.BillTypeId == billTypeId);
          
            if(billSetups !=null) return billSetups.Details;

            return null;

        }

        public async Task<IEnumerable<object>> ListCancelledBills(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _repositoryCancelledBill.GetAllListAsync(c => c.AcademicYearId == academicYearId && c.TermId == termId && c.ClassId == classId && c.BillTypeId == billTypeId);
            
        }

        public async Task<IEnumerable<object>> ListCancelledPayments(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _repositoryCancelledPayment.GetAllListAsync(c => c.AcademicYearId == academicYearId && c.TermId == termId && c.ClassId == classId);
             
        }

        public async Task<IEnumerable<object>> ListTerminalSubjectResults(Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId)
        {
            var results = await _repositoryResultsUpload.GetAllListAsync(c => c.AcademicYearId == academicYearId && c.TermId == termId && c.ClassId == classId && c.SubjectId == subjectId && c.ResultTypeId == resultTypeId);

            var query = from u1 in results
                        select new
                        {
                            u1.StudentIdentifier,u1.StudentName,
                            Marks = $"{u1.MarksObtained}/{u1.TotalMarks}",

                        };
            return query;

        }

        public async Task<IEnumerable<object>> ListAllAlumni(Guid academicYearId)
        {
            var alumni = await _repositoryAlumniHistory.GetAllListAsync(x => x.AcademicYearCompleted == academicYearId);
            return (from u1 in alumni

                    select new
                    {
                        u1.StudentIdentifier,
                        StudentName = u1.MiddleName == null ? $"{u1.LastName}, {u1.FirstName}" : $"{u1.LastName}, {u1.FirstName} {u1.MiddleName}",
                        CompletionDate = u1.CompletionDate.ToString("dd-MMM-yyyy"),

                    }).OrderBy(a => a.StudentName).ToList();
        }

        public async Task<IEnumerable<object>> ListAlumniBalances(Guid academicYearId)
        {
            var bills = await _repositoryStudentBill.GetAllListAsync(c => c.BillBalance > 0 && c.AcademicYearId == academicYearId);
            var students = await _repositoryAlumniHistory.GetAllListAsync(x => x.AcademicYearCompleted == academicYearId);

            return (from u1 in bills
                    join u2 in students on u1.StudentId equals u2.PrimaryId
                    select new
                    {
                        u1.StudentName,u1.StudentIdentifier,
                        u1.BillBalance,
                        CompletionDate = u2.CompletionDate.ToString("dd-MMM-yyyy"),

                    }).OrderBy(a => a.StudentName).ToList();

        }

        //student balances by class...
        public async Task<IEnumerable<object>> ListStudentBalances(Guid academicYearId, Guid termId, Guid classId)
        {
            //make default query by current AcayearId, int TermId,
            var bills = await _repositoryStudentBill.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillBalance > 0);
            var students = await _repositoryStudent.GetAllListAsync();

            return (from u1 in bills
                    join u2 in students on u1.StudentId equals u2.Id
                    select new
                    {
                        u1.StudentIdentifier,u1.StudentName,
                        u1.BillBalance,

                    }).ToList();

        }

       
        public async Task<object> FindStudentBill(Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {

            var academicYears = await _repositoryAcademicYear.FirstOrDefaultAsync(academicYearId);

            var currentTerm = academicYears.Terms.FirstOrDefault(b => b.Id == termId);

            var balanceBroughtForward = await _repositoryStudentBill.FirstOrDefaultAsync(a => a.StudentId == studentId && a.BillDate < currentTerm.StartDate);

            var studentBill = await _repositoryStudentBill.FirstOrDefaultAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.StudentId == studentId);
           
            return new {
                studentBill.BillBalance,
                studentBill.BillAmount,
                studentBill.BillNo,
                Period = $"{studentBill.TermName}, {studentBill.AcademicYearName}",
                BalanceBF = balanceBroughtForward?.BillBalance ?? 0,
                studentBill.CurrencyName,
                studentBill.CurrencyCode,
                studentBill.Details
            };

        }


        public async Task<object> GetTerminalReport(Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {
            return await _repositoryTerminalReport.FirstOrDefaultAsync(x=>x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.StudentId == studentId);
        }

        public async Task<object> GetTerminalReportSummary(Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {
            var schoolProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();

            var conductInfo = await _repositoryConduct.FirstOrDefaultAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.StudentId == studentId);

            var termlyAttendance = await _repositoryStudentAttendance.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId);

            var currentAcademicYear = await _repositoryAcademicYear.FirstOrDefaultAsync(x => x.Id == schoolProfile.CurrentAcademicYearId);

            var currentTerm = currentAcademicYear.Terms.FirstOrDefault(x => x.Id == schoolProfile.CurrentTermId);

            int schoolDays = (currentTerm.EndDate - currentTerm.StartDate).Days;

            var teacherRemarksInfo = await _repositoryTeacherRemarks.FirstOrDefaultAsync(x=>x.AcademicYearId  == academicYearId && x.TermId == termId && x.ClassId == classId && x.StudentId == studentId);

            int attendanceCount = 0;

            string conductText = string.Empty;

            string teacherRemarks = string.Empty;

            if (conductInfo != null)
            {
                conductText = conductInfo.ConductText;
            }

            if (teacherRemarksInfo != null)
            {
                teacherRemarks = teacherRemarksInfo.Remarks;
            }

            for (int i = 0; i < termlyAttendance.Count; i++)
            {
                var attendanceDetails = termlyAttendance[i].Details;
                 
                var studentAttendance = attendanceDetails.FirstOrDefault(x => x.StudentId == studentId);
                if (studentAttendance != null)
                {
                    attendanceCount ++;
                }

            }
            
            return new { attendanceCount, schoolDays, conduct = conductText, teacherRemarks };
        }


        public async Task<string> GetNextTermData(int tenantId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {

                var profile = await _repositorySchoolProfile.FirstOrDefaultAsync(x => x.TenantId == tenantId);

                var currentAcademicYear = await _repositoryAcademicYear.FirstOrDefaultAsync(profile.CurrentAcademicYearId);

                var currentTerm = currentAcademicYear.Terms.FirstOrDefault(b => b.Id == profile.CurrentTermId);

                var nextTerm = currentAcademicYear.Terms.FirstOrDefault(x => x.PrecedenceNo == currentTerm.PrecedenceNo + 1);
                if (nextTerm != null)
                {
                    return nextTerm.StartDate.ToString("dd-MMM-yyyy");
                }

                //move to next academic year
                 
                var nextAcademicYear =
                    await _repositoryAcademicYear.FirstOrDefaultAsync(x => x.PrecedenceNo == currentAcademicYear.PrecedenceNo + 1);

                if (nextAcademicYear != null)
                {
                    return nextAcademicYear.BeginDate.ToString("dd-MMM-yyyy");
                }

                return string.Empty;

            }


        }

        public async Task<Dictionary<string, object>> GetMarksTotals(Guid academicYearId, Guid termId, Guid classId, int tenantId)
        {

            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {

                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

                var classInfo = await _repositorySchClass.FirstOrDefaultAsync(x => x.Id == classId);
                var resultTypes = await _repositoryResultType.GetAllListAsync(a => a.LevelId == classInfo.LevelId);

                foreach (var resultType in resultTypes)
                {
                    var acaResults = await _repositoryResultsUpload.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.ResultTypeId == resultType.Id);
                    keyValuePairs.Add(resultType.Name, acaResults);

                }

                return keyValuePairs;
            }


        }

        public async Task<object> GetReportHeader(string title, int tenantId)
        {
            //Switch to tenantId
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                var data = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();

                return new { data.SchoolName, PrimaryPhoneNo = $"Tel: {data.PrimaryPhoneNo}", SchAddress = $"{data.PostalAddress}", Email = $"Email: {data.PrimaryEmailAddress}", LogoUrl = data.SchoolLogoUrl, ReportTitle = title };

            }

        }
        public async Task<object> FindStudentBillByTenant(Guid academicYearId, Guid termId, Guid classId, Guid studentId, int tenantId)
        {

            //Switch to tenantId
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {

                var academicYears = await _repositoryAcademicYear.FirstOrDefaultAsync(academicYearId);

                var currentTerm = academicYears.Terms.FirstOrDefault(b => b.Id == termId);

                var balanceBroughtForward = await _repositoryStudentBill.GetAllListAsync(a => a.StudentId == studentId && currentTerm.StartDate < a.BillDate);

                var studentBill = await _repositoryStudentBill.FirstOrDefaultAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.StudentId == studentId);

                return new
                {
                    studentBill.BillBalance,
                    studentBill.BillAmount,
                    studentBill.BillNo,
                    Period = $"{studentBill.TermName}, {studentBill.AcademicYearName}",
                    BalanceBF = balanceBroughtForward.Any() ? balanceBroughtForward.Sum(a => a.BillBalance) : 0,
                    //studentBill.CurrencyName,
                    //studentBill.CurrencyCode,
                    CurrencyName = "Ghana Cedi",
                    CurrencyCode = "GHS",
                    studentBill.Details,
                    studentBill.BillDate,
                    PaymentChannels = new List<string>(),
                };
            }

        }

        public async Task<IEnumerable<object>> GetStudsByClass(Guid classId,int tenantId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {

                var studs = await _repositoryStudent.GetAllListAsync(x => x.ClassId == classId);

                return (from u1 in studs
                        select new
                        {
                            FullName = u1.MiddleName == null ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.MiddleName}",
                            EnrollmentDate = u1.EnrollmentDate.ToString("dd-MMM-yyyy"),
                            u1.StudentIdentifier,
                            u1.Gender,
                            u1.ClassId, u1.ClassName,
                            u1.Id,
                            DateOfBirth = u1.DateOfBirth.ToString("dd-MMM-yyyy"),

                        }).OrderBy(a => a.FullName).ToList();
            }
           
        }

        public async Task<IEnumerable<object>> GetResultUploads(Guid academicYearId, Guid termId, Guid classId, int tenantId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            { 
                return await _repositoryResultsUpload.GetAllListAsync(x=>x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId);
            }
        }
 
        public async Task UpdateTerminalReport(TerminalReportDto dto)
        {
            //Logger.Info($"studentsFinalResult => {JsonConvert.SerializeObject(dto)}");

            using (_unitOfWorkManager.Current.SetTenantId(dto.TenantId))
            {

                if (dto.StudentsFinalResult.Any())
                {

                    foreach (var finalResult in dto.StudentsFinalResult)
                    {
                        var terminalReport = await _repositoryTerminalReport.FirstOrDefaultAsync(x => x.AcademicYearId == finalResult.AcademicYearId && x.TermId == finalResult.TermId && x.ClassId == finalResult.ClassId && x.StudentId == finalResult.StudentId);

                        if (terminalReport != null)
                        {
                            List<SubjectResult> newSubjectResultList = new List<SubjectResult>();

                            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(finalResult.AcademicYearId, finalResult.TermId, finalResult.ClassId, finalResult.StudentId);

                            finalResult.FinalResults.ForEach(z => {

                                newSubjectResultList.Add(new SubjectResult
                                {

                                    AcademicYearId = z.AcademicYearId,
                                    TermId = z.TermId,
                                    ClassId = z.ClassId,
                                    ClassScore = z.ClassScore,
                                    ExamScore = z.ExamScore,
                                    StudentId = z.StudentId,
                                    SubjectId = z.SubjectId,
                                    SubjectName = "",
                                    SubjectRemarks = "",
                                    TotalScore = 0,

                                });
                            });

                            terminalReport.SubjectResults = newSubjectResultList;

                            await _repositoryTerminalReport.UpdateAsync(terminalReport);

                        }
                        else
                        {
                            List<SubjectResult> newSubjectResultList = new List<SubjectResult>();

                            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(finalResult.AcademicYearId, finalResult.TermId, finalResult.ClassId, finalResult.StudentId);

                            finalResult.FinalResults.ForEach(z=> {

                                newSubjectResultList.Add(new SubjectResult { 

                                    AcademicYearId = z.AcademicYearId, 
                                    TermId = z.TermId, 
                                    ClassId = z.ClassId, 
                                    ClassScore = z.ClassScore, 
                                    ExamScore = z.ExamScore, 
                                    StudentId = z.StudentId, 
                                    SubjectId = z.SubjectId, 
                                    SubjectName = "", 
                                    SubjectRemarks = "", 
                                    TotalScore = 0,

                                });
                            });

                            var tr = new TerminalReport
                            {
                                AcademicYearId = finalResult.AcademicYearId,
                                TermId = finalResult.TermId,
                                ClassId = finalResult.ClassId,
                                StudentId = finalResult.StudentId,
                                StudentIdentifier = basicInfo.StudentIdentifier,
                                StudentName = basicInfo.StudentName,
                                AcademicYearName = basicInfo.AcademicYearName,
                                ClassName = basicInfo.ClassName,
                                TermName = basicInfo.TermName,
                                Position = 0,
                                TermAverage = 0,
                                SubjectResults = newSubjectResultList,
                                Attendance = 0,
                                Attitude = "",
                                ClassTeacher = "",
                                ClassTeacherRemarks = "",
                                Conduct = "",
                                HeadTeacherSignatureImageUrl = "",
                                NextTermBegins = "",
                                PromotedTo = "",
                                NumberOnRoll = 0,
                                TotalAttendanceDays = 0,
                                TenantId = dto.TenantId,
                            };

                            await _repositoryTerminalReport.InsertAsync(tr);
                        }


                    }


                }

            }
        }

    }
}
