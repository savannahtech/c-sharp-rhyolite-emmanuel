using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AssignedDesignations
{
   public class AssignedDesignationManager : Abp.Domain.Services.DomainService, IAssignedDesignationManager
    {
        private readonly IRepository<AssignedDesignation, Guid> _repositoryAssignedDesignation;

        public AssignedDesignationManager(IRepository<AssignedDesignation, Guid> repositoryAssignedDesignation)
        {
            _repositoryAssignedDesignation = repositoryAssignedDesignation;
        }

        public async Task<object> Create(AssignedDesignation entity)
        {
            var datta = await _repositoryAssignedDesignation.FirstOrDefaultAsync(x => x.StaffId == entity.StaffId && x.AcademicYearId == entity.AcademicYearId && x.TermId == entity.TermId);

            if (datta == null)
            {
                await _repositoryAssignedDesignation.InsertAsync(entity);

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

        public async Task Update(AssignedDesignation entity)
        {
            await _repositoryAssignedDesignation.UpdateAsync(entity);

        }
        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryAssignedDesignation.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryAssignedDesignation.DeleteAsync(id);

        }
    }
}
