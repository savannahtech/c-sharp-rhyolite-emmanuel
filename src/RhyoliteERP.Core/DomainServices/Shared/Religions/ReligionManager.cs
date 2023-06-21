using Abp.Domain.Repositories;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Religions
{
   public class ReligionManager: Abp.Domain.Services.DomainService, IReligionManager
    {
        private readonly IRepository<Religion, Guid> _repositoryReligion;

        public ReligionManager(IRepository<Religion, Guid> repositoryReligion)
        {
            _repositoryReligion = repositoryReligion;
        }

        public async Task<object> Create(Religion entity)
        {
            var datta = await _repositoryReligion.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryReligion.InsertAsync(entity);

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

        public async Task Update(Religion entity)
        {
            await _repositoryReligion.UpdateAsync(entity);
        }
         
        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryReligion.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryReligion.DeleteAsync(id);

        }

    }
}
