using Abp.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.StudentBills
{
   public class StudentBillManager: Abp.Domain.Services.DomainService, IStudentBillManager
    {
        private readonly IRepository<StudentBill, Guid> _repositoryStudentBill;
        private readonly IRepository<CancelledBill, Guid> _repositoryCancelledBill;
        private readonly IRepository<BillSetup, Guid> _repositoryBillSetup;
        private readonly IRepository<BillType, Guid> _repositoryBillType;
        private readonly IRepository<StudentStatement, Guid> _repositoryStudentStatement;
        private readonly IRepository<Currency, Guid> _repositoryCurrency;
        private readonly IRepository<CompanyProfile, Guid> _repositoryCompanyProfile;
        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;
        private readonly IMapper _mapper;

        public StudentBillManager(IRepository<StudentBill, Guid> repositoryStudentBill, IRepository<StudentStatement, Guid> repositoryStudentStatement, IRepository<Currency, Guid> repositoryCurrency, IRepository<CompanyProfile, Guid> repositoryCompanyProfile, IRepository<BillSetup, Guid> repositoryBillSetup, IBasicAcademicInfoManager basicAcademicInfoManager, IRepository<BillType, Guid> repositoryBillType, IRepository<CancelledBill, Guid> repositoryCancelledBill, IMapper mapper)
        {
            _repositoryStudentBill = repositoryStudentBill;
            _repositoryStudentStatement = repositoryStudentStatement;
            _repositoryCurrency = repositoryCurrency;
            _repositoryCompanyProfile = repositoryCompanyProfile;
            _repositoryBillSetup = repositoryBillSetup;
            _basicAcademicInfoManager = basicAcademicInfoManager;
            _repositoryBillType = repositoryBillType;
            _repositoryCancelledBill = repositoryCancelledBill;
            _mapper = mapper;
        }

        public async Task Delete(Guid id)
        {
            var bill = await _repositoryStudentBill.FirstOrDefaultAsync(x => x.Id == id && x.BillStatus == 301);
            if (bill != null)
            {
                var statementRecord = await _repositoryStudentStatement.FirstOrDefaultAsync(x => x.StudentId == bill.StudentId);
                var statementList = statementRecord.Statement;
                var recordToDelete = statementList.FirstOrDefault(a => a.ReferenceNo == bill.BillNo);
                statementList.Remove(recordToDelete);
                statementRecord.Statement = statementList;
                await _repositoryStudentStatement.UpdateAsync(statementRecord);
                await _repositoryStudentBill.DeleteAsync(id);
            }

        }
        

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _repositoryStudentBill.GetAllListAsync(x=>x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillTypeId == billTypeId);
        }

        public async Task<IEnumerable<object>> ListBillsToCancel(Guid academicYearId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _repositoryStudentBill.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillTypeId == billTypeId && x.BillStatus == 301);
        }

        public async Task<IEnumerable<object>> ListOpeningBalancesToCancel(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _repositoryStudentBill.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillTypeId == Guid.Empty && x.BillSetupId == Guid.Empty);
        }

        public async Task<object> GetCurrentBillDetails(Guid acaYrId, Guid termId, Guid classId, Guid billTypeId)
        {
            return await _repositoryStudentBill.FirstOrDefaultAsync(a => a.AcademicYearId == acaYrId && a.TermId == termId && a.ClassId == classId && a.BillTypeId == billTypeId);
        }

        public async Task Create(StudentBill studentBill)
        {

            Currency currency = null;
            var companyProfileInfo = await _repositoryCompanyProfile.GetAll().FirstOrDefaultAsync();
            if (companyProfileInfo != null)
            {
                currency = await _repositoryCurrency.FirstOrDefaultAsync(a => a.Id == companyProfileInfo.DefaultCurrenyId);
            }

            var billSetUp =
                await _repositoryBillSetup.FirstOrDefaultAsync(x => x.Id == studentBill.BillSetupId);


            var billDatta = await _repositoryStudentBill.FirstOrDefaultAsync(x => x.AcademicYearId == studentBill.AcademicYearId && x.TermId == studentBill.TermId && x.ClassId == studentBill.ClassId && x.StudentId == studentBill.StudentId && x.BillTypeId == studentBill.BillTypeId && x.BillNo == studentBill.BillNo);
            
            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(studentBill.AcademicYearId, studentBill.TermId, studentBill.ClassId, studentBill.StudentId);

            var billType = await _repositoryBillType.FirstOrDefaultAsync(studentBill.BillTypeId);

            if (billDatta == null)
            {

                var billSetupInfo = _mapper.Map<BillSetupInfo>(billSetUp);

                studentBill.BillSetupInfo = billSetupInfo;

                if (basicInfo != null)
                {
                    studentBill.ClassName = basicInfo.ClassName;
                    studentBill.StudentIdentifier = basicInfo.StudentIdentifier;
                    studentBill.StudentName = basicInfo.StudentName;
                    studentBill.AcademicYearName = basicInfo.AcademicYearName;
                    studentBill.TermName = basicInfo.TermName;
                    studentBill.Description = $"Bill Processed For {basicInfo.AcademicYearName}-{basicInfo.TermName}";
                }


                if (currency != null)
                {

                    studentBill.CurrencyName = currency.CurrencyName;
                    studentBill.CurrencyCode = currency.CurrencyCode;
                    studentBill.MinorName = currency.MinorName;
                    studentBill.BuyRate = currency.BuyRate;
                    studentBill.SellRate = currency.SellRate;

                }

                if (billType != null)
                {
                    studentBill.BillTypeName = billType.Name;
                }

                var billId  =   await _repositoryStudentBill.InsertAndGetIdAsync(studentBill);
                //create statement record
                var bill =
                    await _repositoryStudentBill.GetAllListAsync(x => x.StudentId == studentBill.StudentId);

                var currentStatement =
                    await _repositoryStudentStatement.FirstOrDefaultAsync(a => a.StudentId == studentBill.StudentId);

                var statementRecord = new List<Statement>
                {
                    new Statement
                    {
                        Id = billId,
                        ActivityDate = studentBill.BillDate.ToString("dd-MMM-yyyy"),
                        ReferenceNo = studentBill.BillNo,
                        Description = studentBill.Description,
                        Invoice = studentBill.BillAmount,
                        Payment = 0,
                        Balance = bill.Sum(x => x.BillBalance) + studentBill.BillBalance,
                        CurrencyName = currency == null ? "GHS" : currency.CurrencyName,
                        StudentIdentifier = basicInfo.StudentIdentifier,
                        StudentName = basicInfo.StudentName,
                        TransactionType = 1
                    }
                };

                if (currentStatement != null)
                {
                    //
                    
                     currentStatement.Statement.AddRange(statementRecord);
                     
                    await _repositoryStudentStatement.UpdateAsync(currentStatement);

                }
                else
                {
                    await _repositoryStudentStatement.InsertAsync(new StudentStatement
                    {
                        StudentId = studentBill.StudentId,
                        Statement = statementRecord,
                        StudentIdentifier = basicInfo.StudentIdentifier,
                        StudentName = basicInfo.StudentName,
                        TenantId = studentBill.TenantId,

                    });
                }
            }
            else
            {

                var billSetupInfo = _mapper.Map<BillSetupInfo>(billSetUp);

                billDatta.BillSetupInfo = billSetupInfo;

                if (basicInfo != null)
                {
                    billDatta.ClassName = basicInfo.ClassName;
                    billDatta.StudentIdentifier = basicInfo.StudentIdentifier;
                    billDatta.StudentName = basicInfo.StudentName;
                    billDatta.AcademicYearName = basicInfo.AcademicYearName;
                    billDatta.TermName = basicInfo.TermName;
                    billDatta.BillDate = studentBill.BillDate;
                    billDatta.BillSetupId = studentBill.BillSetupId;
                    billDatta.Description = $"Bill Processed For {basicInfo.AcademicYearName}-{basicInfo.TermName}";

                }

                if (currency != null)
                {

                    billDatta.CurrencyName = currency.CurrencyName;
                    billDatta.CurrencyCode = currency.CurrencyCode;
                    billDatta.MinorName = currency.MinorName;
                    billDatta.BuyRate = currency.BuyRate;
                    billDatta.SellRate = currency.SellRate;

                }

                if (billType != null)
                {
                    billDatta.BillTypeName = billType.Name;
                }


                await _repositoryStudentBill.UpdateAsync(billDatta);

                //update statement record

                var bills =
                   await _repositoryStudentBill.GetAllListAsync(x => x.StudentId == studentBill.StudentId);

                var currentStatement =
                    await _repositoryStudentStatement.FirstOrDefaultAsync(a => a.StudentId == studentBill.StudentId);


                if (currentStatement != null)
                {
                    //pull billNo record from statement

                    var statementList = currentStatement.Statement;

                    var invoiceOnStatement = statementList.FirstOrDefault(x => x.Id == billDatta.Id);

                    var index = statementList.FindIndex(x => x.Id == billDatta.Id);

                    statementList.Remove(invoiceOnStatement);


                    invoiceOnStatement.ActivityDate = studentBill.BillDate.ToString("dd-MMM-yyyy");
                    invoiceOnStatement.ReferenceNo = billDatta.BillNo;
                    invoiceOnStatement.Description = billDatta.Description ;
                    invoiceOnStatement.Invoice = billDatta.BillAmount;
                    invoiceOnStatement.Payment = 0;
                    invoiceOnStatement.Balance = bills.Sum(x => x.BillBalance) + billDatta.BillBalance;
                    invoiceOnStatement.CurrencyName = currency == null ? "GHS" : currency.CurrencyName;
                    invoiceOnStatement.TransactionType = 1;

                    if (statementList.Any())
                    {
                        currentStatement.Statement[index] = invoiceOnStatement;
                    }
                    else
                    {
                        currentStatement.Statement.Add(invoiceOnStatement);
                    }

                   
                    await _repositoryStudentStatement.UpdateAsync(currentStatement);

                }


            }
           


        }

        public async Task CreateBatch(List<StudentBill> entityList)
        {
            Currency currency = null;
            var companyProfileInfo = await _repositoryCompanyProfile.GetAll().FirstOrDefaultAsync();
            if (companyProfileInfo != null)
            {
                currency = await _repositoryCurrency.FirstOrDefaultAsync(a => a.Id == companyProfileInfo.DefaultCurrenyId);
            }

            var billSetUp =
                await _repositoryBillSetup.FirstOrDefaultAsync(x => x.Id == entityList[0].BillSetupId);

            foreach (var entity in entityList)
            {
                var billDatta = await _repositoryStudentBill.FirstOrDefaultAsync(x => x.AcademicYearId == entity.AcademicYearId && x.TermId == entity.TermId && x.ClassId == entity.ClassId && x.StudentId == entity.StudentId && x.BillTypeId == entity.BillTypeId && x.BillNo == entity.BillNo);
                var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);


                var billSetupInfo = _mapper.Map<BillSetupInfo>(billSetUp);

                entity.BillSetupInfo = billSetupInfo;

                entity.ClassName = basicInfo.ClassName;
                entity.StudentIdentifier = basicInfo.StudentIdentifier;
                entity.StudentName = basicInfo.StudentName;
                entity.AcademicYearName = basicInfo.AcademicYearName;


                await _repositoryStudentBill.InsertAsync(entity);
                //create statement record
                var bill =
                    await _repositoryStudentBill.GetAllListAsync(x => x.StudentId == entity.StudentId);

                var currentStatement =
                    await _repositoryStudentStatement.FirstOrDefaultAsync(a => a.StudentId == entity.StudentId);

                var statementRecord = new List<Statement>
                {
                    new Statement
                    {
                        ActivityDate = entity.BillDate.ToString("dd-MMM-yyyy"),
                        ReferenceNo = entity.BillNo,
                        Description = entity.Description,
                        Invoice = entity.BillAmount,
                        Payment = 0,
                        Balance = bill.Sum(x => x.BillBalance) + entity.BillBalance,
                        CurrencyName = currency == null ? "GHS" : currency.CurrencyName,
                        TransactionType = 1
                    }
                };

                if (currentStatement != null)
                {
                    var statementList = currentStatement.Statement;
                    statementList.AddRange(statementRecord);
                    await _repositoryStudentStatement.UpdateAsync(currentStatement);

                }
                else
                {
                    await _repositoryStudentStatement.InsertAsync(new StudentStatement
                    {
                        StudentId = entity.StudentId,
                        Statement = statementRecord,
                        TenantId = entity.TenantId,

                    });
                }

            }


        }

        public async Task CreateOpeningBalance(StudentBill studentBill)
        {
            Currency currency = null;
            var companyProfileInfo = await _repositoryCompanyProfile.GetAll().FirstOrDefaultAsync();
            if (companyProfileInfo != null)
            {
                currency = await _repositoryCurrency.FirstOrDefaultAsync(a => a.Id == companyProfileInfo.DefaultCurrenyId);
            }

            var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(studentBill.AcademicYearId, studentBill.TermId, studentBill.ClassId, studentBill.StudentId);

            if (basicInfo != null)
            {
                studentBill.ClassName = basicInfo.ClassName;
                studentBill.StudentIdentifier = basicInfo.StudentIdentifier;
                studentBill.StudentName = basicInfo.StudentName;
                studentBill.AcademicYearName = basicInfo.AcademicYearName;
                studentBill.TermName = basicInfo.TermName;
                studentBill.Description = $"Opening Balance For {basicInfo.AcademicYearName}-{basicInfo.TermName}";
            }


            if (currency != null)
            {
                studentBill.CurrencyName = currency.CurrencyName;
                studentBill.CurrencyCode = currency.CurrencyCode;
                studentBill.MinorName = currency.MinorName;
                studentBill.BuyRate = currency.BuyRate;
                studentBill.SellRate = currency.SellRate;
            }

            var billId = Guid.NewGuid();
            studentBill.Id = billId;

            await _repositoryStudentBill.InsertAsync(studentBill);

            //create statement record

            var bill =
                await _repositoryStudentBill.GetAllListAsync(x => x.StudentId == studentBill.StudentId);

            var currentStatement =
                await _repositoryStudentStatement.FirstOrDefaultAsync(a => a.StudentId == studentBill.StudentId);

            var statementRecord = new List<Statement>();
            statementRecord.Add(new Statement
            {
                Id = billId,
                ActivityDate = studentBill.BillDate.ToString("dd-MMM-yyyy"),
                ReferenceNo = studentBill.BillNo,
                Description = studentBill.Description,
                Invoice = studentBill.BillAmount,
                Payment = 0,
                Balance = bill.Sum(x => x.BillBalance) + studentBill.BillBalance,
                CurrencyName = currency == null ? "GHS" : currency.CurrencyName,
                StudentIdentifier = basicInfo.StudentIdentifier,
                StudentName = basicInfo.StudentName,
                TransactionType = 1
            });

            if (currentStatement != null)
            {

                currentStatement.Statement.AddRange(statementRecord);

                await _repositoryStudentStatement.UpdateAsync(currentStatement);
            }
            else
            {
                await _repositoryStudentStatement.InsertAsync(new StudentStatement
                {
                    StudentId = studentBill.StudentId,
                    Statement = statementRecord,
                    TenantId = studentBill.TenantId,
                });
            }


        }

        public async Task CancelBill(StudentBill bill)
        {

            await _repositoryCancelledBill.InsertAsync(new CancelledBill
            {

                ClassId = bill.ClassId,
                ClassName = bill.ClassName,
                AcademicYearId = bill.AcademicYearId,
                AcademicYearName = bill.AcademicYearName,
                TermId = bill.TermId,
                TermName = bill.TermName,
                StudentId = bill.StudentId,
                StudentIdentifier = bill.StudentIdentifier,
                StudentName = bill.StudentName,
                BillAmount = bill.BillAmount,
                BillBalance = bill.BillBalance,
                BillDate = bill.BillDate,
                BillNo = bill.BillNo,
                BillSetupId = bill.BillSetupId,
                BillSetupInfo = bill.BillSetupInfo,
                BillStatus = bill.BillStatus,
                BillTypeId = bill.BillTypeId,
                BillTypeName = bill.BillTypeName,
                Details = bill.Details,
                BuyRate = bill.BuyRate,
                SellRate = bill.SellRate,
                CurrencyCode = bill.CurrencyCode,
                CurrencyName = bill.CurrencyName,
                MinorName = bill.MinorName,
                Description = bill.Description,
            });

            await _repositoryStudentBill.DeleteAsync(bill.Id);

            //update statement
            var currentStatement =
                   await _repositoryStudentStatement.FirstOrDefaultAsync(a => a.StudentId == bill.StudentId);


            if (currentStatement != null)
            {
                //pull bill No record from statement

                var statementList = currentStatement.Statement;

                var invoiceOnStatement = currentStatement.Statement.FirstOrDefault(x => x.Id == bill.Id);

                currentStatement.Statement.Remove(invoiceOnStatement);

                await _repositoryStudentStatement.UpdateAsync(currentStatement);

            }

        }
        
        public async Task<IEnumerable<object>> ListAllBalances(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _repositoryStudentBill.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId);
        }

        public async Task<IEnumerable<object>> ListStudentBills(Guid studentId)
        {
            return await _repositoryStudentBill.GetAllListAsync(x => x.StudentId == studentId && x.BillBalance > 0);
        }

        public async Task<object> GetBill(Guid id)
        {
            return await _repositoryStudentBill.GetAsync(id);
        }


        public async Task<IEnumerable<object>> GetPaidUpStudents(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _repositoryStudentBill.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillBalance <= 0);
        }

        public async Task<IEnumerable<object>> GetStudentDebtors(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _repositoryStudentBill.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId && x.BillBalance > 0);

        }
    }
}
