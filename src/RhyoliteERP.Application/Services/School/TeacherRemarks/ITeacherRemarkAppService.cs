using RhyoliteERP.Services.School.TeacherRemarks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.TeacherRemarks
{
   public interface ITeacherRemarkAppService: Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(CreateTeacherRemarkInput input);
        Task Update(UpdateTeacherRemarkInput input);
        Task Delete(Guid Id);
    }
}
