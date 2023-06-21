using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Shared.SupplierGroups;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.CustomerGroups
{
    public class CustomerGroupManager : Abp.Domain.Services.DomainService, ICustomerGroupManager
    {
        private readonly IRepository<CustomerGroup, Guid> _repositoryCustomerGroup;

        public CustomerGroupManager(IRepository<CustomerGroup, Guid> repositoryCustomerGroup)
        {
            _repositoryCustomerGroup = repositoryCustomerGroup;
        }

        public async Task<object> Create(CustomerGroup entity)
        {
            var datta = await _repositoryCustomerGroup.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryCustomerGroup.InsertAsync(entity);

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

        public async Task Update(CustomerGroup entity)
        {
            await _repositoryCustomerGroup.UpdateAsync(entity);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryCustomerGroup.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryCustomerGroup.DeleteAsync(id);

        }
    }
}
