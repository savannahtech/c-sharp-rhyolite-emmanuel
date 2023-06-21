using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.MessageTemplates
{
   public interface IMessageTemplateManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll(int pageNo, int pageSize);
        Task<object> Create(MessageTemplate entity);
        Task Update(MessageTemplate entity);
        Task Delete(Guid Id);
    }
}
