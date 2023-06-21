using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.VendorCategories
{
    public interface IVendorCategoryManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(VendorCategory input);
        Task Update(VendorCategory input);
        Task Delete(Guid Id);
    }
}
