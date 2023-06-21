using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeCategories
{
   public class EmployeeCategoryManager: Abp.Domain.Services.DomainService, IEmployeeCategoryManager
    {
        private readonly IRepository<EmployeeCategory, Guid> _repositoryEmployeeCategory;

        public EmployeeCategoryManager(IRepository<EmployeeCategory, Guid> repositoryEmployeeCategory)
        {
            _repositoryEmployeeCategory = repositoryEmployeeCategory;
        }

        public async Task<object> Create(EmployeeCategory entity)
        {
            var datta = await _repositoryEmployeeCategory.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryEmployeeCategory.InsertAsync(entity);

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

        public async Task Update(EmployeeCategory entity)
        {
            await _repositoryEmployeeCategory.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryEmployeeCategory.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryEmployeeCategory.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryEmployeeCategory.DeleteAsync(id);

        }
    }
}
