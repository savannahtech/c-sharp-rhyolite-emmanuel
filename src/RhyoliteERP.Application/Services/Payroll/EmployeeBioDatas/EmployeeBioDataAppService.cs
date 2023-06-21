using AutoMapper;
using RhyoliteERP.DomainServices.Payroll.EmployeeBioDatas;
using RhyoliteERP.DomainServices.Payroll.EmployeeBioDatas.Dto;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Services.Payroll.EmployeeBioDatas.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Payroll.EmployeeBioDatas
{
   public class EmployeeBioDataAppService: RhyoliteERPAppServiceBase, IEmployeeBioDataAppService
    {
        private readonly IEmployeeBioDataManager _employeeBioDataManager;
        private readonly IMapper _mapper;

        public EmployeeBioDataAppService(IEmployeeBioDataManager employeeBioDataManager, IMapper mapper)
        {
            _employeeBioDataManager = employeeBioDataManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _employeeBioDataManager.ListAll();
        }

        public async Task<object> Create(CreateEmployeeBioDataInput input)
        {
            var obj = _mapper.Map<EmployeeBioData>(input);
            return await _employeeBioDataManager.Create(obj);
        }

        public async Task Update(UpdateEmployeeBioDataInput input)
        {
            var obj = _mapper.Map<EmployeeBioData>(input);
            await _employeeBioDataManager.Update(obj);
        }

        public async Task UpdateStatus(Guid employeeId, Guid statusId)
        {
            await _employeeBioDataManager.UpdateStatus(employeeId, statusId);
        }

        public async Task Delete(Guid Id)
        {
            await _employeeBioDataManager.Delete(Id);

        }

        // export ...
        public async Task<IEnumerable<AllowanceExportDto>> ExportAllowances()
        {
            return await _employeeBioDataManager.ExportAllowances();
        }

        public async Task<IEnumerable<SalaryInfoDto>> ExportSalaryInfo()
        {
            return await _employeeBioDataManager.ExportSalaryInfo();
        }

        public async Task<IEnumerable<SsnitExportDto>> ExportSsnit()
        {
            return await _employeeBioDataManager.ExportSsnit();
        }

        public async Task<IEnumerable<DeductionsExportDto>> ExportDeductions()
        {
            return await _employeeBioDataManager.ExportDeductions();

        }

    }
}
