using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.TeacherRemarks
{
   public interface ITeacherRemarkManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(TeacherRemark input);
        Task Update(TeacherRemark input);
        Task Delete(Guid Id);
    }
}
