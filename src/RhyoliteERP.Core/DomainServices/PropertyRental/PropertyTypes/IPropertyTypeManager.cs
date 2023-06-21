using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyTypes
{
    public interface IPropertyTypeManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(PropertyType input);
        Task Update(PropertyType input);
        Task Delete(Guid Id);

    }
}
