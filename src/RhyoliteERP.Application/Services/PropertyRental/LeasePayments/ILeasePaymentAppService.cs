using RhyoliteERP.Services.PropertyRental.LeasePayments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.LeasePayments
{
    public interface ILeasePaymentAppService : Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task Create(CreateLeasePaymentInput input);
        Task Delete(Guid Id);
    }
}
