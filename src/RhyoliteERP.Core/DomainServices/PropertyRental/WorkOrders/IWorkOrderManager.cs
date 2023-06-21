using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.WorkOrders
{
    public interface IWorkOrderManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(WorkOrder input);
        Task Update(WorkOrder input);
        Task Delete(Guid Id);
    }
}
