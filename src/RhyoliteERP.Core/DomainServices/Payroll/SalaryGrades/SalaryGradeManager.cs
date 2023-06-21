using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Payroll.SalaryGrades.Dto;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.SalaryGrades
{
   public class SalaryGradeManager : Abp.Domain.Services.DomainService, ISalaryGradeManager
    {
        private readonly IRepository<SalaryGrade, Guid> _repositorySalaryGrade;

        public SalaryGradeManager(IRepository<SalaryGrade, Guid> repositorySalaryGrade)
        {
            _repositorySalaryGrade = repositorySalaryGrade;
        }

        public async Task<object> Create(SalaryGrade entity)
        {
            var datta = await _repositorySalaryGrade.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositorySalaryGrade.InsertAsync(entity);

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
                    message = "Duplicate records are not allowed."
                };
            }
        }

        public async Task<object> CreateSalaryNotch(SalaryNotchInput input)
        {
            var salaryGradeData = await _repositorySalaryGrade.GetAsync(input.SalaryGradeId);
            var notchList = salaryGradeData.SalaryNotches;
            var notchInfo = notchList.FirstOrDefault(x => x.Id == input.Id);

            if (notchInfo == null)
            {
                salaryGradeData.SalaryNotches.Add(new SalaryNotch
                {
                    Id = Guid.NewGuid(),
                    SalaryGradeId = input.SalaryGradeId,
                    SalaryGradeName = salaryGradeData.Name,
                    Notch = input.Notch,
                    Salary = input.Salary,

                });

                await _repositorySalaryGrade.UpdateAsync(salaryGradeData);

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
                    message = "Duplicate records are not allowed."
                };
            }
        }
        public async Task Update(SalaryGrade entity)
        {
            await _repositorySalaryGrade.UpdateAsync(entity);
        }

        public async Task UpdateSalaryNotch(SalaryNotchInput input)
        {
            var salaryGradeData = await _repositorySalaryGrade.GetAsync(input.SalaryGradeId);
            var notchList = salaryGradeData.SalaryNotches;
            var notchInfo = notchList.FirstOrDefault(x => x.Id == input.Id);

            notchList.Remove(notchInfo);

            notchInfo.SalaryGradeName = salaryGradeData.Name;
            notchInfo.Notch = input.Notch;
            notchInfo.Salary = input.Salary;

            notchList.Add(notchInfo);

            salaryGradeData.SalaryNotches = notchList;

            await _repositorySalaryGrade.UpdateAsync(salaryGradeData);
        }
        public async Task<object> GetAsync(Guid id)
        {
            return await _repositorySalaryGrade.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositorySalaryGrade.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositorySalaryGrade.DeleteAsync(id);
        }

        public async Task DeleteSalaryNotch(SalaryNotchInput input)
        {
            var salaryGradeData = await _repositorySalaryGrade.GetAsync(input.SalaryGradeId);
            var notchList = salaryGradeData.SalaryNotches;
            var notchInfo = notchList.FirstOrDefault(x => x.Id == input.Id);

            notchList.Remove(notchInfo);

            salaryGradeData.SalaryNotches = notchList;

            await _repositorySalaryGrade.UpdateAsync(salaryGradeData);

        }
    
    
    }
}
