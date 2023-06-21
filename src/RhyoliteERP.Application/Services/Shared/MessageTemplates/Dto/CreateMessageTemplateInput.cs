using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.MessageTemplates.Dto
{
    public class CreateMessageTemplateInput
    {
        public string MessageContent { get; set; }
        public string MessageSubject { get; set; }
        public string Alias { get; set; }
        public int TenantId { get; set; }
    }
}
