using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AssignedClasses
{
   public class AssignedClassManager : Abp.Domain.Services.DomainService, IAssignedClassManager
    {
        private readonly IRepository<AssignedClass, Guid> _repositoryAssignedClass;

        public AssignedClassManager(IRepository<AssignedClass, Guid> repositoryAssignedClass)
        {
            _repositoryAssignedClass = repositoryAssignedClass;
        }

        public async Task<object> Create(AssignedClass entity)
        {
            var datta = await _repositoryAssignedClass.FirstOrDefaultAsync(x => x.AcademicYearId == entity.AcademicYearId && x.TermId == entity.TermId && entity.StaffId == x.StaffId);

            if (datta == null)
            {
                await _repositoryAssignedClass.InsertAsync(entity);

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


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryAssignedClass.GetAllListAsync();
        }

        public async Task Delete(Guid id)
        {
            await _repositoryAssignedClass.DeleteAsync(id);
        }
    }
}
