using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Payroll.EmployeeBioDatas.Dto;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeBioDatas
{
   public class EmployeeBioDataManager: Abp.Domain.Services.DomainService, IEmployeeBioDataManager
    {

        private readonly IRepository<EmployeeBioData, Guid> _repositoryEmployeeBioData;
        private readonly IRepository<StudentStatus, Guid> _repositoryStudentStatus;
        private readonly IRepository<SystemNumber, Guid> _repositorySystemNumber;

        public EmployeeBioDataManager(IRepository<EmployeeBioData, Guid> repositoryEmployeeBioData, IRepository<StudentStatus, Guid> repositoryStudentStatus, IRepository<SystemNumber, Guid> repositorySystemNumber)
        {
            _repositoryEmployeeBioData = repositoryEmployeeBioData;
            _repositoryStudentStatus = repositoryStudentStatus;
            _repositorySystemNumber = repositorySystemNumber;
        }

        public async Task<object> Create(EmployeeBioData entity)
        {
            var datta = await _repositoryEmployeeBioData.FirstOrDefaultAsync(x => x.EmployeeIdentifier == entity.EmployeeIdentifier);

            if (datta == null)
            {
                //get default status
                var defaultStatus = await _repositoryStudentStatus.FirstOrDefaultAsync(x=>x.IsDefault);

                if (defaultStatus != null && entity.StatusId == Guid.Empty)
                {
                    entity.StatusName = defaultStatus.Name;
                    entity.StatusId= defaultStatus.Id;
                }

                entity.FirstName = entity.FirstName.Trim();
                entity.LastName = entity.LastName.Trim();

                await _repositoryEmployeeBioData.InsertAsync(entity);

                //update system numbers
                var systemNumberInfo = await _repositorySystemNumber.FirstOrDefaultAsync(x => x.ItemName == "EmployeeID");

                systemNumberInfo.LastNo = systemNumberInfo.LastNo + 1;

                await _repositorySystemNumber.UpdateAsync(systemNumberInfo);
                return new
                {
                    code = 200,
                    message = "successful"
                };
            }
            else
            {
                return new
                {
                    code = 400,
                    message = "Duplicate Employee ID's are not allowed."
                };
            }
        }

        public async Task Update(EmployeeBioData entity)
        {
            entity.FirstName = entity.FirstName.Trim();
            entity.LastName = entity.LastName.Trim();

            await _repositoryEmployeeBioData.UpdateAsync(entity);
        }

        public async Task UpdateStatus(Guid employeeId, Guid statusId)
        {
            var employeeInfo = await _repositoryEmployeeBioData.FirstOrDefaultAsync(employeeId);
            if (employeeInfo != null)
            {
                employeeInfo.StatusId = statusId;

                await _repositoryEmployeeBioData.UpdateAsync(employeeInfo);
            }
           

        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryEmployeeBioData.FirstOrDefaultAsync(id);
        }

        public async Task<EmployeeBioData> GetAsync(string employeeIdentifier)
        {
            return await _repositoryEmployeeBioData.FirstOrDefaultAsync(x=> !string.IsNullOrEmpty(employeeIdentifier) && x.EmployeeIdentifier == employeeIdentifier.Trim());
        }

        public async Task<object> ListAll()
        {
            return await _repositoryEmployeeBioData.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryEmployeeBioData.DeleteAsync(id);

        }

        //export data
        public async Task<IEnumerable<AllowanceExportDto>> ExportAllowances()
        {
            var datta =  await _repositoryEmployeeBioData.GetAllListAsync();

            var query = from u1 in datta 
                        select new AllowanceExportDto
                        {
                            EmployeeIdentifier = u1.EmployeeIdentifier,
                            Amount = 0,
                            IsTaxable = "Yes",
                            AllowanceDays = 0,
                            AllowanceType = string.Empty,
                            EmployeeName = string.IsNullOrEmpty(u1.OtherName) ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.OtherName}",
                        };

            return query.OrderBy(a => a.EmployeeName).ToList();
        }

        public async Task<IEnumerable<DeductionsExportDto>> ExportDeductions()
        {
            var datta = await _repositoryEmployeeBioData.GetAllListAsync();

            var query = from u1 in datta
                        select new DeductionsExportDto
                        {
                            EmployeeIdentifier = u1.EmployeeIdentifier,
                            Amount = 0,
                            EmployerAmount = 0,
                            DeductionType = string.Empty,
                            EmployeeName = string.IsNullOrEmpty(u1.OtherName) ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.OtherName}",
                        };

            return query.OrderBy(a => a.EmployeeName).ToList();
        }


        public async Task<IEnumerable<SalaryInfoDto>> ExportSalaryInfo()
        {
            var datta = await _repositoryEmployeeBioData.GetAllListAsync();

            var query = from u1 in datta
                        select new SalaryInfoDto
                        {
                            EmployeeIdentifier = u1.EmployeeIdentifier,
                            SalaryType = "Salaried/Hourly",
                            PayType = "Cheque/Cash/Mobile Wallet",
                            AccountNumber = string.Empty,
                            BankName = string.Empty,
                            Currency = string.Empty,
                            MonthlySalary = 0,
                            EmployeeName = string.IsNullOrEmpty(u1.OtherName) ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.OtherName}",
                        };

            return query.OrderBy(a => a.EmployeeName).ToList();
        }

        public async Task<IEnumerable<SsnitExportDto>> ExportSsnit()
        {
            var datta = await _repositoryEmployeeBioData.GetAllListAsync();

            var query = from u1 in datta
                        select new SsnitExportDto
                        {
                            EmployeeIdentifier = u1.EmployeeIdentifier,
                            SocialSecurityNo = string.Empty,
                            SocialSecurityFundEmployeeContribution = 0,
                            SocialSecurityFundEmployerContribution = 0,
                            ProvidentFundEmployeeContribution = 0,
                            ProvidentFundEmployerContribution = 0,
                            SecondProvidentFundEmployeeContribution = 0,
                            SecondProvidentFundEmployerContribution = 0,
                            SuperAnnuationEmployeeContribution = 0,
                            SuperAnnuationEmployerContribution = 0,
                            EmployeeName = string.IsNullOrEmpty(u1.OtherName) ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.OtherName}",
                        };

            return query.OrderBy(a => a.EmployeeName).ToList();
        }

    }
}
