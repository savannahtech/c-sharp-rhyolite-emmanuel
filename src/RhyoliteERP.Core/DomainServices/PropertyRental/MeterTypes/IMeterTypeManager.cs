using RhyoliteERP.Models.PropertyRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.MeterTypes
{
    public interface IMeterTypeManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(MeterType input);
        Task Update(MeterType input);
        Task Delete(Guid Id);
    }
}
