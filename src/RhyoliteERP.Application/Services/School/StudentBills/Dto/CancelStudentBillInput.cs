using Abp.Application.Services.Dto;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentBills.Dto
{
   public class CancelStudentBillInput:EntityDto<Guid>
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public Guid AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string MinorName { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillBalance { get; set; }
        public Guid StudentId { get; set; }
        public string StudentIdentifier { get; set; }
        public string StudentName { get; set; }
        public int BillStatus { get; set; } // 201 => Paid(credit memo) ; 301=> Unpaid ; 401=>Part Payment
        public string Description { get; set; }
        public Guid BillTypeId { get; set; }  //empty guid for credit memo
        public string BillTypeName { get; set; }
        public Guid BillSetupId { get; set; } //empty guid for credit memo
        public BillSetup BillSetupInfo { get; set; }
        public List<BillDetail> Details { get; set; }
    }
}
