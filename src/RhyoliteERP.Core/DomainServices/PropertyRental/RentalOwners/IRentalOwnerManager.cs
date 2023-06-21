using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.RentalOwners
{
    public interface IRentalOwnerManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(RentalOwner input);
        Task Update(RentalOwner input);
        Task Delete(Guid Id);

    }
}
