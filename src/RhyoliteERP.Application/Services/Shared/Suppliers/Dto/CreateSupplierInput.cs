using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Suppliers.Dto
{
    public class CreateSupplierInput
    {
        public string AccountNumber { get; set; }
        public Guid SupplierGroupId { get; set; }
        public string SupplierGroupName { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public Guid VatAccountId { get; set; }
        public string VatAccountName { get; set; }
        public Guid CreditAccountId { get; set; }
        public string CreditAccountName { get; set; }
        public Guid DebitAccountId { get; set; }
        public string DebitAccountName { get; set; }
        public string TinNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CreditLimit { get; set; }
        public Guid IrsAccountId { get; set; }
        public string IrsAccountName { get; set; }
    }
}
