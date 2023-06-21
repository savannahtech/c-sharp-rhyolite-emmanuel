using Abp.Application.Services;
using RhyoliteERP.Services.School.Staffs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Staffs
{
   public interface IStaffAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll(bool isTeachingStaff);
        Task<object> GetAsync(Guid id);
        Task<object> Create(CreateStaffInput input);
        Task Update(UpdateStaffInput input);
        Task Delete(Guid Id);
    }
}
