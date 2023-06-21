using RhyoliteERP.Services.PropertyRental.Vendors.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.Vendors
{
    public interface IVendorAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateVendorInput input);
        Task Update(UpdateVendorInput input);
        Task Delete(Guid Id);
    }
}
