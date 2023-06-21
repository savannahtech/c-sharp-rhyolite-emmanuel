using AutoMapper;
using RhyoliteERP.DomainServices.School.StudentStatuses;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.StudentStatuses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentStatuses
{
   public class StudentStatusAppService: RhyoliteERPAppServiceBase, IStudentStatusAppService
    {
        private readonly IStudentStatusManager _studentStatusManager;
        private readonly IMapper _mapper;

        public StudentStatusAppService(IStudentStatusManager studentStatusManager, IMapper mapper)
        {
            _studentStatusManager = studentStatusManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _studentStatusManager.ListAll();
        }

        public async Task<object> Create(CreateStudentStatusInput input)
        {
            var obj = _mapper.Map<StudentStatus>(input);
            return await _studentStatusManager.Create(obj);
        }

        public async Task<object> Update(UpdateStudentStatusInput input)
        {
            var obj = _mapper.Map<StudentStatus>(input);
            return await _studentStatusManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _studentStatusManager.Delete(Id);

        }
    }
}
