using Abp.Application.Services;
using RhyoliteERP.Services.School.StudentParents.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StudentParents
{
   public interface IStudentParentAppService: IApplicationService
    {
        Task<object> GetStudentParent(Guid id);
        Task Create(CreateStudentParentInput entity);
        Task Update(UpdateStudentParentInput entity);
        Task Delete(Guid Id);

        //list parent's children
        Task<IEnumerable<Guid>> ListChildren(Guid parentId);
    }
}
