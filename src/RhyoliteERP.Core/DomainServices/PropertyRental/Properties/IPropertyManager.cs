using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.Properties
{
    public interface IPropertyManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> ListAll(bool isRented);
        Task<object> Create(Property input);
        Task Update(Property input);
        Task Delete(Guid Id);
    }
}
