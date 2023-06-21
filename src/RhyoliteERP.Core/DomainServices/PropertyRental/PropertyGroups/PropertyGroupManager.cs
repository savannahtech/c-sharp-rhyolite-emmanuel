using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyGroups
{
    public class PropertyGroupManager: Abp.Domain.Services.DomainService, IPropertyGroupManager
    {

        private readonly IRepository<PropertyGroup, Guid> _repositoryPropertyGroup;

        public PropertyGroupManager(IRepository<PropertyGroup, Guid> repositoryPropertyGroup)
        {
            _repositoryPropertyGroup = repositoryPropertyGroup;
        }

        public async Task<object> Create(PropertyGroup entity)
        {
            var datta = await _repositoryPropertyGroup.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryPropertyGroup.InsertAsync(entity);

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

        public async Task Update(PropertyGroup entity)
        {
            await _repositoryPropertyGroup.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryPropertyGroup.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryPropertyGroup.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryPropertyGroup.DeleteAsync(id);
        }
    }
}
