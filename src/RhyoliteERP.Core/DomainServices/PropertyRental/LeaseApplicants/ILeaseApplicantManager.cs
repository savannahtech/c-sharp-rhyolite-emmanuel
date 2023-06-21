using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.LeaseApplicants
{
    public interface ILeaseApplicantManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(LeaseApplicant entity);
        Task Delete(Guid Id);

    }
}
