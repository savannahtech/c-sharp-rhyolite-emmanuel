using Abp.Domain.Repositories;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Relationships
{
   public class RelationshipManager: Abp.Domain.Services.DomainService, IRelationshipManager
    {
        private readonly IRepository<Relationship, Guid> _repositoryRelationship;

        public RelationshipManager(IRepository<Relationship, Guid> repositoryRelationship)
        {
            _repositoryRelationship = repositoryRelationship;
        }

        public async Task<object> Create(Relationship entity)
        {
            var datta = await _repositoryRelationship.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryRelationship.InsertAsync(entity);

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

        public async Task Update(Relationship entity)
        {
            await _repositoryRelationship.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryRelationship.GetAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryRelationship.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryRelationship.DeleteAsync(id);

        }
    }
}
