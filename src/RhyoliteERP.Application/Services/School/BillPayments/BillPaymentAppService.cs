using AutoMapper;
using RhyoliteERP.DomainServices.School.BillPayments;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.BillPayments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.BillPayments
{
   public class BillPaymentAppService:RhyoliteERPAppServiceBase, IBillPaymentAppService
    {
        private readonly IBillPaymentManager _billPaymentManager;
        private readonly IMapper _mapper;

        public BillPaymentAppService(IBillPaymentManager billPaymentManager, IMapper mapper)
        {
            _billPaymentManager = billPaymentManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId)
        {

            return await _billPaymentManager.ListAll(academicYearId, termId, classId);
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid studentId)
        {

            return await _billPaymentManager.ListAll(academicYearId, termId, classId, studentId);
        }

        public async Task<IEnumerable<object>> ListAllUnPosted(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _billPaymentManager.ListAllUnPosted(academicYearId, termId, classId);
        }


        public async Task<IEnumerable<object>> ListAllPosted(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _billPaymentManager.ListAllPosted(academicYearId, termId, classId);
        }

        public async Task<IEnumerable<object>> ListAllCreditMemos(Guid academicYearId, Guid termId, Guid classId)
        {
            return await _billPaymentManager.ListAllCreditMemos(academicYearId, termId, classId);
        }

        public async Task<IEnumerable<object>> ListDailyPayments(DateTime paymentDate)
        {
            return await _billPaymentManager.ListDailyPayments(paymentDate);
        }

        public async Task<object> Create(CreateBillPaymentInput input)
        {
            var obj = _mapper.Map<BillPayment>(input);
            return await _billPaymentManager.Create(obj);
        }

        public async Task<object> CreateCreditMemo(CreateBillPaymentInput input)
        {
            var obj = _mapper.Map<BillPayment>(input);
            return await _billPaymentManager.CreateCreditMemo(obj);
        }

        public async Task CancelBatch(List<CancelBillPaymentInput> paymentList)
        {
            foreach (var payment in paymentList)
            {
                var obj = _mapper.Map<BillPayment>(payment);
                await _billPaymentManager.CancelPayment(obj);
            }

        }
        public async Task Delete(Guid id)
        {
            await _billPaymentManager.Delete(id);
        }

        public async Task PostPayments(IEnumerable<Guid> ids)
        {
            await _billPaymentManager.PostPayments(ids);
        }
    }
}
