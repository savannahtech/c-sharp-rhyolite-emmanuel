using RhyoliteERP.DomainServices.School.AcademicYears.Dto;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AcademicYears
{
   public interface IAcademicYearManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> GetAsync(Guid id);
        Task<object> Create(AcademicYear entity);
        Task Update(AcademicYear entity);
        Task Delete(Guid Id);

        //terms
        Task<object> CreateTerm(TermInput input);
        Task UpdateTerm(TermInput input);
        Task DeleteTerm(TermInput input);

    }
}
