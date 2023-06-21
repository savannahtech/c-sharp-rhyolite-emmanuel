using AutoMapper;
using RhyoliteERP.DomainServices.Shared.MessageTemplates;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.Shared.MessageTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.MessageTemplates
{
   public class MessageTemplateAppService:RhyoliteERPAppServiceBase, IMessageTemplateAppService
    {
        private readonly IMessageTemplateManager _messageTemplateManager;
        private readonly IMapper _mapper;

        public MessageTemplateAppService(IMessageTemplateManager messageTemplateManager, IMapper mapper)
        {
            _messageTemplateManager = messageTemplateManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll(int pageNo, int pageSize)
        {
            return await _messageTemplateManager.ListAll(pageNo, pageSize);
        }

        public async Task<object> Create(CreateMessageTemplateInput input)
        {
            var obj = _mapper.Map<MessageTemplate>(input);
            return await _messageTemplateManager.Create(obj);
        }

        public async Task Update(UpdateMessageTemplateInput input)
        {
            var obj = _mapper.Map<MessageTemplate>(input);
            await _messageTemplateManager.Update(obj);
        }

        public async Task Delete(Guid id)
        {
            await _messageTemplateManager.Delete(id);
        }

    }
}
