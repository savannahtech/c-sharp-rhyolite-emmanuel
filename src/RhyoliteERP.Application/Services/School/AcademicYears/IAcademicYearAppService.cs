using RhyoliteERP.DomainServices.School.AcademicYears.Dto;
using RhyoliteERP.Services.School.AcademicYears.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.AcademicYears
{
   public interface IAcademicYearAppService: Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> GetAsync(Guid id);

        Task<object> Create(CreateAcademicYearInput input);
        Task Update(UpdateAcademicYearInput input);
        Task Delete(Guid Id);
        //
        Task<object> CreateTerm(TermInput input);
        Task UpdateTerm(TermInput input);
        Task DeleteTerm(TermInput input);
    }
}
