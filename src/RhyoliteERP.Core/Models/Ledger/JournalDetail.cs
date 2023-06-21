﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Models.Ledger
{
   public class JournalDetail
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public Guid OuId { get; set; } 
        public string OuName { get; set; }
        public string ReferenceNumber { get; set; }
        public string ChequeNo { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionSource { get; set; }
        public bool IsMultiCurrency { get; set; }
        public bool IsReconciled { get; set; }
        public bool IsOpeningBalance { get; set; }
        public bool IsConsolidated { get; set; }
        public DateTime DateConsolidated { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public Guid PettyCashReceipientId { get; set; } //FOR PETTY CASH ONLy, otherwise set to empty Guid
        public string PettyCashReceipientName { get; set; } 
        public int TenantId { get; set; }
    }
}
