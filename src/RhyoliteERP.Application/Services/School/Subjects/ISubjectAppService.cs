using Abp.Application.Services;
using RhyoliteERP.Services.School.Subjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Subjects
{
   public interface ISubjectAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CreateSubjectInput input);
        Task Update(UpdateSubjectInput input);
        Task Delete(Guid Id);
    }
}
