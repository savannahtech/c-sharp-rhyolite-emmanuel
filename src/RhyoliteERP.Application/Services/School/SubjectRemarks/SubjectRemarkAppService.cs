using AutoMapper;
using RhyoliteERP.DomainServices.School.SubjectRemarks;
using RhyoliteERP.DomainServices.School.Subjects;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.SubjectRemarks.Dto;
using RhyoliteERP.Services.School.Subjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SubjectRemarks
{
    public class SubjectRemarkAppService: RhyoliteERPAppServiceBase, ISubjectRemarkAppService
    {

        private readonly ISubjectRemarkManager _subjectRemarkManager;
        private readonly IMapper _mapper;

        public SubjectRemarkAppService(ISubjectRemarkManager subjectRemarkManager, IMapper mapper)
        {
            _subjectRemarkManager = subjectRemarkManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _subjectRemarkManager.ListAll();
        }

        public async Task Create(CreateSubjectRemarkInput input)
        {
            var obj = _mapper.Map<SubjectRemark>(input);
            await _subjectRemarkManager.Create(obj);
        }

        public async Task Update(UpdateSubjectRemarkInput input)
        {
            var obj = _mapper.Map<SubjectRemark>(input);
            await _subjectRemarkManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _subjectRemarkManager.Delete(Id);

        }
    }
}
