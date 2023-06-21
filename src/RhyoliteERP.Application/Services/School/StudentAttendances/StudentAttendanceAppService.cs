using AutoMapper;
using RhyoliteERP.DomainServices.School.StudentAttendances;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.StudentAttendances.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentAttendances
{
   public class StudentAttendanceAppService: RhyoliteERPAppServiceBase, IStudentAttendanceAppService
    {

        private readonly IStudentAttendanceManager _studentAttendanceManager;
        private readonly IMapper _mapper;

        public StudentAttendanceAppService(IStudentAttendanceManager studentAttendanceManager, IMapper mapper)
        {
            _studentAttendanceManager = studentAttendanceManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _studentAttendanceManager.ListAll();
        }

        public async Task Create(CreateStudentAttendanceInput input)
        {
            var obj = _mapper.Map<StudentAttendance>(input);
            await _studentAttendanceManager.Create(obj);
        }

        public async Task MarkSingle(int tenantId, string biometricId)
        {
            await _studentAttendanceManager.MarkSingle(tenantId, biometricId);
        }

        public async Task Delete(Guid id)
        {
            await _studentAttendanceManager.Delete(id);
        }

        public async Task<IEnumerable<object>> ListAll(Guid classId, DateTime startDate, DateTime endDate)
        {
            return await _studentAttendanceManager.ListAll(classId, startDate, endDate);
        }

    }
}
