using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.Gratuities
{
   public class GratuityManager: Abp.Domain.Services.DomainService, IGratuityManager
    {
        private readonly IRepository<Gratuity, Guid> _repositoryGratuity;

        public GratuityManager(IRepository<Gratuity, Guid> repositoryGratuity)
        {
            _repositoryGratuity = repositoryGratuity;
        }

        public async Task Create(Gratuity entity)
        {
            await _repositoryGratuity.InsertAsync(entity);

        }

        public async Task Update(Gratuity entity)
        {
            await _repositoryGratuity.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryGratuity.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryGratuity.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryGratuity.DeleteAsync(id);

        }
    }
}
