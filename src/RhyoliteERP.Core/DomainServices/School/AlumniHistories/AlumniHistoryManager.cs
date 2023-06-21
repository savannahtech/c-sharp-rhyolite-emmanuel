using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AlumniHistories
{
   public class AlumniHistoryManager : Abp.Domain.Services.DomainService, IAlumniHistoryManager
    {
        private readonly IRepository<AlumniHistory, Guid> _repositoryAlumniHistory;
        private readonly IRepository<SchoolProfile, Guid> _repositorySchoolProfile;

        public AlumniHistoryManager(IRepository<AlumniHistory, Guid> repositoryAlumniHistory, IRepository<SchoolProfile, Guid> repositorySchoolProfile)
        {
            _repositoryAlumniHistory = repositoryAlumniHistory;
            _repositorySchoolProfile = repositorySchoolProfile;
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId)
        {
            return await _repositoryAlumniHistory.GetAllListAsync(c => c.AcademicYearCompleted == academicYearId);
        }

        public async Task CreateBatch(List<AlumniHistory> entityList)
        {
            var schoolProfile = await _repositorySchoolProfile.GetAll().FirstOrDefaultAsync();

            if (schoolProfile != null)
            {
                foreach (var entity in entityList)
                {
                    entity.AcademicYearCompleted = schoolProfile.CurrentAcademicYearId;
                    entity.CompletionDate = DateTime.UtcNow;

                    await _repositoryAlumniHistory.InsertAsync(entity);
                }
            }
            else
            {
                foreach (var entity in entityList)
                {
                    entity.AcademicYearCompleted =  Guid.Empty;
                    await _repositoryAlumniHistory.InsertAsync(entity);
                }
            }
        }

    }
}
