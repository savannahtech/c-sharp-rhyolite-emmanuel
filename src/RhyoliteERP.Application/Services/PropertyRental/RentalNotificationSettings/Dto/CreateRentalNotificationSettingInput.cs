using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.RentalNotificationSettings.Dto
{
    public class CreateRentalNotificationSettingInput
    {
        public bool SendPaymentSmsAlert { get; set; }
        public bool SendPaymentEmailAlert { get; set; }
        public bool SendRentExpiryReminderSmsAlert { get; set; }
        public bool SendRentExpiryReminderEmailAlert { get; set; }
        public bool SendRentOnboardSmsAlert { get; set; }
        public bool SendRentOnboardEmailAlert { get; set; }
        public bool SendLeaseRenewalSmsAlert { get; set; }
        public bool SendLeaseRenewalEmailAlert { get; set; }
        //personal and general
        public bool SendTenantBirthdaySmsAlert { get; set; }
        public Guid TenantBirthdaySmsAlertTemplateId { get; set; }

        //internal alerts
        public decimal SmsLowBalanceLimit { get; set; }
        public decimal SendSmsLowBalanceLimitAlert { get; set; }
    }
}
