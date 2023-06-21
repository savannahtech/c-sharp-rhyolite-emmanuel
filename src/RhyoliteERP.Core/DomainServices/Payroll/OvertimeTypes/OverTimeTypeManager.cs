using Abp.Domain.Repositories;
using RhyoliteERP.DomainServices.Payroll.DeductionTypes.Dto;
using RhyoliteERP.DomainServices.Payroll.OvertimeTypes.Dto;
using RhyoliteERP.Models.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.OvertimeTypes
{
   public class OverTimeTypeManager : Abp.Domain.Services.DomainService, IOverTimeTypeManager
    {
        private readonly IRepository<OvertimeType, Guid> _repositoryOverTimeType;

        public OverTimeTypeManager(IRepository<OvertimeType, Guid> repositoryOverTimeType)
        {
            _repositoryOverTimeType = repositoryOverTimeType;
        }

        public async Task<object> Create(OvertimeType entity)
        {
            var datta = await _repositoryOverTimeType.FirstOrDefaultAsync(x => x.Name == entity.Name);

            if (datta == null)
            {
                await _repositoryOverTimeType.InsertAsync(entity);

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

        public async Task<object> CreateOvertimeRate(OvertimeRateInput input)
        {
            var overtimeTypeData = await _repositoryOverTimeType.GetAsync(input.OvertimeTypeId);
            var ratesList = overtimeTypeData.Rates;
            var rateInfo = ratesList.FirstOrDefault(x => x.Id == input.Id);

            if (rateInfo == null)
            {
                overtimeTypeData.Rates.Add(new OvertimeRate
                {
                    Id = Guid.NewGuid(),
                    Amount = input.Amount,
                    OvertimeTypeId = input.OvertimeTypeId,
                    OvertimeTypeName = overtimeTypeData.Name,
                    EmployeeCategoryId = input.EmployeeCategoryId,
                    EmployeeCategoryName = input.EmployeeCategoryName,
                    FixedAmount = input.FixedAmount,
                    MaximumAmount = input.MaximumAmount,
                    PercentageBasic = input.PercentageBasic,
                    IsFactor = input.IsFactor,
                    AnnualHours = input.AnnualHours,
                    PercentageLimitOfBasic= input.PercentageLimitOfBasic,
                    Prorate = input.Prorate,

                });

                await _repositoryOverTimeType.UpdateAsync(overtimeTypeData);

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

        public async Task Update(OvertimeType entity)
        {
            await _repositoryOverTimeType.UpdateAsync(entity);
        }

        public async Task UpdateOvertimeRate(OvertimeRateInput input)
        {
            var overtimeTypeData = await _repositoryOverTimeType.GetAsync(input.OvertimeTypeId);
            var ratesList = overtimeTypeData.Rates;
            var rateInfo = ratesList.FirstOrDefault(x => x.Id == input.Id);

            ratesList.Remove(rateInfo);

            rateInfo.Amount = input.Amount;
            rateInfo.EmployeeCategoryId = input.EmployeeCategoryId;
            rateInfo.OvertimeTypeName = overtimeTypeData.Name;
            rateInfo.EmployeeCategoryName = input.EmployeeCategoryName;
            rateInfo.FixedAmount = input.FixedAmount;
            rateInfo.MaximumAmount = input.MaximumAmount;
            rateInfo.PercentageBasic = input.PercentageBasic;
            rateInfo.Prorate = input.Prorate;

            ratesList.Add(rateInfo);

            overtimeTypeData.Rates = ratesList;

            await _repositoryOverTimeType.UpdateAsync(overtimeTypeData);
        }


        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryOverTimeType.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryOverTimeType.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryOverTimeType.DeleteAsync(id);

        }

        public async Task DeleteOvertimeRate(OvertimeRateInput input)
        {
            var overtimeTypeData = await _repositoryOverTimeType.GetAsync(input.OvertimeTypeId);
            var ratesList = overtimeTypeData.Rates;
            var rateInfo = ratesList.FirstOrDefault(x => x.Id == input.Id);
            ratesList.Remove(rateInfo);

            overtimeTypeData.Rates = ratesList;

            await _repositoryOverTimeType.UpdateAsync(overtimeTypeData);

        }
    }
}
