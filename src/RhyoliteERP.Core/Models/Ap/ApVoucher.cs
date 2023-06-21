﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ap
{
   public class ApVoucher : Abp.Domain.Entities.Auditing.FullAuditedEntity<Guid>, Abp.Domain.Entities.IMustHaveTenant
    {
        //Invoice Details...
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string AccountNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime VoucherEntryDate { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal Balance { get; set; }
        public Guid LiabilityAccountId { get; set; }
        public string LiabilityAccountCode { get; set; }
        public string Description { get; set; }
        //payment details...

        public Guid BankAccountId { get; set; }
        public Guid BankId { get; set; }
        public string BankName { get; set; }
        public Guid BankBranchId { get; set; }
        public string BankBranchCode { get; set; }
        public Guid PayCurrencyId { get; set; }
        public string PayCurrencyName { get; set; }
        public string PayCurrencyCode { get; set; }
        public decimal PayCurrencyBuyRate { get; set; }
        public decimal PayCurrencySellRate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string ModeOfPayment { get; set; }
        public string ChequeNumber { get; set; }
        //withholding...
        public bool GETFL { get; set; }
        public decimal GETFLRate { get; set; }
        public bool NHIL { get; set; }
        public decimal NHILRate { get; set; }
        public bool VAT { get; set; }
        public decimal VATRate { get; set; }
        public decimal Total { get; set; }
        public decimal WithHeld { get; set; }
        public decimal VATWithHeld { get; set; }
        public decimal VATAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal WithHoldingRate { get; set; }
        public Guid VatWithHoldAccountId { get; set; }
        public string VatWithHoldAccountName { get; set; }
        public Guid CreditAccountId { get; set; }
        public string CreditAccountName { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "jsonb")] public List<ApVoucherDetail> Details { get; set; }
        public int TenantId { get; set; }
    }
}
