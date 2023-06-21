using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeCategories
{
   public interface IEmployeeCategoryManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(EmployeeCategory input);
        Task Update(EmployeeCategory input);
        Task Delete(Guid Id);
    }
}
