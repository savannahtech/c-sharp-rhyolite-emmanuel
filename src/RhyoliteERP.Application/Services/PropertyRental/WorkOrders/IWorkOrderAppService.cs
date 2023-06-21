using RhyoliteERP.Services.PropertyRental.Vendors.Dto;
using RhyoliteERP.Services.PropertyRental.WorkOrders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.WorkOrders
{
    public interface IWorkOrderAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateWorkOrderInput input);
        Task Update(UpdateWorkOrderInput input);
        Task Delete(Guid Id);

    }
}
