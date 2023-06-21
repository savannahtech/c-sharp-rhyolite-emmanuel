using RhyoliteERP.Services.PropertyRental.VendorCategories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.VendorCategories
{
    public interface IVendorCategoryAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateVendorCategoryInput input);
        Task Update(UpdateVendorCategoryInput input);
        Task Delete(Guid Id);
    }
}
