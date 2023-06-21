using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.Leases
{
    public interface ILeaseManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(Lease input);
        Task Update(Lease input);
        Task Delete(Guid Id);
    }
}
