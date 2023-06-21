using AutoMapper;
using RhyoliteERP.DomainServices.School.ResultTypes;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.ResultTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.ResultTypes
{
   public class ResultTypeAppService: RhyoliteERPAppServiceBase , IResultTypeAppService
    {
        private readonly IResultTypeManager _resultTypeManager;
        private readonly IMapper _mapper;

        public ResultTypeAppService(IResultTypeManager resultTypeManager, IMapper mapper)
        {
            _resultTypeManager = resultTypeManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll(Guid levelId)
        {
            return await _resultTypeManager.ListAll(levelId);
        }

        public async Task<IEnumerable<object>> ListByClass(Guid classId)
        {
            return await _resultTypeManager.ListByClass(classId);
        }
        public async Task<object> Create(CreateResultTypeInput input)
        {
            var obj = _mapper.Map<ResultType>(input);
            return await _resultTypeManager.Create(obj);
        }

        public async Task Update(UpdateResultTypeInput input)
        {
            var obj = _mapper.Map<ResultType>(input);
            await _resultTypeManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _resultTypeManager.Delete(Id);

        }
    }
}
