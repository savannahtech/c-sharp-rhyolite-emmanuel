using AutoMapper;
using RhyoliteERP.DomainServices.School.AcademicYears;
using RhyoliteERP.DomainServices.School.AcademicYears.Dto;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.AcademicYears.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.AcademicYears
{
   public class AcademicYearAppService: RhyoliteERPAppServiceBase, IAcademicYearAppService
    {

        private readonly IAcademicYearManager _academicYearManager;
        private readonly IMapper _mapper;

        public AcademicYearAppService(IAcademicYearManager academicYearManager, IMapper mapper)
        {
            _academicYearManager = academicYearManager;
            _mapper = mapper;
        }

        public async Task<object> Create(CreateAcademicYearInput input)
        {
            var obj = _mapper.Map<AcademicYear>(input);
            return await _academicYearManager.Create(obj);

        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _academicYearManager.GetAsync(id);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _academicYearManager.ListAll();

        }

        public async Task Update(UpdateAcademicYearInput input)
        {
            var obj = _mapper.Map<AcademicYear>(input);
            await _academicYearManager.Update(obj);
        }

        public async Task Delete(Guid id)
        {
            await _academicYearManager.Delete(id);
        }

        //terms
        public async Task<object> CreateTerm(TermInput input)
        {
           return await _academicYearManager.CreateTerm(input);
        }

        public async Task UpdateTerm(TermInput input)
        {
             await _academicYearManager.UpdateTerm(input);
        }

        public async Task DeleteTerm(TermInput input)
        {
            await _academicYearManager.DeleteTerm(input);
        }




    }
}
