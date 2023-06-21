using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.WorkOrders
{
    public class WorkOrderManager: Abp.Domain.Services.DomainService, IWorkOrderManager
    {
        private readonly IRepository<WorkOrder, Guid> _repositoryWorkOrder;

        public WorkOrderManager(IRepository<WorkOrder, Guid> repositoryWorkOrder)
        {
            _repositoryWorkOrder = repositoryWorkOrder;
        }

        public async Task Create(WorkOrder entity)
        {
            await _repositoryWorkOrder.InsertAsync(entity);
        }

        public async Task Update(WorkOrder entity)
        {
            await _repositoryWorkOrder.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryWorkOrder.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryWorkOrder.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryWorkOrder.DeleteAsync(id);
        }
    }
}
