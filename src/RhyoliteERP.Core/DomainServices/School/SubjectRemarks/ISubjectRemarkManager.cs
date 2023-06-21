using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.SubjectRemarks
{
   public interface ISubjectRemarkManager : Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(SubjectRemark entity);
        Task Update(SubjectRemark entity);
        Task Delete(Guid Id);
    }
}
