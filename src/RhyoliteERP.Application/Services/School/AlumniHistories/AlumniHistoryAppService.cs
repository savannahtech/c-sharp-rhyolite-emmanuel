using RhyoliteERP.DomainServices.School.AlumniHistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.AlumniHistories
{
   public class AlumniHistoryAppService: RhyoliteERPAppServiceBase, IAlumniHistoryAppService
    {
        private readonly IAlumniHistoryManager _alumniHistoryManager;

        public AlumniHistoryAppService(IAlumniHistoryManager alumniHistoryManager)
        {
            _alumniHistoryManager = alumniHistoryManager;
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId)
        {
            return await _alumniHistoryManager.ListAll(academicYearId);
        }
    }
}
