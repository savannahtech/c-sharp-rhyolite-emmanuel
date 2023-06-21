using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.LeasePayments
{
    public class LeasePaymentManager: Abp.Domain.Services.DomainService, ILeasePaymentManager
    {
        private readonly IRepository<LeasePayment, Guid> _repositoryLeasePayment;

        public LeasePaymentManager(IRepository<LeasePayment, Guid> repositoryLeasePayment)
        {
            _repositoryLeasePayment = repositoryLeasePayment;
        }

        public async Task Create(LeasePayment entity)
        {
            await _repositoryLeasePayment.InsertAsync(entity);

            //send notification
        }

        public async Task Update(LeasePayment entity)
        {
            await _repositoryLeasePayment.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryLeasePayment.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryLeasePayment.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryLeasePayment.DeleteAsync(id);
        }
    }
}
