using Abp.Application.Services;
using RhyoliteERP.Services.Shared.Religions.Dto;
using RhyoliteERP.Services.Shared.SupplierGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.SupplierGroups
{
    public interface ISupplierGroupAppService: IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateSupplierGroupInput input);
        Task Update(UpdateSupplierGroupInput input);
        Task Delete(Guid Id);
    }
}
