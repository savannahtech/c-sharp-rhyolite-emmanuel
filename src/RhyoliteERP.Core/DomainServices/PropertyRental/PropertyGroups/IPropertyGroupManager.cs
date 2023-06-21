using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyGroups
{
    public interface IPropertyGroupManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(PropertyGroup input);
        Task Update(PropertyGroup input);
        Task Delete(Guid Id);

    }
}
