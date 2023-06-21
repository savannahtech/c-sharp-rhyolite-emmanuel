using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyTasks
{
    public class PropertyTaskManager: Abp.Domain.Services.DomainService, IPropertyTaskManager
    {
        private readonly IRepository<PropertyTask, Guid> _repositoryPropertyTask;

        public PropertyTaskManager(IRepository<PropertyTask, Guid> repositoryPropertyTask)
        {
            _repositoryPropertyTask = repositoryPropertyTask;
        }

        public async Task Create(PropertyTask entity)
        {
            await _repositoryPropertyTask.InsertAsync(entity);
        }

        public async Task Update(PropertyTask entity)
        {
            await _repositoryPropertyTask.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryPropertyTask.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryPropertyTask.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryPropertyTask.DeleteAsync(id);
        }

    }
}
