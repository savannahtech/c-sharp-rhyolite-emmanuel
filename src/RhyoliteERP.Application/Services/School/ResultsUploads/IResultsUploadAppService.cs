using RhyoliteERP.Services.School.ResultsUploads.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.ResultsUploads
{
   public interface IResultsUploadAppService: Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId);
        Task Create(CreateResultsUploadInput input);
        Task Update(UpdateResultsUploadInput input);
        Task Delete(Guid id);
    }
}
