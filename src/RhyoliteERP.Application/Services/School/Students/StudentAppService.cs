using AutoMapper;
using RhyoliteERP.DomainServices.School.Students;
using RhyoliteERP.DomainServices.School.Students.Dto;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.Students.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Students
{
   public class StudentAppService: RhyoliteERPAppServiceBase, IStudentAppService
    {
        private readonly IStudentManager _studentManager;
        private readonly IMapper _mapper;


        public StudentAppService(IStudentManager studentManager, IMapper mapper)
        {
            _studentManager = studentManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _studentManager.ListAll();
        }

        public async Task Promote(StudentPromotion studentPromotion)
        {
             await _studentManager.Promote(studentPromotion);
        }

        public async Task PromoteAlumni(AlumniStudentPromotion studentPromotion)
        {
            await _studentManager.PromoteAlumni(studentPromotion);
        }


        public async Task<object> GetAsync(Guid id)
        {
            return await _studentManager.GetAsync(id);
        }

        public async Task<IEnumerable<object>> ListAllByClass(Guid classId)
        {
            return await _studentManager.ListAllByClass(classId);
        }


        public async Task<IEnumerable<object>> ListStudsByClass(Guid classId)
        {
            return await _studentManager.ListStudsByClass(classId);
        }


        public async Task<object> Create(CreateStudentInput input)
        {
            var obj = _mapper.Map<Student>(input);

            return await _studentManager.Create(obj);
        }

        public async Task<object> Update(UpdateStudentInput input)
        {
            var obj = _mapper.Map<Student>(input);

            return await _studentManager.Update(obj);
        }

        public async Task Delete(Guid id)
        {
            await _studentManager.Delete(id);
        }


        public async Task<Guid> GetStudentId(string studentId)
        {
            return await _studentManager.GetStudentId(studentId);
        }

        public async Task<IEnumerable<object>> ListStudentsForPromotion(Guid classId)
        {
            return await _studentManager.ListStudentsForPromotion(classId);

        }

        public async Task<IEnumerable<object>> OpeningBalanceExcelExport(Guid classId)
        {
            return await _studentManager.OpeningBalanceExcelExport(classId);
        }

        public async Task<IEnumerable<object>> EnqStudentsByClass(Guid classId)
        {
            return await _studentManager.EnqStudsByClass(classId);
        }

        public async Task<IEnumerable<object>> EnqStudentsByNationality(Guid nationalityId)
        {
            return await _studentManager.EnqStudsByNationality(nationalityId);
        }

        public async Task<IEnumerable<object>> EnqStudentsByReligion(Guid nationalityId)
        {
            return await _studentManager.EnqStudsByReligion(nationalityId);
        }

        public async Task<IEnumerable<object>> AttitudeExcelExport(Guid classId)
        {
            return await _studentManager.AttitudeExcelExport(classId);
        }

        public async Task<IEnumerable<object>> ConductsExcelExport(Guid classId)
        {
            return await _studentManager.ConductsExcelExport(classId);
        }

        public async Task<IEnumerable<object>> TeacherRemarksExcelExport(Guid classId)
        {
            return await _studentManager.TeacherRemarksExcelExport(classId);
        }

        public async Task<IEnumerable<object>> ResultsUploadExcelExport(Guid classId)
        {
            return await _studentManager.ResultsUploadExcelExport(classId);
        }
    }
}
