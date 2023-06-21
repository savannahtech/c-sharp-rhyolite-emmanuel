using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyUnits
{
    public class PropertyUnitManager: Abp.Domain.Services.DomainService, IPropertyUnitManager
    {
        private readonly IRepository<PropertyUnit, Guid> _repositoryPropertyUnit;
        private readonly IRepository<Property, Guid> _repositoryProperty;

        public PropertyUnitManager(IRepository<PropertyUnit, Guid> repositoryPropertyUnit, IRepository<Property, Guid> repositoryProperty)
        {
            _repositoryPropertyUnit = repositoryPropertyUnit;
            _repositoryProperty = repositoryProperty;
        }

        public async Task<object> Create(PropertyUnit entity)
        {
            var datta = await _repositoryPropertyUnit.FirstOrDefaultAsync(x => x.UnitNo == entity.UnitNo);

            if (datta == null)
            {

                var propertyInfo = await _repositoryProperty.FirstOrDefaultAsync(entity.PropertyId);

                if(propertyInfo != null) { entity.PropertyName = propertyInfo.Name; }

                entity.IsSold = false;
                entity.IsRented = false;
                await _repositoryPropertyUnit.InsertAsync(entity);

                return new
                {
                    code = 200,
                    data = entity.UnitNo,
                    message = "successful"
                };
            }
            else
            {
                return new
                {
                    code = 400,
                    data = entity.UnitNo,
                    message = "Duplicate records are not allowed."
                };
            }

        }

        public async Task Update(PropertyUnit entity)
        {
            await _repositoryPropertyUnit.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryPropertyUnit.FirstOrDefaultAsync(id);
        }


        public async Task<object> ListAll()
        {
            var data = await _repositoryPropertyUnit.GetAllListAsync(x => x.UnitNo != null);
            return data.OrderBy(x => x.UnitNo).ToList();

        }

        public async Task<object> ListAll(bool isRented)
        {
            return await _repositoryPropertyUnit.GetAllListAsync(x=> x.IsRented == isRented && x.UnitNo != null);
        }


        public async Task<object> ListAll(Guid propertyId, bool isRented = false)
        {
            return await _repositoryPropertyUnit.GetAllListAsync(x=> x.PropertyId == propertyId && x.IsRented == isRented && x.UnitNo != null);
        }
        public async Task Delete(Guid id)
        {
            await _repositoryPropertyUnit.DeleteAsync(id);
        }
    }
}
