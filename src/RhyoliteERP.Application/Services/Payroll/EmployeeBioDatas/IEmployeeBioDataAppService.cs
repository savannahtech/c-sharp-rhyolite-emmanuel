using RhyoliteERP.DomainServices.Payroll.EmployeeBioDatas.Dto;
using RhyoliteERP.Services.Payroll.EmployeeBioDatas.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeBioDatas
{
   public interface IEmployeeBioDataAppService: Abp.Application.Services.IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateEmployeeBioDataInput input);
        Task Update(UpdateEmployeeBioDataInput input);
        Task UpdateStatus(Guid employeeId, Guid statusId);
        Task Delete(Guid Id);

        //export 
        Task<IEnumerable<AllowanceExportDto>> ExportAllowances();
        Task<IEnumerable<SalaryInfoDto>> ExportSalaryInfo();
        Task<IEnumerable<SsnitExportDto>> ExportSsnit();
        Task<IEnumerable<DeductionsExportDto>> ExportDeductions();

    }
}
