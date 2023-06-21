using Abp.Application.Services;
using RhyoliteERP.Services.Shared.CustomerGroups.Dto;
using RhyoliteERP.Services.Shared.Religions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.CustomerGroups
{
    public interface ICustomerGroupAppService: IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateCustomerGroupInput input);
        Task Update(UpdateCustomerGroupInput input);
        Task Delete(Guid Id);
    }
}
