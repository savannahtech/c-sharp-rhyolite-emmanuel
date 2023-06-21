using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.BillTypes
{
   public class BillTypeManager: Abp.Domain.Services.DomainService, IBillTypeManager
    {
        private readonly IRepository<BillType, Guid> _repositoryBillType;
        
        public BillTypeManager(IRepository<BillType, Guid> repositoryBillType)
        {
            _repositoryBillType = repositoryBillType;
        }

        public async Task<object> Create(BillType entity)
        {
            var datta = await _repositoryBillType.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryBillType.InsertAsync(entity);

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

        public async Task Update(BillType entity)
        {
            await _repositoryBillType.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryBillType.GetAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryBillType.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryBillType.DeleteAsync(id);

        }
    }
}
