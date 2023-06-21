using Abp.Application.Services;
using RhyoliteERP.Services.Shared.CustomerGroups.Dto;
using RhyoliteERP.Services.Shared.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Customers
{
    public interface ICustomerAppService: IApplicationService
    {
        Task<object> ListAll();
        Task<object> ListAllByGroup(Guid groupId);
        Task<object> Create(CreateCustomerInput input);
        Task Update(UpdateCustomerInput input);
        Task Delete(Guid Id);
    }
}
