using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.Vendors
{
    public class VendorManager: Abp.Domain.Services.DomainService, IVendorManager
    {
        private readonly IRepository<Vendor, Guid> _repositoryVendor;

        public VendorManager(IRepository<Vendor, Guid> repositoryVendor)
        {
            _repositoryVendor = repositoryVendor;
        }

        public async Task<object> Create(Vendor entity)
        {
            var datta = await _repositoryVendor.FirstOrDefaultAsync(x => x.VendorIdentifier == entity.VendorIdentifier);

            if (datta == null)
            {
                await _repositoryVendor.InsertAsync(entity);

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

        public async Task Update(Vendor entity)
        {
            await _repositoryVendor.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryVendor.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryVendor.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryVendor.DeleteAsync(id);
        }

    }
}
