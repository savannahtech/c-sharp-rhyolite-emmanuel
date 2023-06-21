using RhyoliteERP.Services.PropertyRental.PropertyTasks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.PropertyTasks
{
    public interface IPropertyTaskAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreatePropertyTaskInput input);
        Task Update(UpdatePropertyTaskInput input);
        Task Delete(Guid Id);

    }
}
