using Abp.Application.Services;
using RhyoliteERP.Services.School.StaffDesignations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.StaffDesignations
{
   public interface IStaffDesignationAppService: IApplicationService
    {
        Task<IEnumerable<object>> ListAll();
        Task<object> Create(CreateStaffDesignationInput entity);
        Task Update(UpdateStaffDesignationInput entity);
        Task Delete(Guid Id);
    }
}
