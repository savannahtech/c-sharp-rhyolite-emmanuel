using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.TaxTables
{
   public interface ITaxTableManager: Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<object>> ListAll();
        Task Create(TaxTable input);
        Task Update(TaxTable input);
        Task Delete(Guid Id);
    }
}
