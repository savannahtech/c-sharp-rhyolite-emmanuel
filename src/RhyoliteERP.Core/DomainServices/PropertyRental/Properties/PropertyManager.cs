using Abp.Domain.Repositories;
using RhyoliteERP.Models.Ledger;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.Properties
{
    public class PropertyManager: Abp.Domain.Services.DomainService, IPropertyManager
    {
        private readonly IRepository<Property, Guid> _repositoryProperty;
        private readonly IRepository<PropertyType, Guid> _repositoryPropertyType;
        private readonly IRepository<PropertyGroup, Guid> _repositoryPropertyGroup;
        private readonly IRepository<Country, Guid> _repositoryCountry;
        private readonly IRepository<CoaDetail, Guid> _repositoryCoaDetail;

        public PropertyManager(IRepository<Property, Guid> repositoryProperty, IRepository<PropertyType, Guid> repositoryPropertyType, IRepository<PropertyGroup, Guid> repositoryPropertyGroup, IRepository<Country, Guid> repositoryCountry, IRepository<CoaDetail, Guid> repositoryCoaDetail)
        {
            _repositoryProperty = repositoryProperty;
            _repositoryPropertyType = repositoryPropertyType;
            _repositoryPropertyGroup = repositoryPropertyGroup;
            _repositoryCountry = repositoryCountry;
            _repositoryCoaDetail = repositoryCoaDetail;
        }

        public async Task<object> Create(Property entity)
        {
            var propertyTypeInfo = await _repositoryPropertyType.FirstOrDefaultAsync(entity.PropertyTypeId);
            if (propertyTypeInfo != null) { entity.PropertyTypeName = propertyTypeInfo.Name; }

            var propertyGroupInfo = await _repositoryPropertyGroup.FirstOrDefaultAsync(entity.PropertyTypeId);
            if (propertyGroupInfo != null) { entity.PropertyGroupName = propertyGroupInfo.Name; }

            var countryInfo = await _repositoryCountry.FirstOrDefaultAsync(entity.CountryId);
            if (countryInfo != null) { entity.CountryName = countryInfo.Name; }


            var ledgerAccountInfo = await _repositoryCoaDetail.FirstOrDefaultAsync(entity.LedgerAccountId);
            if (ledgerAccountInfo != null) { entity.LedgerAccountName = $"{ledgerAccountInfo.AccountName} - {ledgerAccountInfo.AccountNo}"; }


            entity.IsSold = false;
            entity.IsRented = false;

            var datta = await _repositoryProperty.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryProperty.InsertAsync(entity);

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

        public async Task Update(Property entity)
        {
            await _repositoryProperty.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryProperty.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            var data = await _repositoryProperty.GetAllListAsync(x=> x.Name != null);
            return data.OrderBy(x => x.Name).ToList();
        }

        public async Task<object> ListAll(bool isRented)
        {
            return await _repositoryProperty.GetAllListAsync(x => x.IsRented == isRented & x.Name != null);
        }
        public async Task Delete(Guid id)
        {
            await _repositoryProperty.DeleteAsync(id);
        }
    }
}
