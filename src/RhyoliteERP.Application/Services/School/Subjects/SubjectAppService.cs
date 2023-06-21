using AutoMapper;
using RhyoliteERP.DomainServices.School.Subjects;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.Subjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Subjects
{
   public class SubjectAppService: RhyoliteERPAppServiceBase, ISubjectAppService
    {
        private readonly ISubjectManager _subjectManager;
        private readonly IMapper _mapper;

        public SubjectAppService(ISubjectManager subjectManager, IMapper mapper)
        {
            _subjectManager = subjectManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _subjectManager.ListAll();
        }

        public async Task<object> Create(CreateSubjectInput input)
        {
            var obj = _mapper.Map<Subject>(input);
            return await _subjectManager.Create(obj);
        }

        public async Task Update(UpdateSubjectInput input)
        {
            var obj = _mapper.Map<Subject>(input);
            await _subjectManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _subjectManager.Delete(Id);

        }
    }
}
