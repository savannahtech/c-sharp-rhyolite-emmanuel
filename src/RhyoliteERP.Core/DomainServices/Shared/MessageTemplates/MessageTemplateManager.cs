using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.MessageTemplates
{
   public class MessageTemplateManager: Abp.Domain.Services.DomainService, IMessageTemplateManager
    {
        private readonly IRepository<MessageTemplate, Guid> _repositoryMessageTemplate;

        public MessageTemplateManager(IRepository<MessageTemplate, Guid> repositoryMessageTemplate)
        {
            _repositoryMessageTemplate = repositoryMessageTemplate;
        }

        public async Task<object> Create(MessageTemplate entity)
        {
            var datta = await _repositoryMessageTemplate.FirstOrDefaultAsync(x => x.Alias == entity.Alias);
            if (datta == null)
            {
                await _repositoryMessageTemplate.InsertAsync(entity);

                return new
                {
                    code = 200,
                    message = "successful"
                };
            }
            else
            {
                return new
                {
                    code = 400,
                    message = "Duplicate records are not allowed."
                };
            }
        }

        public async Task<object> ListAll(int pageNo, int pageSize)
        {
            var totalCount =
                await _repositoryMessageTemplate.CountAsync();

            var data = await _repositoryMessageTemplate.GetAll().Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();

            var totalPages = totalCount > 0 ? Convert.ToInt32(Math.Ceiling(totalCount / (decimal)pageSize)) : 0;

            var paginatedResult = new
            {
                pageNo,
                totalCount,
                totalPages,
                data,
                lowerBound = pageSize * (pageNo - 1) + 1,
                upperBound = totalPages == pageNo ? totalCount : pageNo * pageSize,

            };

            return paginatedResult;
        }

        public async Task Delete(Guid id)
        {
            await _repositoryMessageTemplate.DeleteAsync(id);
        }

        public async Task Update(MessageTemplate entity)
        {
            await _repositoryMessageTemplate.UpdateAsync(entity);
        }
    }
}
