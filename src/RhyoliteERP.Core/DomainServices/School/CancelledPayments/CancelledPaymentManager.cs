using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.BasicInfo;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.CancelledPayments
{
   public class CancelledPaymentManager: Abp.Domain.Services.DomainService, ICancelledPaymentManager
    {
        private readonly IRepository<CancelledPayment, Guid> _repositoryCancelledPayment;
        private readonly IRepository<Currency, Guid> _repositoryCurrency;

        private readonly IBasicAcademicInfoManager _basicAcademicInfoManager;

        public CancelledPaymentManager(IRepository<CancelledPayment, Guid> repositoryCancelledPayment, IBasicAcademicInfoManager basicAcademicInfoManager, IRepository<Currency, Guid> repositoryCurrency)
        {
            _repositoryCancelledPayment = repositoryCancelledPayment;
            _basicAcademicInfoManager = basicAcademicInfoManager;
            _repositoryCurrency = repositoryCurrency;
        }

        public async Task CreateBatch(List<CancelledPayment> payments)
        {
            foreach (CancelledPayment entity in payments)
            {
                var basicInfo = await _basicAcademicInfoManager.GetBasicAcademicInfo(entity.AcademicYearId, entity.TermId, entity.ClassId, entity.StudentId);
                var currency = await _repositoryCurrency.FirstOrDefaultAsync(x=>x.Id == entity.CurrencyId);

                var cancelledPayment = new CancelledPayment
                {
                    AcademicYearId = entity.AcademicYearId,
                    AcademicYearName = basicInfo.AcademicYearName,
                    TermName = basicInfo.TermName,
                    ClassName = basicInfo.ClassName,
                    TermId = entity.TermId,
                    ClassId = entity.ClassId,
                    StudentId = entity.StudentId,
                    StudentIdentifier = basicInfo.StudentIdentifier,
                    StudentName = basicInfo.StudentName,
                    AmountPaid = entity.AmountPaid,
                    ModeOfPayment = entity.ModeOfPayment,
                    PaymentDate = entity.PaymentDate,
                    CurrencyId = entity.CurrencyId,
                    CurrencyBuyRate = currency.BuyRate,
                    CurrencySellRate = currency.SellRate,
                    ChequeNo = entity.ChequeNo,
                    BillId = entity.BillId,
                    BillNo = entity.BillNo,
                    ReceiptNo = entity.ReceiptNo,
                    PaymentDescription = entity.PaymentDescription,
                    IsCreditMemo = entity.IsCreditMemo,
                };

                await _repositoryCancelledPayment.InsertAsync(cancelledPayment);
            }

        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _repositoryCancelledPayment.GetAllListAsync(x => x.AcademicYearId == academicYearId && x.TermId == termId && x.ClassId == classId);
        }
    }
}
