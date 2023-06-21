using Abp.Application.Services;
using RhyoliteERP.Services.School.StudentStatuses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentStatuses
{
   public interface IStudentStatusAppService:IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CreateStudentStatusInput entity);
        Task<object> Update(UpdateStudentStatusInput entity);
        Task Delete(Guid Id);
    }
}
