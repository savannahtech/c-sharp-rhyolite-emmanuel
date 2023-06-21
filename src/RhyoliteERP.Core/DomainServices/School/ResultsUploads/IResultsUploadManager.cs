using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.ResultsUploads
{
   public interface IResultsUploadManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId);
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId,int tenantId);
        Task Create(ResultsUpload entity);
        Task Update(ResultsUpload entity);
        Task Delete(Guid id);
    }
}
