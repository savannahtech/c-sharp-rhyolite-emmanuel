using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Messaging.Dto
{
   public class StartCampaign
    {
        public string Subject { get; set; }
        public string MessagingChannel { get; set; }
        public int? TenantId { get; set; }
        public string Message { get; set; }
        public int ModuleSource { get; set; }
        public int RecipientCount { get; set; }
        public Guid RecipientId { get; set; }
        public IFormFile File { get; set; }
        public string RecipientType { get; set; }
        public List<string> RecipientList { get; set; }
    }

    public class MessengerModel
    {
        public int? TenantId { get; set; }
        public string AccountSource { get; set; }
        public string MessagingChannel { get; set; } //sms //telegram //email //whatsapp
        public object SmsInfo { get; set; }
        public object TelegramInfo { get; set; }
        public int ModuleSource { get; set; }

    }

    public class ReceiptDto
    {
        public int? TenantId { get; set;}
        public string MessagingChannel { get; set; }
        public Guid StatementId { get; set; }
        public Guid Id { get; set; }

    }
}
