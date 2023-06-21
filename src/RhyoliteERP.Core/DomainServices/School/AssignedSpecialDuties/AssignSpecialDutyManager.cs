using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AssignedSpecialDuties
{
   public class AssignSpecialDutyManager: Abp.Domain.Services.DomainService, IAssignSpecialDutyManager
    {
        private readonly IRepository<AssignSpecialDuty, Guid> _repositoryAssignSpecialDuty;

        public AssignSpecialDutyManager(IRepository<AssignSpecialDuty, Guid> repositoryAssignSpecialDuty)
        {
            _repositoryAssignSpecialDuty = repositoryAssignSpecialDuty;
        }


        public async Task<object> Create(AssignSpecialDuty input)
        {
            var datta = await _repositoryAssignSpecialDuty.FirstOrDefaultAsync(x => x.AcademicYearId == input.AcademicYearId && x.TermId == input.TermId && x.StaffId == input.StaffId && x.DutyId == input.DutyId);
            if (datta == null)
            {
                await _repositoryAssignSpecialDuty.InsertAsync(datta);

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


        public async Task Delete(Guid id)
        {
            await _repositoryAssignSpecialDuty.DeleteAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _repositoryAssignSpecialDuty.GetAllListAsync();
        }
    }
}
