using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeRanks
{
   public class EmployeeRankManager: Abp.Domain.Services.DomainService, IEmployeeRankManager
    {
        private readonly IRepository<EmployeeRank, Guid> _repositoryAllowanceType;

        public EmployeeRankManager(IRepository<EmployeeRank, Guid> repositoryAllowanceType)
        {
            _repositoryAllowanceType = repositoryAllowanceType;
        }

        public async Task<object> Create(EmployeeRank entity)
        {
            var datta = await _repositoryAllowanceType.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryAllowanceType.InsertAsync(entity);

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

        public async Task Update(EmployeeRank entity)
        {
            await _repositoryAllowanceType.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryAllowanceType.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryAllowanceType.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryAllowanceType.DeleteAsync(id);

        }
    }
}
