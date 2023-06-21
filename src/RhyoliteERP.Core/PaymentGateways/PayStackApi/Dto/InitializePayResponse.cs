using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.PaymentGateways.PayStackApi.Dto
{
    public class TopupRequest
    {
        public decimal Amount { get; set; }
        public string ServiceType { get; set; }
    }
    public class BaseResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
   public class InitializePayResponse : BaseResponse
    {
        public PayData Data { get; set; }
   }

    public class PayData
    {
        [JsonProperty("authorization_url")]
        public string AuthorizationUrl { get; set; }

        [JsonProperty("access_code")]
        public string AccessCode { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

    }

    public class VerifyPayResponse : BaseResponse
    {
        public VerifyData Data { get; set; }

    }

    public class VerifyData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("gateway_response")]
        public string GatewayResponse { get; set; }

        [JsonProperty("paid_at")]
        public DateTimeOffset DataPaidAt { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset DataCreatedAt { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("log")]
        public Log Log { get; set; }

        [JsonProperty("fees")]
        public long Fees { get; set; }

        [JsonProperty("fees_split")]
        public object FeesSplit { get; set; }

        [JsonProperty("authorization")]
        public Authorization Authorization { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("plan")]
        public object Plan { get; set; }

        [JsonProperty("split")]
        public object Split { get; set; }

        [JsonProperty("order_id")]
        public object OrderId { get; set; }

        [JsonProperty("paidAt")]
        public DateTimeOffset PaidAt { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("requested_amount")]
        public long RequestedAmount { get; set; }

        [JsonProperty("pos_transaction_data")]
        public object PosTransactionData { get; set; }

        [JsonProperty("source")]
        public object Source { get; set; }

        [JsonProperty("fees_breakdown")]
        public object FeesBreakdown { get; set; }

        [JsonProperty("transaction_date")]
        public DateTimeOffset TransactionDate { get; set; }

        [JsonProperty("plan_object")]
        public object PlanObject { get; set; }

        [JsonProperty("subaccount")]
        public object Subaccount { get; set; }
    }

    public class Authorization
    {
        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }

        [JsonProperty("bin")]
        public string Bin { get; set; }

        [JsonProperty("last4")]
        public string Last4 { get; set; }

        [JsonProperty("exp_month")]
        public string ExpMonth { get; set; }

        [JsonProperty("exp_year")]
        public string ExpYear { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("reusable")]
        public bool Reusable { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

    }

    public class Customer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("first_name")]
        public object FirstName { get; set; }

        [JsonProperty("last_name")]
        public object LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("customer_code")]
        public string CustomerCode { get; set; }

        [JsonProperty("phone")]
        public object Phone { get; set; }

        [JsonProperty("metadata")]
        public object Metadata { get; set; }

        [JsonProperty("risk_action")]
        public string RiskAction { get; set; }

        [JsonProperty("international_format_phone")]
        public object InternationalFormatPhone { get; set; }
    }

    public class Log
    {
        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        [JsonProperty("time_spent")]
        public long TimeSpent { get; set; }

        [JsonProperty("attempts")]
        public long Attempts { get; set; }

        [JsonProperty("errors")]
        public long Errors { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("mobile")]
        public bool Mobile { get; set; }

        [JsonProperty("input")]
        public object[] Input { get; set; }

        [JsonProperty("history")]
        public History[] History { get; set; }
    }

    public class History
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }

}
