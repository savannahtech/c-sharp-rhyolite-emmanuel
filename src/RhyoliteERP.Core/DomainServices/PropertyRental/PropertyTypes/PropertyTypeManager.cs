using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyTypes
{
    public class PropertyTypeManager: Abp.Domain.Services.DomainService, IPropertyTypeManager
    {
        private readonly IRepository<PropertyType, Guid> _repositoryPropertyType;

        public PropertyTypeManager(IRepository<PropertyType, Guid> repositoryPropertyType)
        {
            _repositoryPropertyType = repositoryPropertyType;
        }

        public async Task<object> Create(PropertyType entity)
        {

            var datta = await _repositoryPropertyType.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryPropertyType.InsertAsync(entity);

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

        public async Task Update(PropertyType entity)
        {
            await _repositoryPropertyType.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryPropertyType.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryPropertyType.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryPropertyType.DeleteAsync(id);
        }
    }
}
