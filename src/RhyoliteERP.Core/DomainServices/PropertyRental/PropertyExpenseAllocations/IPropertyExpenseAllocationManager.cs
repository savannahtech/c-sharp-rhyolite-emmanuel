using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyExpenseAllocations
{
    public interface IPropertyExpenseAllocationManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(PropertyExpenseAllocation input);
        Task Update(PropertyExpenseAllocation input);
        Task Delete(Guid Id);
    }
}
