using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Banking
{
    public class SavingsAccount : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        public string AccountNo { get; set; }
        public string ClientId { get; set; }
        public string ProductId { get; set; }
        public int Status { get; set; }
        public string AccountType { get; set; }
        public string DepositType { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime RejectedOn { get; set; }
        public string RejectedBy { get; set; }
        public DateTime WithdrawnOn { get; set; }
        public string WithdrawnBy { get; set; }
        public DateTime ActivatedOn { get; set; }
        public string ActivatedBy { get; set; }
        public DateTime ClosedOn { get; set; }
        public string ClosedBy { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal NominalAnnualInterestRate { get; set; }
        public int InterestCompoundingPeriod { get; set; }
        public int InterestPostingPeriod { get; set; }
        public int InterestCaculationType { get; set; }
        public int InterestCaculationDaysInYearType { get; set; }
        public decimal MinimumRequiredOpeningBalance { get; set; }
        public int LockInPeriodFrequecncy { get; set; }
        public string LockInPeriodFrequency { get; set; }
        public int LockInPeriodFrequencyType { get; set; }
        public bool WithdrawalFeeForTransfer { get; set; }
        public bool AllowOverDraft { get; set; }
        public decimal OverDraftLimit { get; set; }
        public decimal NominalAnnualInterestRateOverdraft { get; set; }
        public DateTime LockedInUntilDateDerived { get; set; }
        public decimal TotalDepositsDerived { get; set; }
        public decimal TotalWithdrawlsDerived { get; set; }
        public decimal TotalWithdrawlFeesDerived { get; set; }
        public decimal TotalFeesChargeDerived { get; set; }
        public decimal TotalPenaltyChargeDerived { get; set; }
        public decimal TotalAnnualFeesDerived { get; set; }
        public decimal TotalInterestEarnedDerived { get; set; }
        public decimal TotalInterestPostedDerived { get; set; }
        public decimal TotalOverDraftInterestDerived { get; set; }
        public decimal TotalWithholdingTaxDerived { get; set; }
        public decimal AccountBalanceDerived { get; set; }
        public decimal MinRequiredAccountBalance { get; set; }
        public bool EnforceMinRequiredAccountBalance { get; set; }
        public decimal MinBalanceForInterestCalculation { get; set; }
        public DateTime StartInterestCalculationDate { get; set; }
        public decimal OnHoldFundsDerived { get; set; }
        public bool WithHoldTax { get; set; }
        public Guid TaxGroupId { get; set; }
        public DateTime LastInterestCalculationDate { get; set; }
        public decimal TotalSavingsAmountOnHold { get; set; }
        public int TenantId { get; set; }
    }

    
}
