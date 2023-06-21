using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.School
{
    public class BillPayment : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid StudentId { get; set; }
        public string StudentIdentifier { get; set; }
        public string StudentName { get; set; }
        public decimal AmountPaid { get; set; }
        public string ModeOfPayment { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyBuyRate { get; set; }
        public decimal CurrencySellRate { get; set; }
        public string ChequeNo { get; set; }
        public Guid BillId { get; set; }
        public string BillNo { get; set; }
        public string ReceiptNo { get; set; }
        public string PaymentDescription { get; set; }
        public bool IsCreditMemo { get; set; }
        public bool IsPosted { get; set; }
        public int TenantId { get; set; }
    }
}
