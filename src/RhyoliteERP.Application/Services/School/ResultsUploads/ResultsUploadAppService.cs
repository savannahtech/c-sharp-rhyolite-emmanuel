using AutoMapper;
using RhyoliteERP.DomainServices.School.ResultsUploads;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.ResultsUploads.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.ResultsUploads
{
   public class ResultsUploadAppService: RhyoliteERPAppServiceBase, IResultsUploadAppService
    {
        private readonly IResultsUploadManager _resultsUploadManager;
        private readonly IMapper _mapper;

        public ResultsUploadAppService(IResultsUploadManager resultsUploadManager, IMapper mapper)
        {
            _resultsUploadManager = resultsUploadManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll(Guid academicYearId, Guid termId, Guid classId, Guid subjectId, Guid resultTypeId)
        {
            return await _resultsUploadManager.ListAll(academicYearId, termId, classId, subjectId, resultTypeId);
        }


        public async Task Create(CreateResultsUploadInput input)
        {
            var obj = _mapper.Map<ResultsUpload>(input);
            await _resultsUploadManager.Create(obj);
        }

        public async Task Update(UpdateResultsUploadInput input)
        {
            var obj = _mapper.Map<ResultsUpload>(input);
            await _resultsUploadManager.Update(obj);
        }

        public async Task Delete(Guid id)
        {
            await _resultsUploadManager.Delete(id);
        }
    }
}
