using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.PastQuestions
{
   public class PastQuestionManager:Abp.Domain.Services.DomainService, IPastQuestionManager
    {
        private readonly IRepository<PastQuestion, Guid> _repositoryPastQuestion;

        public PastQuestionManager(IRepository<PastQuestion, Guid> repositoryPastQuestion)
        {
            _repositoryPastQuestion = repositoryPastQuestion;
        }


        public async Task<object> Create(PastQuestion entity)
        {
            var datta = await _repositoryPastQuestion.FirstOrDefaultAsync(x => x.Caption == entity.Caption);

            if (datta == null)
            {
                await _repositoryPastQuestion.InsertAsync(entity);

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

        public async Task Update(PastQuestion entity)
        {
            await _repositoryPastQuestion.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryPastQuestion.GetAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryPastQuestion.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryPastQuestion.DeleteAsync(id);

        }
    }
}
