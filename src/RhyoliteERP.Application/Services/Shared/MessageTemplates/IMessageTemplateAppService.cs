using RhyoliteERP.Services.Shared.MessageTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.MessageTemplates
{
   public interface IMessageTemplateAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll(int pageNo, int pageSize);
        Task<object> Create(CreateMessageTemplateInput entity);
        Task Update(UpdateMessageTemplateInput entity);
        Task Delete(Guid Id);
    }
}
