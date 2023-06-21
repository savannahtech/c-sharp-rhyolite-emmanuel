using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.PropertyTasks
{
    public interface IPropertyTaskManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task Create(PropertyTask input);
        Task Update(PropertyTask input);
        Task Delete(Guid Id);
    }
}
