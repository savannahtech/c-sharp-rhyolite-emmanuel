using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.LeasePayments
{
    public interface ILeasePaymentManager : Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(LeasePayment input);
        Task Update(LeasePayment input);
        Task Delete(Guid Id);
    }
}
