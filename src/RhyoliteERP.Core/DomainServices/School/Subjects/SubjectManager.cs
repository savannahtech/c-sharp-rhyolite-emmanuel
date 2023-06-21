using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.Subjects
{
   public class SubjectManager: Abp.Domain.Services.DomainService, ISubjectManager
    {
        private readonly IRepository<Subject, Guid> _repositorySubject;

        public SubjectManager(IRepository<Subject, Guid> repositorySubject)
        {
            _repositorySubject = repositorySubject;
        }

        public async Task<object> Create(Subject entity)
        {
            var datta = await _repositorySubject.FirstOrDefaultAsync(x => x.Name == entity.Name);
            if (datta == null)
            {
                await _repositorySubject.InsertAsync(entity);

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

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositorySubject.GetAllListAsync();
        }

        public async Task Delete(Guid id)
        {
            await _repositorySubject.DeleteAsync(id);
        }

        public async Task Update(Subject entity)
        {
            await _repositorySubject.UpdateAsync(entity);
        }
    }
}
