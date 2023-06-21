using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.TaxTables
{
   public class TaxTableManager:Abp.Domain.Services.DomainService, ITaxTableManager
    {
        private readonly IRepository<TaxTable, Guid> _repositoryTaxTable;

        public TaxTableManager(IRepository<TaxTable, Guid> repositoryTaxTable)
        {
            _repositoryTaxTable = repositoryTaxTable;
        }

        public async Task Create(TaxTable entity)
        {
            await _repositoryTaxTable.InsertAsync(entity);
        }

        public async Task Update(TaxTable entity)
        {
            await _repositoryTaxTable.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryTaxTable.FirstOrDefaultAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryTaxTable.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryTaxTable.DeleteAsync(id);

        }
    }
}
