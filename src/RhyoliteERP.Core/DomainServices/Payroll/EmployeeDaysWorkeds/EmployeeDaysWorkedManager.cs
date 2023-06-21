using Abp.Domain.Repositories;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.EmployeeDaysWorkeds
{
   public class EmployeeDaysWorkedManager : Abp.Domain.Services.DomainService, IEmployeeDaysWorkedManager
    {
        private readonly IRepository<EmployeeDaysWorked, Guid> _repositoryEmployeeDaysWorked;
        private readonly IRepository<EmployeeBioData, Guid> _repositoryEmployeeBioData;
        private readonly IRepository<EmployeeSalaryInfo, Guid> _repositoryEmployeeSalaryInfo;

        public EmployeeDaysWorkedManager(IRepository<EmployeeDaysWorked, Guid> repositoryEmployeeDaysWorked, IRepository<EmployeeBioData, Guid> repositoryEmployeeBioData, IRepository<EmployeeSalaryInfo, Guid> repositoryEmployeeSalaryInfo)
        {
            _repositoryEmployeeDaysWorked = repositoryEmployeeDaysWorked;
            _repositoryEmployeeBioData = repositoryEmployeeBioData;
            _repositoryEmployeeSalaryInfo = repositoryEmployeeSalaryInfo;
        }

        public async Task Create(EmployeeDaysWorked entity)
        {

            var datta = await _repositoryEmployeeDaysWorked.FirstOrDefaultAsync(x => x.EmployeeId == entity.EmployeeId && x.Month == entity.Month && x.Year == entity.Year);
            if (datta == null)
            {
                var employeeInfo = await _repositoryEmployeeBioData.FirstOrDefaultAsync(entity.EmployeeId);

                if (employeeInfo != null)
                {
                    entity.EmployeeIdentifier = employeeInfo.EmployeeIdentifier;
                    entity.EmployeeCategory = employeeInfo.CategoryName;
                    entity.EmployeeDepartment = employeeInfo.DepartmentName;
                    entity.EmployeeName = string.IsNullOrEmpty(employeeInfo.OtherName) ? $"{employeeInfo.LastName} {employeeInfo.FirstName}" : $"{employeeInfo.LastName} {employeeInfo.FirstName} {employeeInfo.OtherName}";

                }
                await _repositoryEmployeeDaysWorked.InsertAsync(entity);

            }
            else
            {
                datta.Days = entity.Days;
                datta.Hours = entity.Hours;
                datta.Minutes = entity.Minutes;
                await _repositoryEmployeeDaysWorked.UpdateAsync(datta);

            }
        }

        public async Task Update(EmployeeDaysWorked entity)
        {
            var employeeInfo = await _repositoryEmployeeBioData.FirstOrDefaultAsync(entity.EmployeeId);

            if (employeeInfo != null)
            {
                entity.EmployeeIdentifier = employeeInfo.EmployeeIdentifier;
                entity.EmployeeCategory = employeeInfo.CategoryName;
                entity.EmployeeDepartment = employeeInfo.DepartmentName;
                entity.EmployeeName = string.IsNullOrEmpty(employeeInfo.OtherName) ? $"{employeeInfo.LastName} {employeeInfo.FirstName}" : $"{employeeInfo.LastName} {employeeInfo.FirstName} {employeeInfo.OtherName}";

            }

            await _repositoryEmployeeDaysWorked.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryEmployeeDaysWorked.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryEmployeeDaysWorked.GetAllListAsync();
        }

        public async Task<object> ListAll(int month, int year, string salaryType)
        {
            var employeeDaysWorked  = await _repositoryEmployeeDaysWorked.GetAllListAsync(a => a.Month == month && a.Year == year);
            var employees = await _repositoryEmployeeBioData.GetAllListAsync();
            var employeeSalaryData = await _repositoryEmployeeSalaryInfo.GetAllListAsync(a => a.SalaryType == salaryType);

            var query = from u1 in employees
                        join u2 in employeeSalaryData on u1.Id equals u2.EmployeeId
                        join u3 in employeeDaysWorked on u1.Id equals u3.EmployeeId into aa
                        from f in aa.DefaultIfEmpty()
                        select new
                        {
                            u1.EmployeeIdentifier,
                            u1.LastName,
                            u1.FirstName,
                            u1.OtherName,
                            EmployeeName = string.IsNullOrEmpty(u1.OtherName) ? $"{u1.LastName} {u1.FirstName}" : $"{u1.LastName} {u1.FirstName} {u1.OtherName}",
                            EmployeeId = u1.Id,
                            Days = f == null ? 0 : f.Days,
                            Hours = f == null ? 0 : f.Hours,
                            Minutes = f == null ? 0 : f.Minutes,
                            u1.TenantId
                        };

            return query.OrderBy(a => a.EmployeeName).ToList();

        }
        public async Task Delete(Guid id)
        {
            await _repositoryEmployeeDaysWorked.DeleteAsync(id);

        }
    }
}
