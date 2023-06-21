using Abp.Domain.Repositories;
using Castle.MicroKernel.Registration;
using RhyoliteERP.DomainServices.Payroll.DeductionTypes.Dto;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.DeductionTypes
{
   public class DeductionTypeManager: Abp.Domain.Services.DomainService, IDeductionTypeManager
    {
        private readonly IRepository<DeductionType, Guid> _repositoryDeductionType;
        private readonly IRepository<EmployeeSalaryInfo, Guid> _repositoryEmployeeSalaryInfo;

        public DeductionTypeManager(IRepository<DeductionType, Guid> repositoryDeductionType, IRepository<EmployeeSalaryInfo, Guid> repositoryEmployeeSalaryInfo)
        {
            _repositoryDeductionType = repositoryDeductionType;
            _repositoryEmployeeSalaryInfo = repositoryEmployeeSalaryInfo;
        }

        public async Task<object> Create(DeductionType entity)
        {
            var datta = await _repositoryDeductionType.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryDeductionType.InsertAsync(entity);

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

        public async Task<object> CreateDeductionRate(DeductionRateInput input)
        {
            var deductionTypeData = await _repositoryDeductionType.GetAsync(input.DeductionTypeId);
            var ratesList = deductionTypeData.Rates;
            var rateInfo = ratesList.FirstOrDefault(x => x.Id == input.Id);

            if (rateInfo == null)
            {
                deductionTypeData.Rates.Add(new DeductionRate
                {
                    Id = Guid.NewGuid(),
                    Amount = input.Amount,
                    DeductionTypeId = input.DeductionTypeId,
                    DeductionTypeName = deductionTypeData.Name,
                    EmployeeCategoryId = input.EmployeeCategoryId,
                    EmployeeCategoryName = input.EmployeeCategoryName,
                    FixedAmount = input.FixedAmount,
                    MaximumAmount = input.MaximumAmount,
                    PercentageBasic = input.PercentageBasic,
                    Prorate = input.Prorate,

                });

                await _repositoryDeductionType.UpdateAsync(deductionTypeData);

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

        public async Task Update(DeductionType entity)
        {
            await _repositoryDeductionType.UpdateAsync(entity);
        }


        public async Task UpdateDeductionRate(DeductionRateInput input)
        {
            var deductionTypeData = await _repositoryDeductionType.GetAsync(input.DeductionTypeId);
            var ratesList = deductionTypeData.Rates;
            var rateInfo = ratesList.FirstOrDefault(x => x.Id == input.Id);

            ratesList.Remove(rateInfo);

            rateInfo.Amount = input.Amount;
            rateInfo.EmployeeCategoryId = input.EmployeeCategoryId;
            rateInfo.DeductionTypeName = deductionTypeData.Name;
            rateInfo.EmployeeCategoryName = input.EmployeeCategoryName;
            rateInfo.FixedAmount = input.FixedAmount;
            rateInfo.MaximumAmount = input.MaximumAmount;
            rateInfo.PercentageBasic = input.PercentageBasic;
            rateInfo.Prorate = input.Prorate;

            ratesList.Add(rateInfo);

            deductionTypeData.Rates = ratesList;

            await _repositoryDeductionType.UpdateAsync(deductionTypeData);
        }


        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryDeductionType.FirstOrDefaultAsync(id);
        }

        public async Task<DeductionType> GetAsync(string name)
        {
            return await _repositoryDeductionType.FirstOrDefaultAsync(x => x.Name.ToLower() == name.Trim().ToLower());
        }
        public async Task<object> ListAll()
        {
            return await _repositoryDeductionType.GetAllListAsync();
        }

        public async Task<object> ListAll(Guid deductionTypeId, Guid employeeId, Guid categoryId)
        {

            List<DeductionRate> deductionRates = new List<DeductionRate>();

            var deductionTypes = await _repositoryDeductionType.GetAllListAsync();

            deductionTypes.ForEach((a) =>
            {
                deductionRates.AddRange(a.Rates);
            });

            var deductionRate = deductionRates.FirstOrDefault(o => categoryId != Guid.Empty && o.EmployeeCategoryId == categoryId && o.DeductionTypeId == deductionTypeId);

            var salaryInfo = await _repositoryEmployeeSalaryInfo.FirstOrDefaultAsync(a => a.EmployeeId == employeeId);

            if (deductionRate != null)
            {
                if (deductionRate.FixedAmount)
                {
                    deductionRate.Amount = deductionRate.Amount;
                    if (deductionRate.MaximumAmount != 0 && deductionRate.Amount > deductionRate.MaximumAmount)
                    {
                        deductionRate.Amount = deductionRate.MaximumAmount;
                    }
                }
                else if (!deductionRate.FixedAmount)
                {
                    deductionRate.Amount = (deductionRate.PercentageBasic * salaryInfo.MonthlySalary) / 100;
                    if (deductionRate.MaximumAmount != 0 && deductionRate.Amount > deductionRate.MaximumAmount)
                    {
                        deductionRate.Amount = deductionRate.MaximumAmount;
                    }
                }
            }

            return deductionRate;
        }

        public async Task<object> ListAll(Guid categoryId)
        {
            List<DeductionRate> deductionRates = new List<DeductionRate>();

            List<DeductionType> deductionTypes = new List<DeductionType>();

            var data = await _repositoryDeductionType.GetAllListAsync();

            data.ForEach((a) =>
            {
                deductionRates.AddRange(a.Rates);
            });

            var filteredDeductionRates = deductionRates.Where(o => categoryId != Guid.Empty && o.EmployeeCategoryId == categoryId).ToList();

            filteredDeductionRates.ForEach((a) =>
            {
                var deductionType = data.FirstOrDefault(x=> x.Id == a.DeductionTypeId);
                deductionTypes.Add(deductionType);

            });

            return deductionTypes;

        }

        public async Task Delete(Guid id)
        {
            await _repositoryDeductionType.DeleteAsync(id);
        }

        public async Task DeleteDeductionRate(DeductionRateInput input)
        {
            var deductionTypeData = await _repositoryDeductionType.GetAsync(input.DeductionTypeId);
            var ratesList = deductionTypeData.Rates;
            var rateInfo = ratesList.FirstOrDefault(x => x.Id == input.Id);
            ratesList.Remove(rateInfo);

            deductionTypeData.Rates = ratesList;

            await _repositoryDeductionType.UpdateAsync(deductionTypeData);

        }
    }
}
