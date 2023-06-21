using Newtonsoft.Json;
using RhyoliteERP.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Subscription.Dto
{
    public class BalanceDto : BaseApiResponse
    {
        [JsonProperty("result")]
        public BalanceResult Result { get; set; }
    }


    public class BalanceResult
    {
        public Guid Id { get; set; }
        public string BusinessIdentifier { get; set; }
        public decimal Balance { get; set; }
        public decimal LastAmountPaid { get; set; }
    }

    public class ModuleLicenseInfo : BaseApiResponse
    {
        [JsonProperty("result")]
        public ModuleLicense Result { get; set; }
    }

    public class ModuleLicense
    {
        public bool IsValid { get; set; }
        public int ModuleId { get; set; }

    }

    public class SubscriptionSummaryDto
    {
        public string ModuleName { get; set; }
        public int ModuleId { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public string BillingCycle { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int StatusCode { get; set; }
        public decimal Fee { get; set; }
    }


    public class BusinessSubscriptionInfo : BaseApiResponse
    {
        [JsonProperty("result")]
        public BusinessSubscription Result { get; set; }
    }

    public class BusinessSubscription
    {
        [JsonProperty("tenantId")]
        public long TenantId { get; set; }

        [JsonProperty("accountSource")]
        public string AccountSource { get; set; }

        [JsonProperty("isGlSubscribed")]
        public bool IsGlSubscribed { get; set; }

        [JsonProperty("glSubscriptionStartDate")]
        public DateTime GlSubscriptionStartDate { get; set; }

        [JsonProperty("glSubscriptionEndDate")]
        public DateTime GlSubscriptionEndDate { get; set; }

        [JsonProperty("glFee")]
        public decimal GlFee { get; set; }

        [JsonProperty("glLicenseType")]
        public string GlLicenseType { get; set; }

        [JsonProperty("glRunningVersion")]
        public string GlRunningVersion { get; set; }

        [JsonProperty("glPaidVersion")]
        public string GlPaidVersion { get; set; }

        [JsonProperty("glBillingCycle")]
        public string GlBillingCycle { get; set; }

        [JsonProperty("glNextRenewalDate")]
        public DateTime GlNextRenewalDate { get; set; }

        [JsonProperty("isPayrollSubscribed")]
        public bool IsPayrollSubscribed { get; set; }

        [JsonProperty("payrollSubscriptionStartDate")]
        public DateTime PayrollSubscriptionStartDate { get; set; }

        [JsonProperty("payrollSubscriptionEndDate")]
        public DateTime PayrollSubscriptionEndDate { get; set; }

        [JsonProperty("payrollFee")]
        public decimal PayrollFee { get; set; }

        [JsonProperty("payrollLicenseType")]
        public string PayrollLicenseType { get; set; }

        [JsonProperty("payrollRunningVersion")]
        public string PayrollRunningVersion { get; set; }

        [JsonProperty("payrollPaidVersion")]
        public string PayrollPaidVersion { get; set; }

        [JsonProperty("payrollBillingCycle")]
        public string PayrollBillingCycle { get; set; }

        [JsonProperty("payrollNextRenewalDate")]
        public DateTime PayrollNextRenewalDate { get; set; }

        [JsonProperty("isSchoolManagerSubscribed")]
        public bool IsSchoolManagerSubscribed { get; set; }

        [JsonProperty("schoolManagerSubscriptionStartDate")]
        public DateTime SchoolManagerSubscriptionStartDate { get; set; }

        [JsonProperty("schoolManagerSubscriptionEndDate")]
        public DateTime SchoolManagerSubscriptionEndDate { get; set; }

        [JsonProperty("schoolManagerFee")]
        public decimal SchoolManagerFee { get; set; }

        [JsonProperty("schoolManagerLicenseType")]
        public string SchoolManagerLicenseType { get; set; }

        [JsonProperty("schoolManagerRunningVersion")]
        public string SchoolManagerRunningVersion { get; set; }

        [JsonProperty("schoolManagerPaidVersion")]
        public string SchoolManagerPaidVersion { get; set; }

        [JsonProperty("schoolManagerBillingCycle")]
        public string SchoolManagerBillingCycle { get; set; }

        [JsonProperty("schoolManagerNextRenewalDate")]
        public DateTime SchoolManagerNextRenewalDate { get; set; }

        [JsonProperty("isHrSubscribed")]
        public bool IsHrSubscribed { get; set; }

        [JsonProperty("hrSubscriptionStartDate")]
        public DateTime HrSubscriptionStartDate { get; set; }

        [JsonProperty("hrSubscriptionEndDate")]
        public DateTime HrSubscriptionEndDate { get; set; }

        [JsonProperty("hrFee")]
        public decimal HrFee { get; set; }

        [JsonProperty("hrLicenseType")]
        public string HrLicenseType { get; set; }

        [JsonProperty("hrRunningVersion")]
        public string HrRunningVersion { get; set; }

        [JsonProperty("hrPaidVersion")]
        public string HrPaidVersion { get; set; }

        [JsonProperty("hrBillingCycle")]
        public string HrBillingCycle { get; set; }

        [JsonProperty("hrNextRenewalDate")]
        public DateTime HrNextRenewalDate { get; set; }

        [JsonProperty("isArSubscribed")]
        public bool IsArSubscribed { get; set; }

        [JsonProperty("arSubscriptionStartDate")]
        public DateTime ArSubscriptionStartDate { get; set; }

        [JsonProperty("arSubscriptionEndDate")]
        public DateTime ArSubscriptionEndDate { get; set; }

        [JsonProperty("arFee")]
        public decimal ArFee { get; set; }

        [JsonProperty("arLicenseType")]
        public string ArLicenseType { get; set; }

        [JsonProperty("arRunningVersion")]
        public string ArRunningVersion { get; set; }

        [JsonProperty("arPaidVersion")]
        public string ArPaidVersion { get; set; }

        [JsonProperty("arBillingCycle")]
        public string ArBillingCycle { get; set; }

        [JsonProperty("arNextRenewalDate")]
        public DateTime ArNextRenewalDate { get; set; }

        [JsonProperty("isAPSubscribed")]
        public bool IsApSubscribed { get; set; }

        [JsonProperty("apSubscriptionStartDate")]
        public DateTime ApSubscriptionStartDate { get; set; }

        [JsonProperty("apSubscriptionEndDate")]
        public DateTime ApSubscriptionEndDate { get; set; }

        [JsonProperty("apFee")]
        public decimal ApFee { get; set; }

        [JsonProperty("apLicenseType")]
        public string ApLicenseType { get; set; }

        [JsonProperty("apRunningVersion")]
        public string ApRunningVersion { get; set; }

        [JsonProperty("apPaidVersion")]
        public string ApPaidVersion { get; set; }

        [JsonProperty("apBillingCycle")]
        public string ApBillingCycle { get; set; }

        [JsonProperty("apNextRenewalDate")]
        public DateTime ApNextRenewalDate { get; set; }

        [JsonProperty("isStockManagerSubscribed")]
        public bool IsStockManagerSubscribed { get; set; }

        [JsonProperty("stockManagerSubscriptionStartDate")]
        public DateTime StockManagerSubscriptionStartDate { get; set; }

        [JsonProperty("stockManagerSubscriptionEndDate")]
        public DateTime StockManagerSubscriptionEndDate { get; set; }

        [JsonProperty("stockManagerFee")]
        public decimal StockManagerFee { get; set; }

        [JsonProperty("stockManagerLicenseType")]
        public string StockManagerLicenseType { get; set; }

        [JsonProperty("stockManagerRunningVersion")]
        public string StockManagerRunningVersion { get; set; }

        [JsonProperty("stockManagerPaidVersion")]
        public string StockManagerPaidVersion { get; set; }

        [JsonProperty("stockManagerBillingCycle")]
        public string StockManagerBillingCycle { get; set; }

        [JsonProperty("stockManagerNextRenewalDate")]
        public DateTime StockManagerNextRenewalDate { get; set; }

        [JsonProperty("isPropertyRentalModuleSubscribed")]
        public bool IsPropertyRentalModuleSubscribed { get; set; }

        [JsonProperty("propertyRentalModuleSubscriptionStartDate")]
        public DateTime PropertyRentalModuleSubscriptionStartDate { get; set; }

        [JsonProperty("propertyRentalModuleSubscriptionEndDate")]
        public DateTime PropertyRentalModuleSubscriptionEndDate { get; set; }

        [JsonProperty("propertyRentalModuleFee")]
        public decimal PropertyRentalModuleFee { get; set; }

        [JsonProperty("propertyRentalModuleLicenseType")]
        public string PropertyRentalModuleLicenseType { get; set; }

        [JsonProperty("propertyRentalModuleRunningVersion")]
        public string PropertyRentalModuleRunningVersion { get; set; }

        [JsonProperty("propertyRentalModulePaidVersion")]
        public string PropertyRentalModulePaidVersion { get; set; }

        [JsonProperty("propertyRentalModuleBillingCycle")]
        public string PropertyRentalModuleBillingCycle { get; set; }

        [JsonProperty("propertyRentalModuleNextRenewalDate")]
        public DateTime PropertyRentalModuleNextRenewalDate { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("creationTime")]
        public DateTime CreationTime { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }

    public class InvoiceInfo : BaseApiResponse
    {
        [JsonProperty("result")]
        public PaginatedInvoice Result { get; set; }
    }
    public class PaginatedInvoice 
    {
        [JsonProperty("pageNo")]
        public int PageNo { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("data")]
        public List<Invoice> Data { get; set; }

        [JsonProperty("lowerBound")]
        public int LowerBound { get; set; }

        [JsonProperty("upperBound")]
        public int UpperBound { get; set; }
    }

    public class Invoice
    {

        [JsonProperty("tenantId")]
        public int TenantId { get; set; }

        [JsonProperty("businessId")]
        public Guid BusinessId { get; set; }

        [JsonProperty("tenantName")]
        public string TenantName { get; set; }

        [JsonProperty("accountSource")]
        public string AccountSource { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("invoiceNo")]
        public string InvoiceNo { get; set; }

        [JsonProperty("serviceType")]
        public string ServiceType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
 
        [JsonProperty("creationTime")]
        public DateTime CreationTime { get; set; }
        public string Status { get; set; } //pending, paid

        [JsonProperty("id")]
        public Guid Id { get; set; }

    }

    public class SmsBillInfo
    {
        public int? TenantId { get; set; }
        public decimal Rate { get; set; }
        public string AccountSource { get; set; }
    }
}
