using Abp.Domain.Repositories;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Shared.Customers
{
    public class CustomerManager : Abp.Domain.Services.DomainService, ICustomerManager
    {

        private readonly IRepository<Customer, Guid> _repositoryCustomer;

        public CustomerManager(IRepository<Customer, Guid> repositoryCustomer)
        {
            _repositoryCustomer = repositoryCustomer;
        }

        public async Task<object> Create(Customer entity)
        {
            var datta = await _repositoryCustomer.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryCustomer.InsertAsync(entity);

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

        public async Task Update(Customer entity)
        {
            await _repositoryCustomer.UpdateAsync(entity);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryCustomer.GetAllListAsync();
        }

        public async Task<object> ListAllByGroup(Guid groupId)
        {
            return await _repositoryCustomer.GetAllListAsync(x=>x.CustomerGroupId == groupId);
        }

        
        public async Task Delete(Guid id)
        {
            await _repositoryCustomer.DeleteAsync(id);

        }

    }
}
