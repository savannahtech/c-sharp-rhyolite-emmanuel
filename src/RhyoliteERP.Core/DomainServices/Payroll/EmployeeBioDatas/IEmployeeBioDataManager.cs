using RhyoliteERP.DomainServices.Payroll.EmployeeBioDatas.Dto;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeBioDatas
{
   public interface IEmployeeBioDataManager: Abp.Domain.Services.IDomainService
    {
        Task<object> ListAll();
        Task<object> Create(EmployeeBioData input);
        Task<EmployeeBioData> GetAsync(string employeeIdentifier);
        Task Update(EmployeeBioData input);
        Task UpdateStatus(Guid employeeId, Guid statusId);
        Task Delete(Guid Id);

        //export
        Task<IEnumerable<AllowanceExportDto>> ExportAllowances();
        Task<IEnumerable<SalaryInfoDto>> ExportSalaryInfo();
        Task<IEnumerable<SsnitExportDto>> ExportSsnit();
        Task<IEnumerable<DeductionsExportDto>> ExportDeductions();
    }
}
