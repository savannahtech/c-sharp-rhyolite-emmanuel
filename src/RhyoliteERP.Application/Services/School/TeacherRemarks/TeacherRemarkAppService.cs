using AutoMapper;
using RhyoliteERP.DomainServices.School.TeacherRemarks;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.TeacherRemarks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.TeacherRemarks
{
   public class TeacherRemarkAppService:RhyoliteERPAppServiceBase, ITeacherRemarkAppService
    {
        private readonly ITeacherRemarkManager _teacherRemarkManager;
        private readonly IMapper _mapper;

        public TeacherRemarkAppService(ITeacherRemarkManager teacherRemarkManager, IMapper mapper)
        {
            _teacherRemarkManager = teacherRemarkManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _teacherRemarkManager.ListAll();
        }

        public async Task Create(CreateTeacherRemarkInput input)
        {
            var obj = _mapper.Map<TeacherRemark>(input);
             await _teacherRemarkManager.Create(obj);
        }

        public async Task Update(UpdateTeacherRemarkInput input)
        {
            var obj = _mapper.Map<TeacherRemark>(input);
            await _teacherRemarkManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _teacherRemarkManager.Delete(Id);

        }

    }
}
