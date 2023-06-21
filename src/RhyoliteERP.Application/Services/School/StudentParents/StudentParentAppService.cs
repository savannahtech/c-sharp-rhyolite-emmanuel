using AutoMapper;
using RhyoliteERP.DomainServices.School.Parents;
using RhyoliteERP.DomainServices.School.StudentParents;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.StudentParents.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentParents
{
   public class StudentParentAppService: RhyoliteERPAppServiceBase, IStudentParentAppService
    {
        private readonly IStudentParentManager _studentParentManager;
        private readonly IMapper _mapper;

        public StudentParentAppService(IStudentParentManager studentParentManager, IMapper mapper)
        {
            _studentParentManager = studentParentManager;
            _mapper = mapper;
        }

        public async Task<object> GetStudentParent(Guid id)
        {
            return await _studentParentManager.GetStudentParent(id);
        }

        public async Task Create(CreateStudentParentInput input)
        {
            var obj = _mapper.Map<StudentParent>(input);
            await _studentParentManager.Create(obj);
        }


        public async Task Update(UpdateStudentParentInput input)
        {
            var obj = _mapper.Map<StudentParent>(input);
            await _studentParentManager.Update(obj);
        }

        public async Task Delete(Guid id)
        {
            await _studentParentManager.Delete(id);
        }

        public async Task<IEnumerable<Guid>> ListChildren(Guid parentId)
        {
            return await _studentParentManager.ListChildren(parentId);
        }
    }
}
