using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.VendorCategories
{
    public class VendorCategoryManager: Abp.Domain.Services.DomainService, IVendorCategoryManager
    {
        private readonly IRepository<VendorCategory, Guid> _repositoryVendorCategory;

        public VendorCategoryManager(IRepository<VendorCategory, Guid> repositoryVendorCategory)
        {
            _repositoryVendorCategory = repositoryVendorCategory;
        }

        public async Task<object> Create(VendorCategory entity)
        {
            var datta = await _repositoryVendorCategory.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryVendorCategory.InsertAsync(entity);

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

        public async Task Update(VendorCategory entity)
        {
            await _repositoryVendorCategory.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryVendorCategory.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryVendorCategory.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryVendorCategory.DeleteAsync(id);
        }
    }
}
