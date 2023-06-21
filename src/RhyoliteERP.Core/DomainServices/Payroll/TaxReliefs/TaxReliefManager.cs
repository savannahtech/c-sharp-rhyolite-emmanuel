using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.TaxReliefs
{
   public class TaxReliefManager : Abp.Domain.Services.DomainService, ITaxReliefManager
    {
        private readonly IRepository<TaxRelief, Guid> _repositoryTaxRelief;

        public TaxReliefManager(IRepository<TaxRelief, Guid> repositoryTaxRelief)
        {
            _repositoryTaxRelief = repositoryTaxRelief;
        }

        public async Task<object> Create(TaxRelief entity)
        {
            var datta = await _repositoryTaxRelief.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryTaxRelief.InsertAsync(entity);

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

        public async Task Update(TaxRelief entity)
        {
            await _repositoryTaxRelief.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryTaxRelief.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryTaxRelief.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryTaxRelief.DeleteAsync(id);

        }
    }
}
