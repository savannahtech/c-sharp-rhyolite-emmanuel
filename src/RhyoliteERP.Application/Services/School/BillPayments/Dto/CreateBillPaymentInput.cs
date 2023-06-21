using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.BillPayments.Dto
{
   public class CreateBillPaymentInput
    {
        public Guid AcademicYearId { get; set; }
        public Guid TermId { get; set; }
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
        public decimal AmountPaid { get; set; }
        public string ModeOfPayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid CurrencyId { get; set; }
        public string ChequeNo { get; set; }
        public Guid BillId { get; set; }
        public string BillNo { get; set; }
        public string ReceiptNo { get; set; }
        public string PaymentDescription { get; set; }
        public bool IsCreditMemo { get; set; }
        public bool IsPosted { get; set; }
    }
}
