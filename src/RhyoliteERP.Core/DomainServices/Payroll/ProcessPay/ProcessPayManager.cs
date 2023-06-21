﻿using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RhyoliteERP.DomainServices.Payroll.ProcessPay.Dto;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.Payroll.ProcessPay
{
    public class ProcessPayManager: Abp.Domain.Services.DomainService, IProcessPayManager
    {

        private readonly IRepository<TaxTable, Guid> _repositoryTaxTable;
        private readonly IRepository<EmployeeBioData, Guid> _repositoryEmployeeBioData;
        private readonly IRepository<EmployeeSalaryInfo, Guid> _repositorySalaryInfo;
        private readonly IRepository<EmployeeSnit, Guid> _repositoryEmployeeSnit;
        private readonly IRepository<EmployeeLoan, Guid> _repositoryEmployeeLoan;
        private readonly IRepository<EmployeeLoanRepaymentSchedule, Guid> _repositoryEmployeeLoanRepaymentSchedule;
        private readonly IRepository<EmployeeSalaryAdvance, Guid> _repositoryEmployeeSalaryAdvance;
        private readonly IRepository<BonusAndOnetimeAllowance, Guid> _repositoryBonusAndOnetimeAllowance;
        private readonly IRepository<EmployeeOnetimeDeduction, Guid> _repositoryEmployeeOnetimeDeduction;
        private readonly IRepository<EmployeeAllowance, Guid> _repositoryEmployeeAllowance;
        private readonly IRepository<EmployeeDeduction, Guid> _repositoryEmployeeDeduction;
        private readonly IRepository<EmployeeBenefitInKind, Guid> _repositoryEmployeeBenefitInKind;
        private readonly IRepository<EmployeeRelief, Guid> _repositoryEmployeeRelief;
        private readonly IRepository<LoanType, Guid> _repositoryLoanType;
        private readonly IRepository<OvertimeTimeSheet, Guid> _repositoryOvertimeTimeSheet;
        private readonly IRepository<EmployeeDaysWorked, Guid> _repositoryEmployeeDaysWorked;
        private readonly IRepository<TaxRelief, Guid> _repositoryTaxRelief;
        private readonly IRepository<EmployeeCategory, Guid> _repositoryEmployeeCategory;
        private readonly IRepository<AllowanceType, Guid> _repositoryAllowanceType;
        private readonly IRepository<PayCalendar, Guid> _repositoryPayCalendar;
        //
        private readonly IRepository<MonthlyAllowance, Guid> _repositoryMonthlyAllowance;
        private readonly IRepository<MonthlyDeduction, Guid> _repositoryMonthlyDeduction;
        private readonly IRepository<MonthlyBenefitsInKind, Guid> _repositoryMonthlyBenefitsInKind;
        private readonly IRepository<MonthlyRelief, Guid> _repositoryMonthlyRelief;
        private readonly IRepository<MonthlySalaryAdvance, Guid> _repositoryMonthlySalaryAdvance;
        private readonly IRepository<MonthlyBonus, Guid> _repositoryMonthlyBonus;
        private readonly IRepository<MonthlyCumulativeDeduction, Guid> _repositoryMonthlyCumulativeDeduction;
        private readonly IRepository<MonthlySsnitDeduction, Guid> _repositoryMonthlySsnitDeduction;
        private readonly IRepository<MonthlyOnetimeDeduction, Guid> _repositoryMonthlyOnetimeDeduction;
        private readonly IRepository<MonthlyPfDeduction, Guid> _repositoryMonthlyPfDeduction;
        private readonly IRepository<MonthlyLoanDeduction, Guid> _repositoryMonthlyLoanDeduction;
        private readonly IRepository<MonthlyIrsTax, Guid> _repositoryMonthlyIrsTax;
        private readonly IRepository<MonthlySecPfDeduction, Guid> _repositoryMonthlySecPfDeduction;
        private readonly IRepository<Paymaster, Guid> _repositoryPaymaster;
        private readonly IRepository<MonthlyOvertime, Guid> _repositoryMonthlyOvertime;
        private readonly IRepository<InitializePayMonth, Guid> _repositoryInitializePayMonth;
        private readonly IRepository<DeductionType, Guid> _repositoryDeductionType;
        private readonly IRepository<Currency, Guid> _repositoryCurrency;
        private readonly IRepository<BikType, Guid> _repositoryBikType;
        private readonly IRepository<OvertimeType, Guid> _repositoryOverTimeType;

        //history
        private readonly IRepository<MonthlyCumulativeDeductionHist, Guid> _repositoryMonthlyCumulativeDeductionHist;
        private readonly IRepository<MonthlySsnitDeductionHist, Guid> _repositoryMonthlySsnitDeductionHist;
        private readonly IRepository<MonthlyOnetimeDeductionHist, Guid> _repositoryMonthlyOnetimeDeductionHist;
        private readonly IRepository<MonthlyPfDeductionHist, Guid> _repositoryMonthlyPfDeductionHist;
        private readonly IRepository<MonthlyLoanDeductionHist, Guid> _repositoryMonthlyLoanDeductionHist;
        private readonly IRepository<MonthlyIrsTaxHist, Guid> _repositoryMonthlyIrsTaxHist;
        private readonly IRepository<MonthlySalaryAdvanceHist, Guid> _repositoryMonthlySalaryAdvanceHist;
        private readonly IRepository<MonthlySecPfDeductionHist, Guid> _repositoryMonthlySecPfDeductionHist;
        private readonly IRepository<MonthlyOvertimeHistory, Guid> _repositoryMonthlyOvertimeHist;
        private readonly IRepository<MonthlyAllowanceHist, Guid> _repositoryMonthlyAllowanceHist;
        private readonly IRepository<MonthlyDeductionHist, Guid> _repositoryMonthlyDeductionHist;
        private readonly IRepository<MonthlyBenefitsInKindHist, Guid> _repositoryMonthlyBenefitsInKindHist;
        private readonly IRepository<PaymasterHist, Guid> _repositoryPaymasterHist;
        private readonly IRepository<MonthlyReliefHist, Guid> _repositoryMonthlyReliefHist;

        // =>



        public ProcessPayManager(IRepository<TaxTable, Guid> repositoryTaxTable, IRepository<EmployeeBioData, Guid> repositoryEmployeeBioData, IRepository<EmployeeSalaryInfo, Guid> repositorySalaryInfo, IRepository<EmployeeSnit, Guid> repositoryEmployeeSnit, IRepository<EmployeeLoan, Guid> repositoryEmployeeLoan, IRepository<EmployeeLoanRepaymentSchedule, Guid> repositoryEmployeeLoanRepaymentSchedule, IRepository<EmployeeSalaryAdvance, Guid> repositoryEmployeeSalaryAdvance, IRepository<BonusAndOnetimeAllowance, Guid> repositoryBonusAndOnetimeAllowance, IRepository<EmployeeOnetimeDeduction, Guid> repositoryEmployeeOnetimeDeduction, IRepository<EmployeeAllowance, Guid> repositoryEmployeeAllowance, IRepository<EmployeeDeduction, Guid> repositoryEmployeeDeduction, IRepository<EmployeeBenefitInKind, Guid> repositoryEmployeeBenefitInKind, IRepository<EmployeeRelief, Guid> repositoryEmployeeRelief, IRepository<LoanType, Guid> repositoryLoanType, IRepository<OvertimeTimeSheet, Guid> repositoryOvertimeTimeSheet, IRepository<EmployeeDaysWorked, Guid> repositoryEmployeeDaysWorked, IRepository<TaxRelief, Guid> repositoryTaxRelief, IRepository<EmployeeCategory, Guid> repositoryEmployeeCategory, IRepository<AllowanceType, Guid> repositoryAllowanceType, IRepository<MonthlyAllowance, Guid> repositoryMonthlyAllowance, IRepository<MonthlyDeduction, Guid> repositoryMonthlyDeduction, IRepository<MonthlyBenefitsInKind, Guid> repositoryMonthlyBenefitsInKind, IRepository<MonthlyRelief, Guid> repositoryMonthlyRelief, IRepository<MonthlySalaryAdvance, Guid> repositoryMonthlySalaryAdvance, IRepository<MonthlyBonus, Guid> repositoryMonthlyBonus, IRepository<MonthlyCumulativeDeduction, Guid> repositoryMonthlyCumulativeDeduction, IRepository<MonthlySsnitDeduction, Guid> repositoryMonthlySsnitDeduction, IRepository<MonthlyOnetimeDeduction, Guid> repositoryMonthlyOnetimeDeduction, IRepository<MonthlyPfDeduction, Guid> repositoryMonthlyPfDeduction, IRepository<MonthlyLoanDeduction, Guid> repositoryMonthlyLoanDeduction, IRepository<MonthlyIrsTax, Guid> repositoryMonthlyIrsTax, IRepository<MonthlySecPfDeduction, Guid> repositoryMonthlySecPfDeduction, IRepository<Paymaster, Guid> repositoryPaymaster, IRepository<MonthlyOvertime, Guid> repositoryMonthlyOvertime, IRepository<InitializePayMonth, Guid> repositoryInitializePayMonth, IRepository<DeductionType, Guid> repositoryDeductionType, IRepository<Currency, Guid> repositoryCurrency, IRepository<BikType, Guid> repositoryBikType, IRepository<PayCalendar, Guid> repositoryPayCalendar, IRepository<OvertimeType, Guid> repositoryOverTimeType, IRepository<PaymasterHist, Guid> repositoryPaymasterHist, IRepository<MonthlyReliefHist, Guid> repositoryMonthlyReliefHist, IRepository<MonthlyAllowanceHist, Guid> repositoryMonthlyAllowanceHist, IRepository<MonthlyDeductionHist, Guid> repositoryMonthlyDeductionHist, IRepository<MonthlyBenefitsInKindHist, Guid> repositoryMonthlyBenefitsInKindHist, IRepository<MonthlyIrsTaxHist, Guid> repositoryMonthlyIrsTaxHist, IRepository<MonthlySsnitDeductionHist, Guid> repositoryMonthlySsnitDeductionHist, IRepository<MonthlyCumulativeDeductionHist, Guid> repositoryMonthlyCumulativeDeductionHist, IRepository<MonthlyPfDeductionHist, Guid> repositoryMonthlyPfDeductionHist, IRepository<MonthlyLoanDeductionHist, Guid> repositoryMonthlyLoanDeductionHist, IRepository<MonthlySecPfDeductionHist, Guid> repositoryMonthlySecPfDeductionHist, IRepository<MonthlySalaryAdvanceHist, Guid> repositoryMonthlySalaryAdvanceHist, IRepository<MonthlyOvertimeHistory, Guid> repositoryMonthlyOvertimeHist, IRepository<MonthlyOnetimeDeductionHist, Guid> repositoryMonthlyOnetimeDeductionHist)
        {
            _repositoryTaxTable = repositoryTaxTable;
            _repositoryEmployeeBioData = repositoryEmployeeBioData;
            _repositorySalaryInfo = repositorySalaryInfo;
            _repositoryEmployeeSnit = repositoryEmployeeSnit;
            _repositoryEmployeeLoan = repositoryEmployeeLoan;
            _repositoryEmployeeLoanRepaymentSchedule = repositoryEmployeeLoanRepaymentSchedule;
            _repositoryEmployeeSalaryAdvance = repositoryEmployeeSalaryAdvance;
            _repositoryBonusAndOnetimeAllowance = repositoryBonusAndOnetimeAllowance;
            _repositoryEmployeeOnetimeDeduction = repositoryEmployeeOnetimeDeduction;
            _repositoryEmployeeAllowance = repositoryEmployeeAllowance;
            _repositoryEmployeeDeduction = repositoryEmployeeDeduction;
            _repositoryEmployeeBenefitInKind = repositoryEmployeeBenefitInKind;
            _repositoryEmployeeRelief = repositoryEmployeeRelief;
            _repositoryLoanType = repositoryLoanType;
            _repositoryOvertimeTimeSheet = repositoryOvertimeTimeSheet;
            _repositoryEmployeeDaysWorked = repositoryEmployeeDaysWorked;
            _repositoryTaxRelief = repositoryTaxRelief;
            _repositoryEmployeeCategory = repositoryEmployeeCategory;
            _repositoryAllowanceType = repositoryAllowanceType;
            _repositoryMonthlyAllowance = repositoryMonthlyAllowance;
            _repositoryMonthlyDeduction = repositoryMonthlyDeduction;
            _repositoryMonthlyBenefitsInKind = repositoryMonthlyBenefitsInKind;
            _repositoryMonthlyRelief = repositoryMonthlyRelief;
            _repositoryMonthlySalaryAdvance = repositoryMonthlySalaryAdvance;
            _repositoryMonthlyBonus = repositoryMonthlyBonus;
            _repositoryMonthlyCumulativeDeduction = repositoryMonthlyCumulativeDeduction;
            _repositoryMonthlySsnitDeduction = repositoryMonthlySsnitDeduction;
            _repositoryMonthlyOnetimeDeduction = repositoryMonthlyOnetimeDeduction;
            _repositoryMonthlyPfDeduction = repositoryMonthlyPfDeduction;
            _repositoryMonthlyLoanDeduction = repositoryMonthlyLoanDeduction;
            _repositoryMonthlyIrsTax = repositoryMonthlyIrsTax;
            _repositoryMonthlySecPfDeduction = repositoryMonthlySecPfDeduction;
            _repositoryPaymaster = repositoryPaymaster;
            _repositoryMonthlyOvertime = repositoryMonthlyOvertime;
            _repositoryInitializePayMonth = repositoryInitializePayMonth;
            _repositoryDeductionType = repositoryDeductionType;
            _repositoryCurrency = repositoryCurrency;
            _repositoryBikType = repositoryBikType;
            _repositoryPayCalendar = repositoryPayCalendar;
            _repositoryOverTimeType = repositoryOverTimeType;
            _repositoryPaymasterHist = repositoryPaymasterHist;
            _repositoryMonthlyReliefHist = repositoryMonthlyReliefHist;
            _repositoryMonthlyAllowanceHist = repositoryMonthlyAllowanceHist;
            _repositoryMonthlyDeductionHist = repositoryMonthlyDeductionHist;
            _repositoryMonthlyBenefitsInKindHist = repositoryMonthlyBenefitsInKindHist;
            _repositoryMonthlyIrsTaxHist = repositoryMonthlyIrsTaxHist;
            _repositoryMonthlySsnitDeductionHist = repositoryMonthlySsnitDeductionHist;
            _repositoryMonthlyCumulativeDeductionHist = repositoryMonthlyCumulativeDeductionHist;
            _repositoryMonthlyPfDeductionHist = repositoryMonthlyPfDeductionHist;
            _repositoryMonthlyLoanDeductionHist = repositoryMonthlyLoanDeductionHist;
            _repositoryMonthlySecPfDeductionHist = repositoryMonthlySecPfDeductionHist;
            _repositoryMonthlySalaryAdvanceHist = repositoryMonthlySalaryAdvanceHist;
            _repositoryMonthlyOvertimeHist = repositoryMonthlyOvertimeHist;
            _repositoryMonthlyOnetimeDeductionHist = repositoryMonthlyOnetimeDeductionHist;
        }


        public async Task<object> ProcessPayroll(Guid defaultStatusId)
        {

            //delete all...
            await InitPayroll();

            var employeeList = await _repositoryEmployeeBioData.GetAllListAsync(x=>x.StatusId == defaultStatusId);

            var salaryInfoList = await _repositorySalaryInfo.GetAllListAsync();

            var employeeReliefList = await _repositoryEmployeeRelief.GetAllListAsync();

            var ssnitContributionList = await _repositoryEmployeeSnit.GetAllListAsync();

            var employeeBenefitInKindList = await _repositoryEmployeeBenefitInKind.GetAllListAsync();

            var employeeDeductionList = await _repositoryEmployeeDeduction.GetAllListAsync();

            var employeeAllowanceList = await _repositoryEmployeeAllowance.GetAllListAsync();

            var onetimeDeductionList = await _repositoryEmployeeOnetimeDeduction.GetAllListAsync(); 

            var salaryAdvanceList = await _repositoryEmployeeSalaryAdvance.GetAllListAsync();

            var overtimeTimeSheetList = await _repositoryOvertimeTimeSheet.GetAllListAsync();    

            var employeeLoanList = await _repositoryEmployeeLoan.GetAllListAsync(x=>x.IsApproved);

            var currentPayMonth = await _repositoryInitializePayMonth.GetAll().FirstOrDefaultAsync();


            if (currentPayMonth == null)
            {
                return new { code = 400, message = $"Current pay month setup required.\nInitialize pay month and reprocess payroll." };
            }

            var bonusAndOnetimeAllowanceList = await _repositoryBonusAndOnetimeAllowance.GetAllListAsync();


            foreach (var employeeInfo in employeeList)
            {
                decimal basicSalary = 0;
                decimal currentAmount = 0;

                var employeeName = string.IsNullOrEmpty(employeeInfo.OtherName) ? $"{employeeInfo.LastName} {employeeInfo.FirstName}" : $"{employeeInfo.LastName} {employeeInfo.FirstName} {employeeInfo.OtherName}";

                var employeeSalaryInfo = salaryInfoList.FirstOrDefault(a => a.EmployeeId == employeeInfo.Id);
                var employeeReliefs = employeeReliefList.Where(a => a.EmployeeId == employeeInfo.Id);
                var employeeSsnitContributions = ssnitContributionList.FirstOrDefault(a => a.EmployeeId == employeeInfo.Id);
                var employeeBenefitsInKind = employeeBenefitInKindList.Where(a => a.EmployeeId == employeeInfo.Id);
                var employeeDeductions = employeeDeductionList.Where(a => a.EmployeeId == employeeInfo.Id);
                var employeeAllowances = employeeAllowanceList.Where(a => a.EmployeeId == employeeInfo.Id);
                var employeeOnetimeDeductions = onetimeDeductionList.Where(a => a.EmployeeId == employeeInfo.Id);

                var employeeSalaryAdvances = salaryAdvanceList.Where(a => a.EmployeeId == employeeInfo.Id);
                var employeeOvertimes = overtimeTimeSheetList.Where(a => a.EmployeeId == employeeInfo.Id);
                var employeeLoans = employeeLoanList.Where(a => a.EmployeeId == employeeInfo.Id);

                var payCalendarInfo = await _repositoryPayCalendar.FirstOrDefaultAsync(x => x.Year == currentPayMonth.Year);

                var payCalendarDetails = payCalendarInfo.PayCalendarDetails;

                var daysInPayCalendar = payCalendarDetails.FirstOrDefault(a => a.Month == currentPayMonth.Month && a.Year == currentPayMonth.Year);

                if (daysInPayCalendar == null)
                {
                    return new { code = 400, message = $"Pay calendar month and days not set up for {currentPayMonth.Year}" };
                }

                var employeeBonusAndOnetimeAllowances = bonusAndOnetimeAllowanceList.Where(a => a.EmployeeId == employeeInfo.Id);

                var actualDaysWorked = await _repositoryEmployeeDaysWorked.FirstOrDefaultAsync(a => a.EmployeeId == employeeInfo.Id && a.Month == currentPayMonth.Month && a.Year == currentPayMonth.Year);

                if (employeeSalaryInfo != null)
                {

                    decimal employeeDaysWorked = 0;
                    decimal employeeHoursWorked = 0;

                    if (actualDaysWorked != null)
                    {
                        //means employee worked the full month...
                        employeeDaysWorked = actualDaysWorked.Days + (actualDaysWorked.Hours / employeeSalaryInfo.DailyHours) + (actualDaysWorked.Minutes / (employeeSalaryInfo.DailyHours * 60));
                        employeeHoursWorked = actualDaysWorked.Hours;
                        //ADJUST BASIC TO REFLECT DAYS WORKED 
                        if (daysInPayCalendar.Days > 0 && employeeDaysWorked > 0)
                        {
                            //check if the division is greater than 1...
                            if (employeeDaysWorked / daysInPayCalendar.Days > 1)
                            {
                                basicSalary = employeeSalaryInfo.MonthlySalary;
                            }
                            else
                            {
                                basicSalary = employeeDaysWorked / daysInPayCalendar.Days * employeeSalaryInfo.MonthlySalary;
                                currentAmount = basicSalary;
                            }

                        }

                    }
                    else if (actualDaysWorked == null)
                    {
                        employeeDaysWorked = daysInPayCalendar.Days;
                        basicSalary = employeeSalaryInfo.MonthlySalary;
                        currentAmount = basicSalary;

                    }


                    //process allowances
                    decimal taxableAllowance = 0;
                    decimal nonTaxableAllowance = 0;


                    List<EmployeeAllowanceDto> employeeTaxableAllowanceList = new List<EmployeeAllowanceDto>();

                    List<decimal> employeeNonTaxableAllowanceList = new List<decimal>();

                    var allowanceTypeList = await _repositoryAllowanceType.GetAllListAsync();

                    List<AllowanceRate> allowanceRates = new List<AllowanceRate>();

                    allowanceTypeList.ForEach((a) =>
                    {
                        allowanceRates.AddRange(a.AllowanceRates);
                    });


                    foreach (EmployeeAllowance employeeAllowance in employeeAllowances)
                    {
                        var allowanceRate = allowanceRates.FirstOrDefault(a => a.AllowanceTypeId == employeeAllowance.AllowanceTypeId && a.EmployeeCategoryId == employeeInfo.CategoryId);

                        if (employeeAllowance.Taxable && employeeAllowance.IsMonthly)
                        {
                            //obtain allowance rate...
                            if (allowanceRate != null)
                            {
                                if (allowanceRate.FixedAmount && allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = employeeAllowance.Amount * (employeeAllowance.AllowanceDays / daysInPayCalendar.Days);

                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    var proratedEmployeeTaxableAllowance = new EmployeeAllowanceDto
                                    {
                                        MaximumAmount = allowanceRate.MaximumAmount,
                                        AllowanceAmount = allowanceAmount
                                    };

                                    employeeTaxableAllowanceList.Add(proratedEmployeeTaxableAllowance);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount,
                                        EmployeeName = string.IsNullOrEmpty(employeeInfo.OtherName) ? $"{employeeInfo.LastName} {employeeInfo.FirstName}" : $"{employeeInfo.LastName} {employeeInfo.FirstName} {employeeInfo.OtherName}",
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId
                                        
                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (allowanceRate.FixedAmount && !allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = employeeAllowance.Amount;

                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    var nonProratedEmployeeTaxableAllowance = new EmployeeAllowanceDto
                                    {
                                        MaximumAmount = allowanceRate.MaximumAmount,
                                        AllowanceAmount = allowanceAmount
                                    };

                                    employeeTaxableAllowanceList.Add(nonProratedEmployeeTaxableAllowance);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount,
                                        EmployeeName = string.IsNullOrEmpty(employeeInfo.OtherName) ? $"{employeeInfo.LastName} {employeeInfo.FirstName}" : $"{employeeInfo.LastName} {employeeInfo.FirstName} {employeeInfo.OtherName}",
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId

                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);

                                }
                                else if (!allowanceRate.FixedAmount && allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = ((allowanceRate.PercentageBasic * basicSalary) / 100) * (employeeAllowance.AllowanceDays / daysInPayCalendar.Days);
                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }
                                    var nonFixedProratedEmployeeTaxableAllowance = new EmployeeAllowanceDto
                                    {
                                        MaximumAmount = allowanceRate.MaximumAmount,
                                        AllowanceAmount = allowanceAmount
                                    };

                                    employeeTaxableAllowanceList.Add(nonFixedProratedEmployeeTaxableAllowance);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName =  employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId

                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (!allowanceRate.FixedAmount && !allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = (allowanceRate.PercentageBasic * employeeSalaryInfo.MonthlySalary) / 100;
                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    var nonFixedNonProratedEmployeeTaxableAllowance = new EmployeeAllowanceDto
                                    {
                                        MaximumAmount = allowanceRate.MaximumAmount,
                                        AllowanceAmount = allowanceAmount
                                    };

                                    employeeTaxableAllowanceList.Add(nonFixedNonProratedEmployeeTaxableAllowance);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName =  employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                            }

                        }
                        else if (!employeeAllowance.Taxable && employeeAllowance.IsMonthly)
                        {
                            // non taxable allowance
                            if (allowanceRate != null)
                            {
                                if (allowanceRate.FixedAmount && allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = employeeAllowance.Amount * ((decimal)employeeAllowance.AllowanceDays / (decimal)daysInPayCalendar.Days);

                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (allowanceRate.FixedAmount && !allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = employeeAllowance.Amount;

                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId

                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (!allowanceRate.FixedAmount && allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = ((allowanceRate.PercentageBasic * basicSalary) / 100) * employeeAllowance.AllowanceDays;
                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (!allowanceRate.FixedAmount && !allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = (allowanceRate.PercentageBasic * basicSalary) / 100;
                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                            }


                        }
                        else if (employeeAllowance.Taxable && !employeeAllowance.IsMonthly)
                        {
                            // non taxable allowance
                            if (allowanceRate != null)
                            {
                                if (allowanceRate.FixedAmount && allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = employeeAllowance.Amount;

                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount * employeeAllowance.AllowanceDays);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount * employeeAllowance.AllowanceDays,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (allowanceRate.FixedAmount && !allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = employeeAllowance.Amount;

                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount * daysInPayCalendar.Days);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount * employeeAllowance.AllowanceDays,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (!allowanceRate.FixedAmount && allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = ((allowanceRate.PercentageBasic * basicSalary) / 100);
                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount * employeeAllowance.AllowanceDays);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName =  employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount * employeeAllowance.AllowanceDays,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (!allowanceRate.FixedAmount && !allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = (allowanceRate.PercentageBasic * basicSalary) / 100;
                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount * daysInPayCalendar.Days);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount * employeeAllowance.AllowanceDays,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                            }

                        }
                        else if (!employeeAllowance.Taxable && !employeeAllowance.IsMonthly)
                        {
                            // non taxable allowance
                            if (allowanceRate != null)
                            {
                                if (allowanceRate.FixedAmount && allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = employeeAllowance.Amount;

                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount * employeeAllowance.AllowanceDays);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount * employeeAllowance.AllowanceDays,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (allowanceRate.FixedAmount && !allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = employeeAllowance.Amount;

                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount * daysInPayCalendar.Days);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount * daysInPayCalendar.Days,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (!allowanceRate.FixedAmount && allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = ((allowanceRate.PercentageBasic * basicSalary) / 100);
                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount * employeeAllowance.AllowanceDays);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount * employeeAllowance.AllowanceDays,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                                else if (!allowanceRate.FixedAmount && !allowanceRate.Prorate)
                                {
                                    decimal allowanceAmount = (allowanceRate.PercentageBasic * basicSalary) / 100;
                                    if (allowanceRate.MaximumAmount != 0 && allowanceAmount > allowanceRate.MaximumAmount)
                                    {
                                        allowanceAmount = allowanceRate.MaximumAmount;
                                    }

                                    employeeNonTaxableAllowanceList.Add(allowanceAmount * daysInPayCalendar.Days);

                                    var monthlyallowance = new MonthlyAllowance
                                    {
                                        EmployeeId = employeeAllowance.EmployeeId,
                                        EmployeeName = employeeName,
                                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                        EmployeeCategory = employeeInfo.CategoryName,
                                        EmployeeDepartment = employeeInfo.DepartmentName,
                                        AllowanceTypeId = employeeAllowance.AllowanceTypeId,
                                        AllowanceTypeName = employeeAllowance.AllowanceTypeName,
                                        Amount = allowanceAmount * daysInPayCalendar.Days,
                                        Taxable = employeeAllowance.Taxable,
                                        SSF = employeeAllowance.SSF,
                                        ProvidentFund = employeeAllowance.ProvidentFund,
                                        IsMonthly = employeeAllowance.IsMonthly,
                                        AllowanceDays = employeeAllowance.AllowanceDays,
                                        Month = currentPayMonth.Month,
                                        Year = currentPayMonth.Year,
                                        CurrencyId = employeeSalaryInfo.CurrencyId


                                    };

                                    await _repositoryMonthlyAllowance.InsertAsync(monthlyallowance);
                                }
                            }

                        }

                    }

                    taxableAllowance = employeeTaxableAllowanceList.Sum(a => a.AllowanceAmount);

                    nonTaxableAllowance = employeeNonTaxableAllowanceList.Sum();


                    //process benefits in kind
                    List<decimal> employeeBenefitInKindAmountList = new List<decimal>();

                    List<BikRate> bikRates = new List<BikRate>();

                    var bikTypes = await _repositoryBikType.GetAllListAsync();

                    bikTypes.ForEach((a) =>
                    {
                        bikRates.AddRange(a.BikRates);
                    });


                    foreach (EmployeeBenefitInKind employeeBenefitInKind in employeeBenefitsInKind)
                    {
                        var benefitsInKindRate = bikRates.FirstOrDefault(a => a.EmployeeCategoryId == employeeInfo.CategoryId && a.BikTypeId == employeeBenefitInKind.BenefitInKindTypeId);

                        if (benefitsInKindRate != null)
                        {
                            if (benefitsInKindRate.FixedAmount)
                            {
                                decimal bikAmt = employeeBenefitInKind.Amount;

                                if (benefitsInKindRate.MaximumAmount != 0 && bikAmt > benefitsInKindRate.MaximumAmount)
                                {
                                    bikAmt = benefitsInKindRate.MaximumAmount;
                                }

                                employeeBenefitInKindAmountList.Add(bikAmt);

                                var monthlybik = new MonthlyBenefitsInKind
                                {
                                    EmployeeId = employeeBenefitInKind.EmployeeId,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    BenefitInKindTypeId = employeeBenefitInKind.BenefitInKindTypeId,
                                    BenefitInKindTypeName= employeeBenefitInKind.BenefitInKindTypeName,
                                    Amount = bikAmt,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year
                                };

                                await _repositoryMonthlyBenefitsInKind.InsertAsync(monthlybik);
                            }
                            else
                            {
                                decimal bikAmt = basicSalary * (benefitsInKindRate.PercentageBasic / 100);

                                if (benefitsInKindRate.MaximumAmount != 0 && bikAmt > benefitsInKindRate.MaximumAmount)
                                {
                                    bikAmt = benefitsInKindRate.MaximumAmount;
                                }

                                employeeBenefitInKindAmountList.Add(bikAmt);

                                var monthlybik = new MonthlyBenefitsInKind
                                {
                                    EmployeeId = employeeBenefitInKind.EmployeeId,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    BenefitInKindTypeId = employeeBenefitInKind.BenefitInKindTypeId,
                                    BenefitInKindTypeName = employeeBenefitInKind.BenefitInKindTypeName,
                                    Amount = bikAmt,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year
                                };

                                await _repositoryMonthlyBenefitsInKind.InsertAsync(monthlybik);
                            }
                        }

                    }

                    //bonus and onetime allowance...
                    List<decimal> employeebonusList = new List<decimal>();

                    var bonusAndOnetimeAllowances = await _repositoryBonusAndOnetimeAllowance.GetAllListAsync(a => a.EmployeeId == employeeInfo.Id);

                    foreach (BonusAndOnetimeAllowance bonusAndOnetimeAllowance in employeeBonusAndOnetimeAllowances)
                    {
                        if (bonusAndOnetimeAllowance.IsFixedAmount)
                        {
                            decimal bonus = bonusAndOnetimeAllowance.Amount;

                            var monthlyBonus = new MonthlyBonus
                            {
                                Month = currentPayMonth.Month,
                                Year = currentPayMonth.Year,
                                Amount = bonus,
                                EmployeeId = employeeInfo.Id,
                                EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                EmployeeName = employeeName,
                                EmployeeCategory = employeeInfo.CategoryName,
                                EmployeeDepartment = employeeInfo.DepartmentName,
                                

                            };
                            await _repositoryMonthlyBonus.InsertAsync(monthlyBonus);
                            employeebonusList.Add(bonus);
                        }
                        else
                        {
                            decimal bonus = basicSalary * (bonusAndOnetimeAllowance.Percentage / 100);
                            var monthlyBonus = new MonthlyBonus
                            {
                                Month = currentPayMonth.Month,
                                Year = currentPayMonth.Year,
                                Amount = bonus,
                                EmployeeId = employeeInfo.Id,
                                EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                EmployeeName = employeeName,
                                EmployeeCategory = employeeInfo.CategoryName,
                                EmployeeDepartment = employeeInfo.DepartmentName,
                            };
                            await _repositoryMonthlyBonus.InsertAsync(monthlyBonus);

                            employeebonusList.Add(bonus);

                        }

                    }

                    //deductions...

                    List<EmployeeAllowanceDto> employeeDeductionsList = new List<EmployeeAllowanceDto>();

                    List<DeductionRate> deductionRates = new List<DeductionRate>();

                    var deductionTypes = await _repositoryDeductionType.GetAllListAsync();

                    deductionTypes.ForEach((a) =>
                    {
                        deductionRates.AddRange(a.Rates);
                    });

                    foreach (EmployeeDeduction employeeDeduction in employeeDeductions)
                    {
                        //obtain deduction type name...
                        var deductionRate = deductionRates.FirstOrDefault(a => a.DeductionTypeId == employeeDeduction.DeductionTypeId && a.EmployeeCategoryId == employeeInfo.CategoryId);

                        if (deductionRate != null)
                        {
                            if (deductionRate.FixedAmount && deductionRate.Prorate)
                            {
                                decimal deductionAmt = employeeDeduction.Amount * (employeeDaysWorked / daysInPayCalendar.Days);
                                if (deductionRate.MaximumAmount != 0 && deductionAmt > deductionRate.MaximumAmount)
                                {
                                    deductionAmt = deductionRate.MaximumAmount;
                                }
                                var fixedProratedDeduction = new EmployeeAllowanceDto
                                {
                                    MaximumAmount = deductionRate.MaximumAmount,
                                    AllowanceAmount = deductionAmt,
                                };
                                employeeDeductionsList.Add(fixedProratedDeduction);

                                var monthlyDeduction = new MonthlyDeduction
                                {
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    DeductionTypeId = employeeDeduction.DeductionTypeId,
                                    DeductionTypeName = deductionRate.DeductionTypeName,
                                    Amount = deductionAmt,
                                    EmployerAmount = employeeDeduction.EmployerAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year
                                };

                                await _repositoryMonthlyDeduction.InsertAsync(monthlyDeduction);

                                var monthlyCumulativeDeduction = new MonthlyCumulativeDeduction
                                {
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Description = deductionRate.DeductionTypeName,
                                    Amount = deductionAmt,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                };

                                await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyCumulativeDeduction);

                            }
                            else if (deductionRate.FixedAmount && !deductionRate.Prorate)
                            {
                                decimal deductionAmt = employeeDeduction.Amount;
                                if (deductionRate.MaximumAmount != 0 && deductionAmt > deductionRate.MaximumAmount)
                                {
                                    deductionAmt = deductionRate.MaximumAmount;
                                }
                                var fixedNonProratedDeduction = new EmployeeAllowanceDto
                                {
                                    MaximumAmount = deductionRate.MaximumAmount,
                                    AllowanceAmount = deductionAmt,
                                };

                                employeeDeductionsList.Add(fixedNonProratedDeduction);
                                var monthlyDeduction = new MonthlyDeduction
                                {
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    DeductionTypeId = employeeDeduction.DeductionTypeId,
                                    DeductionTypeName = deductionRate.DeductionTypeName,
                                    Amount = deductionAmt,
                                    EmployerAmount = employeeDeduction.EmployerAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year
                                };

                                await _repositoryMonthlyDeduction.InsertAsync(monthlyDeduction);

                                var monthlyCumulativeDeduction = new MonthlyCumulativeDeduction
                                {
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Description = deductionRate.DeductionTypeName,
                                    Amount = deductionAmt,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                };

                                await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyCumulativeDeduction);

                            }
                            else if (!deductionRate.FixedAmount && deductionRate.Prorate)
                            {
                                decimal deductionAmt = (deductionRate.PercentageBasic * basicSalary) / 100;
                                if (deductionRate.MaximumAmount != 0 && deductionAmt > deductionRate.MaximumAmount)
                                {
                                    deductionAmt = deductionRate.MaximumAmount;
                                }
                                var nonFixedProratedDeduction = new EmployeeAllowanceDto
                                {
                                    MaximumAmount = deductionRate.MaximumAmount,
                                    AllowanceAmount = deductionAmt,
                                };

                                employeeDeductionsList.Add(nonFixedProratedDeduction);

                                var monthlyDeduction = new MonthlyDeduction
                                {
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    DeductionTypeId = employeeDeduction.DeductionTypeId,
                                    DeductionTypeName = deductionRate.DeductionTypeName,
                                    Amount = deductionAmt,
                                    EmployerAmount = employeeDeduction.EmployerAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year
                                };

                                await _repositoryMonthlyDeduction.InsertAsync(monthlyDeduction);

                                var monthlyCumulativeDeduction = new MonthlyCumulativeDeduction
                                {
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Description = employeeDeduction.DeductionTypeName,
                                    Amount = deductionAmt,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                };

                                await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyCumulativeDeduction);

                            }
                            else if (!deductionRate.FixedAmount && !deductionRate.Prorate)
                            {
                                decimal deductionAmt = (deductionRate.PercentageBasic * employeeSalaryInfo.MonthlySalary) / 100;

                                if (deductionRate.MaximumAmount != 0 && deductionAmt > deductionRate.MaximumAmount)
                                {
                                    deductionAmt = deductionRate.MaximumAmount;
                                }
                                var nonFixedNonProratedDeduction = new EmployeeAllowanceDto
                                {
                                    MaximumAmount = deductionRate.MaximumAmount,
                                    AllowanceAmount = deductionAmt,
                                };

                                employeeDeductionsList.Add(nonFixedNonProratedDeduction);

                                var monthlyDeduction = new MonthlyDeduction
                                {
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    DeductionTypeId = employeeDeduction.DeductionTypeId,
                                    DeductionTypeName = deductionRate.DeductionTypeName,
                                    Amount = deductionAmt,
                                    EmployerAmount = employeeDeduction.EmployerAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year
                                };

                                await _repositoryMonthlyDeduction.InsertAsync(monthlyDeduction);

                                var monthlyCumulativeDeduction = new MonthlyCumulativeDeduction
                                {
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Description = deductionRate.DeductionTypeName,
                                    Amount = deductionAmt,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                };

                                await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyCumulativeDeduction);

                            }
                        }

                    }

                    //salaryAdvance

                    List<decimal> salaryAdvanceAmountList = new List<decimal>();
                    foreach (EmployeeSalaryAdvance employeeSalaryAdvance in employeeSalaryAdvances)
                    {

                        var loanType = await _repositoryLoanType.FirstOrDefaultAsync(a => a.Id == employeeSalaryAdvance.LoanTypeId);

                        var monthlySalaryAdvance = new MonthlySalaryAdvance
                        {
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                            LoanDate = employeeSalaryAdvance.LoanDate,
                            LoanTypeId = employeeSalaryAdvance.LoanTypeId,
                            LoanTypeName = loanType.Name,
                            Amount = employeeSalaryAdvance.Amount,
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,

                        };

                        await _repositoryMonthlySalaryAdvance.InsertAsync(monthlySalaryAdvance);

                        salaryAdvanceAmountList.Add(employeeSalaryAdvance.Amount);

                        var monthlyCumulativeDeduction = new MonthlyCumulativeDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Salary Advance -" + loanType.Name,
                            Amount = employeeSalaryAdvance.Amount,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyCumulativeDeduction);

                    }


                    decimal totalOnetimeDeduction = 0;
                    foreach (EmployeeOnetimeDeduction employeeOnetimeDeduction in employeeOnetimeDeductions)
                    {

                        var deductionType = await _repositoryDeductionType.FirstOrDefaultAsync(a => a.Id == employeeOnetimeDeduction.DeductionTypeId);
                        //var deductionRate = deductionRates.FirstOrDefault(a => a.DeductionTypeId == employeeOnetimeDeduction.DeductionTypeId && a.EmployeeCategoryId == employeeInfo.CategoryId);

                        decimal otdAmount = 0;

                        if (employeeOnetimeDeduction.IsFixedAmount)
                        {

                            totalOnetimeDeduction += employeeOnetimeDeduction.Amount;
                            otdAmount = employeeOnetimeDeduction.Amount;

                            var monthlyOtd = new MonthlyOnetimeDeduction
                            {

                                EmployeeId = employeeInfo.Id,
                                EmployeeName = employeeName,
                                EmployeeCategory = employeeInfo.CategoryName,
                                EmployeeDepartment = employeeInfo.DepartmentName,
                                EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                Description = deductionType.Name,
                                Amount = otdAmount,
                                Month = currentPayMonth.Month,
                                Year = currentPayMonth.Year,

                            };
                            await _repositoryMonthlyOnetimeDeduction.InsertAsync(monthlyOtd);

                            //save to cumulative deduction...
                            var cumulativeDeduction5 = new MonthlyCumulativeDeduction
                            {
                                EmployeeId = employeeInfo.Id,
                                EmployeeName = employeeName,
                                EmployeeCategory = employeeInfo.CategoryName,
                                EmployeeDepartment = employeeInfo.DepartmentName,
                                EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                Description = deductionType.Name,
                                Amount = otdAmount,
                                Month = currentPayMonth.Month,
                                Year = currentPayMonth.Year,
                            };

                            await _repositoryMonthlyCumulativeDeduction.InsertAsync(cumulativeDeduction5);

                        }
                        else
                        {
                            totalOnetimeDeduction += employeeSalaryInfo.MonthlySalary * (employeeOnetimeDeduction.Percentage / 100);
                            otdAmount = employeeSalaryInfo.MonthlySalary * (employeeOnetimeDeduction.Percentage / 100);

                            var monthlyOtd = new MonthlyOnetimeDeduction
                            {

                                EmployeeId = employeeInfo.Id,
                                EmployeeName = employeeName,
                                EmployeeCategory = employeeInfo.CategoryName,
                                EmployeeDepartment = employeeInfo.DepartmentName,
                                EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                Description = deductionType.Name,
                                Amount = otdAmount,
                                Month = currentPayMonth.Month,
                                Year = currentPayMonth.Year,

                            };
                            await _repositoryMonthlyOnetimeDeduction.InsertAsync(monthlyOtd);

                            //save to cumulative deduction...
                            var cumulativeDeduction5 = new MonthlyCumulativeDeduction
                            {
                                EmployeeId = employeeInfo.Id,
                                EmployeeName = employeeName,
                                EmployeeCategory = employeeInfo.CategoryName,
                                EmployeeDepartment = employeeInfo.DepartmentName,
                                EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                Description = deductionType.Name,
                                Amount = otdAmount,
                                Month = currentPayMonth.Month,
                                Year = currentPayMonth.Year,
                            };

                            await _repositoryMonthlyCumulativeDeduction.InsertAsync(cumulativeDeduction5);
                        }


                    }


                    //=> process reliefs
                    List<decimal> taxReliefAmountList = new List<decimal>();

                    foreach (EmployeeRelief employeeRelief in employeeReliefs)
                    {
                        taxReliefAmountList.Add(employeeRelief.Amount);

                        var monthlyReliefs = new MonthlyRelief
                        {
                            EmployeeId = employeeRelief.EmployeeId,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            ReliefTypeId = employeeRelief.ReliefTypeId,
                            ReliefTypeName = employeeRelief.ReliefTypeName,
                            Amount = employeeRelief.Amount,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year
                        };

                        await _repositoryMonthlyRelief.InsertAsync(monthlyReliefs);

                    }


                    //=> process employee Loans...
                    List<decimal> loanAmountList = new List<decimal>();

                    foreach (EmployeeLoan employeeLoan in employeeLoans)
                    {
                        if (employeeLoan.NextDeduction <= employeeLoan.Duration)
                        {
                            var loanPayment = await _repositoryEmployeeLoanRepaymentSchedule.FirstOrDefaultAsync(a => a.EmployeeLoanId == employeeLoan.Id && a.Period == employeeLoan.NextDeduction);
                            //obtain loan type...
                            var loanType = _repositoryLoanType.GetAll().Where(a => a.Id == employeeLoan.LoanTypeId).FirstOrDefault();

                            var monthlyLoanDeduction = new MonthlyLoanDeduction
                            {
                                EmployeeId = employeeInfo.Id,
                                EmployeeName = employeeName,
                                EmployeeCategory = employeeInfo.CategoryName,
                                EmployeeDepartment = employeeInfo.DepartmentName,
                                EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                EmployeeLoanName = loanType.Name,
                                LoanTypeName = loanType.Name,
                                Description = loanType.Name,
                                LoanTypeId = employeeLoan.LoanTypeId,
                                LoanPeriod = loanPayment.Period,
                                LoanAmount = employeeLoan.Amount,
                                RepayAmount = loanPayment.MonthlyPayment,
                                OpeningBalance = employeeLoan.CurrentBalance,
                                ClosingBalance = loanPayment.InterestPlusPrincipalBalance,
                                Month = currentPayMonth.Month,
                                Year = currentPayMonth.Year,
                                ScheduleDate = loanPayment.ScheduleDate,
                                MonthlyPayment = loanPayment.MonthlyPayment,
                                PrincipalPayment = loanPayment.PrincipalBalance,
                                InterestPayment = loanPayment.InterestPayment,
                                PrincipalBalance = loanPayment.PrincipalBalance,
                                InterestPlusPrincipalBalance = loanPayment.InterestPlusPrincipalBalance,
                                EmployeeLoanId = employeeLoan.Id,

                            };

                            loanAmountList.Add(employeeLoan.MonthlyDeduction);

                            await _repositoryMonthlyLoanDeduction.InsertAsync(monthlyLoanDeduction);

                            //save to cumulative deduction...
                            var cumulativeDeduction1 = new MonthlyCumulativeDeduction
                            {
                                EmployeeId = employeeInfo.Id,
                                EmployeeName = employeeName,
                                EmployeeCategory = employeeInfo.CategoryName,
                                EmployeeDepartment = employeeInfo.DepartmentName,
                                EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                Description = "Loan - " + loanType.Name,
                                Amount = employeeLoan.MonthlyDeduction,
                                Month = currentPayMonth.Month,
                                Year = currentPayMonth.Year,
                            };

                            await _repositoryMonthlyCumulativeDeduction.InsertAsync(cumulativeDeduction1);

                        }
                    }


                    //calculate overtime...
                    List<decimal> overtimeAmountList = new List<decimal>();

                    List<OvertimeRate> overtimeRates = new List<OvertimeRate>();

                    var overtimeTypes = await _repositoryOverTimeType.GetAllListAsync();

                    overtimeTypes.ForEach((a) =>
                    {
                        overtimeRates.AddRange(a.Rates);
                    });


                    foreach (OvertimeTimeSheet empoyeeOvertime in employeeOvertimes)
                    {
                        //Obtain overtime rate...
                        var overtimeRate = overtimeRates.FirstOrDefault(a => a.OvertimeTypeId == empoyeeOvertime.OvertimeTypeId && a.EmployeeCategoryId == employeeInfo.CategoryId);

                        if (overtimeRate != null)
                        {
                            decimal dailyRate = employeeSalaryInfo.MonthlySalary / daysInPayCalendar.Days;

                            if (overtimeRate.FixedAmount && overtimeRate.Prorate && overtimeRate.IsFactor)
                            {
                                //convert days,hours,minutes to days
                                decimal otDaysWorked = (empoyeeOvertime.OvertimeHours * employeeSalaryInfo.DailyHours) + (empoyeeOvertime.OvertimeMinutes / (employeeSalaryInfo.DailyHours * 60));

                                decimal otAmount = overtimeRate.Amount * (otDaysWorked / daysInPayCalendar.Days) * dailyRate * overtimeRate.Factor;

                                if (overtimeRate.MaximumAmount != 0 && otAmount > overtimeRate.MaximumAmount)
                                {
                                    otAmount = overtimeRate.MaximumAmount;
                                }
                                overtimeAmountList.Add(otAmount);

                                var mot = new MonthlyOvertime
                                {
                                    OvertimeTypeId = empoyeeOvertime.OvertimeTypeId,
                                    Amount = otAmount,
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                    OvertimeHours = empoyeeOvertime.OvertimeHours + (empoyeeOvertime.OvertimeMinutes / 60),

                                };

                                await _repositoryMonthlyOvertime.InsertAsync(mot);

                            }
                            if (overtimeRate.FixedAmount && !overtimeRate.Prorate && overtimeRate.IsFactor)
                            {

                                decimal otAmount = overtimeRate.Amount * dailyRate * overtimeRate.Factor;

                                if (overtimeRate.MaximumAmount != 0 && otAmount > overtimeRate.MaximumAmount)
                                {
                                    otAmount = overtimeRate.MaximumAmount;
                                }
                                overtimeAmountList.Add(otAmount);
                                var mot = new MonthlyOvertime
                                {
                                    OvertimeTypeId = empoyeeOvertime.OvertimeTypeId,
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Amount = otAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                    OvertimeHours = empoyeeOvertime.OvertimeHours + (empoyeeOvertime.OvertimeMinutes / 60),


                                };

                                await _repositoryMonthlyOvertime.InsertAsync(mot);

                            }
                            if (overtimeRate.FixedAmount && !overtimeRate.Prorate && !overtimeRate.IsFactor)
                            {
                                decimal otAmount = overtimeRate.Amount;

                                if (overtimeRate.MaximumAmount != 0 && otAmount > overtimeRate.MaximumAmount)
                                {
                                    otAmount = overtimeRate.MaximumAmount;
                                }
                                overtimeAmountList.Add(otAmount);
                                var mot = new MonthlyOvertime
                                {
                                    OvertimeTypeId = empoyeeOvertime.OvertimeTypeId,
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Amount = otAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                    OvertimeHours = empoyeeOvertime.OvertimeHours + (empoyeeOvertime.OvertimeMinutes / 60),


                                };

                                await _repositoryMonthlyOvertime.InsertAsync(mot);
                            }
                            if (overtimeRate.FixedAmount && overtimeRate.Prorate && !overtimeRate.IsFactor)
                            {
                                //convert days,hours,minutes to days
                                decimal otDaysWorked = (empoyeeOvertime.OvertimeHours * employeeSalaryInfo.DailyHours) + (empoyeeOvertime.OvertimeMinutes / (employeeSalaryInfo.DailyHours * 60));

                                decimal otAmount = overtimeRate.Amount * (otDaysWorked / daysInPayCalendar.Days);

                                if (overtimeRate.MaximumAmount != 0 && otAmount > overtimeRate.MaximumAmount)
                                {
                                    otAmount = overtimeRate.MaximumAmount;
                                }
                                overtimeAmountList.Add(otAmount);
                                var mot = new MonthlyOvertime
                                {
                                    OvertimeTypeId = empoyeeOvertime.OvertimeTypeId,
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Amount = otAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                    OvertimeHours = empoyeeOvertime.OvertimeHours + (60 / empoyeeOvertime.OvertimeMinutes),

                                };

                                await _repositoryMonthlyOvertime.InsertAsync(mot);

                            }
                            if (!overtimeRate.FixedAmount && overtimeRate.Prorate && overtimeRate.IsFactor)
                            {
                                //convert days,hours,minutes to days
                                decimal otDaysWorked = (empoyeeOvertime.OvertimeHours * employeeSalaryInfo.DailyHours) + (empoyeeOvertime.OvertimeMinutes / (employeeSalaryInfo.DailyHours * 60));

                                decimal otAmount = (overtimeRate.PercentageBasic / 100) * basicSalary * (otDaysWorked / daysInPayCalendar.Days) * dailyRate * overtimeRate.Factor;

                                if (overtimeRate.MaximumAmount != 0 && otAmount > overtimeRate.MaximumAmount)
                                {
                                    otAmount = overtimeRate.MaximumAmount;
                                }
                                overtimeAmountList.Add(otAmount);
                                var mot = new MonthlyOvertime
                                {
                                    OvertimeTypeId = empoyeeOvertime.OvertimeTypeId,
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Amount = otAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                    OvertimeHours = empoyeeOvertime.OvertimeHours + (empoyeeOvertime.OvertimeMinutes / 60),

                                };

                                await _repositoryMonthlyOvertime.InsertAsync(mot);

                            }
                            if (!overtimeRate.FixedAmount && overtimeRate.Prorate && !overtimeRate.IsFactor)
                            {
                                //convert days,hours,minutes to days
                                decimal otDaysWorked = (empoyeeOvertime.OvertimeHours * employeeSalaryInfo.DailyHours) + (empoyeeOvertime.OvertimeMinutes / (employeeSalaryInfo.DailyHours * 60));

                                decimal otAmount = (overtimeRate.PercentageBasic / 100) * basicSalary * (otDaysWorked / daysInPayCalendar.Days);

                                if (overtimeRate.MaximumAmount != 0 && otAmount > overtimeRate.MaximumAmount)
                                {
                                    otAmount = overtimeRate.MaximumAmount;
                                }
                                overtimeAmountList.Add(otAmount);
                                var mot = new MonthlyOvertime
                                {
                                    OvertimeTypeId = empoyeeOvertime.OvertimeTypeId,
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Amount = otAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                    OvertimeHours = empoyeeOvertime.OvertimeHours + (empoyeeOvertime.OvertimeMinutes / 60),

                                };

                                await _repositoryMonthlyOvertime.InsertAsync(mot);

                            }
                            if (!overtimeRate.FixedAmount && !overtimeRate.Prorate && overtimeRate.IsFactor)
                            {

                                decimal otAmount = (overtimeRate.PercentageBasic / 100) * basicSalary * dailyRate * overtimeRate.Factor;

                                if (overtimeRate.MaximumAmount != 0 && otAmount > overtimeRate.MaximumAmount)
                                {
                                    otAmount = overtimeRate.MaximumAmount;
                                }
                                overtimeAmountList.Add(otAmount);
                                var mot = new MonthlyOvertime
                                {
                                    OvertimeTypeId = empoyeeOvertime.OvertimeTypeId,
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Amount = otAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                    OvertimeHours = empoyeeOvertime.OvertimeHours + (empoyeeOvertime.OvertimeMinutes / 60),

                                };

                                await _repositoryMonthlyOvertime.InsertAsync(mot);

                            }
                            if (!overtimeRate.FixedAmount && !overtimeRate.Prorate && !overtimeRate.IsFactor)
                            {
                                decimal otAmount = (overtimeRate.PercentageBasic / 100) * basicSalary;

                                if (overtimeRate.MaximumAmount != 0 && otAmount > overtimeRate.MaximumAmount)
                                {
                                    otAmount = overtimeRate.MaximumAmount;
                                }

                                overtimeAmountList.Add(otAmount);
                                var mot = new MonthlyOvertime
                                {
                                    OvertimeTypeId = empoyeeOvertime.OvertimeTypeId,
                                    EmployeeId = employeeInfo.Id,
                                    EmployeeName = employeeName,
                                    EmployeeCategory = employeeInfo.CategoryName,
                                    EmployeeDepartment = employeeInfo.DepartmentName,
                                    EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                                    Amount = otAmount,
                                    Month = currentPayMonth.Month,
                                    Year = currentPayMonth.Year,
                                    OvertimeHours = empoyeeOvertime.OvertimeHours + (empoyeeOvertime.OvertimeMinutes / 60),

                                };

                                await _repositoryMonthlyOvertime.InsertAsync(mot);
                            }

                        }

                    }

                    decimal ssnitDeduction = 0;
                    decimal employerSSFDeduction = 0;

                    if (employeeSsnitContributions != null && employeeSsnitContributions.SocialSecurityFundEmployeeContribution != 0)
                    {
                        //employee
                        ssnitDeduction = (employeeSsnitContributions.SocialSecurityFundEmployeeContribution / 100) * basicSalary;
                        var monthlysnitdeduction = new MonthlySsnitDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Social Security (" + employeeSsnitContributions.SocialSecurityFundEmployeeContribution + "%)",
                            Amount = ssnitDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlySsnitDeduction.InsertAsync(monthlysnitdeduction);

                        //save to cumulative deduction...
                        var monthlysnitdeduction2 = new MonthlyCumulativeDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Social Security (" + employeeSsnitContributions.SocialSecurityFundEmployeeContribution + "%)",
                            Amount = ssnitDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlysnitdeduction2);

                        //employer
                        employerSSFDeduction = (employeeSsnitContributions.SocialSecurityFundEmployerContribution / 100) * basicSalary;
                        var monthlyemployersnitdeduction = new MonthlySsnitDeduction
                        {
                            EmployeeId = Guid.Empty,
                            EmployeeIdentifier = "--",
                            EmployeeName = "Social Security Employer",
                            Description = "Social Security Employer (" + employeeSsnitContributions.SocialSecurityFundEmployerContribution + "%)",
                            Amount = employerSSFDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlySsnitDeduction.InsertAsync(monthlyemployersnitdeduction);

                        //save to cumulative deduction...
                        var monthlyEmployerSnitdeduction2 = new MonthlyCumulativeDeduction
                        {
                            EmployeeId = Guid.Empty,
                            Description = "Social Security Employer (" + employeeSsnitContributions.SocialSecurityFundEmployerContribution + "%)",
                            Amount = employerSSFDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyEmployerSnitdeduction2);


                    }



                    //calculate provident fund...
                    decimal pfDeduction = 0;

                    if (employeeSsnitContributions != null && employeeSsnitContributions.ProvidentFundEmployeeContribution != 0)
                    {
                        pfDeduction = (employeeSsnitContributions.ProvidentFundEmployeeContribution / 100) * basicSalary;

                        var monthlypfdeduction = new MonthlyPfDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Provident Fund (" + employeeSsnitContributions.ProvidentFundEmployeeContribution + "%)",
                            Amount = pfDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyPfDeduction.InsertAsync(monthlypfdeduction);


                        //save to cumulative deduction...
                        var monthlyPfdeduction2 = new MonthlyCumulativeDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Provident Fund (" + employeeSsnitContributions.ProvidentFundEmployeeContribution + "%)",
                            Amount = pfDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyPfdeduction2);

                    }

                    //second provident fund...
                    decimal secPFEmployeeDeduction = 0;
                    if (employeeSsnitContributions != null && employeeSsnitContributions.SecondProvidentFundEmployeeContribution != 0)
                    {
                        secPFEmployeeDeduction = (employeeSsnitContributions.SecondProvidentFundEmployeeContribution / 100) * basicSalary;

                        var monthlysecPFDeduction = new MonthlySecPfDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Second Provident Fund (" + employeeSsnitContributions.SecondProvidentFundEmployeeContribution + "%)",
                            Amount = secPFEmployeeDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlySecPfDeduction.InsertAsync(monthlysecPFDeduction);

                        //save to cumulative deduction...
                        var monthlysecPFDeduction2 = new MonthlyCumulativeDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Second Provident Fund (" + employeeSsnitContributions.SecondProvidentFundEmployeeContribution + "%)",
                            Amount = secPFEmployeeDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlysecPFDeduction2);

                    }

                    decimal secPFEmployerDeduction = 0;
                    if (employeeSsnitContributions != null && employeeSsnitContributions.SecondProvidentFundEmployerContribution != 0)
                    {
                        secPFEmployerDeduction = (employeeSsnitContributions.SecondProvidentFundEmployerContribution / 100) * basicSalary;

                        var monthlysecPFDeduction = new MonthlySecPfDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Second Provident Fund-Employer (" + employeeSsnitContributions.SecondProvidentFundEmployerContribution + "%)",
                            Amount = secPFEmployerDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlySecPfDeduction.InsertAsync(monthlysecPFDeduction);

                        //save to cumulative deduction...
                        var monthlysecPFDeduction2 = new MonthlyCumulativeDeduction
                        {
                            EmployeeId = employeeInfo.Id,
                            EmployeeName = employeeName,
                            EmployeeCategory = employeeInfo.CategoryName,
                            EmployeeDepartment = employeeInfo.DepartmentName,
                            EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                            Description = "Second Provident Fund-Employer (" + employeeSsnitContributions.SecondProvidentFundEmployerContribution + "%)",
                            Amount = secPFEmployerDeduction,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlysecPFDeduction2);

                    }

                    decimal employerpfContribution = 0;

                    if (employeeSsnitContributions != null && employeeSsnitContributions.ProvidentFundEmployerContribution != 0)
                    {
                        employerpfContribution = (employeeSsnitContributions.ProvidentFundEmployerContribution / 100) * basicSalary;

                        var monthlyemployerpfdeduction = new MonthlyPfDeduction
                        {
                            EmployeeId = Guid.Empty,
                            Description = "Employer Provident Fund (" + employeeSsnitContributions.ProvidentFundEmployerContribution + "%)",
                            Amount = employerpfContribution,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyPfDeduction.InsertAsync(monthlyemployerpfdeduction);


                        //save to cumulative deduction...
                        var monthlyEmployerPfDeduction2 = new MonthlyCumulativeDeduction
                        {
                            EmployeeId = Guid.Empty,
                            Description = "Employer Provident Fund (" + employeeSsnitContributions.ProvidentFundEmployerContribution + "%)",
                            Amount = employerpfContribution,
                            Month = currentPayMonth.Month,
                            Year = currentPayMonth.Year,
                        };

                        await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyEmployerPfDeduction2);
                    }


                    decimal taxableIncome = basicSalary + taxableAllowance + employeeBenefitInKindAmountList.Sum() - ssnitDeduction - pfDeduction - secPFEmployeeDeduction - taxReliefAmountList.Sum();
                    //
                    decimal irsTax = await CalculateIrsTax(taxableIncome);

                    var monthlyIrsdeduction = new MonthlyIrsTax
                    {
                        EmployeeId = employeeInfo.Id,
                        EmployeeName = employeeName,
                        EmployeeCategory = employeeInfo.CategoryName,
                        EmployeeDepartment = employeeInfo.DepartmentName,
                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                        Description = "Income Tax",
                        Amount = irsTax,
                        Month = currentPayMonth.Month,
                        Year = currentPayMonth.Year,
                    };
                    await _repositoryMonthlyIrsTax.InsertAsync(monthlyIrsdeduction);

                    //save to cumulative deduction...
                    var monthlyIrsdeduction2 = new MonthlyCumulativeDeduction
                    {
                        EmployeeId = employeeInfo.Id,
                        EmployeeName = employeeName,
                        EmployeeCategory = employeeInfo.CategoryName,
                        EmployeeDepartment = employeeInfo.DepartmentName,
                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                        Description = "Income Tax",
                        Amount = irsTax,
                        Month = currentPayMonth.Month,
                        Year = currentPayMonth.Year,
                    };

                    await _repositoryMonthlyCumulativeDeduction.InsertAsync(monthlyIrsdeduction2);

                    decimal overtimeTax = 0;
                    decimal overtimeExcessTax = 0;
                    if (overtimeAmountList.Sum() < basicSalary * 0.5M)
                    {
                        overtimeTax = 0.05M * overtimeAmountList.Sum();
                    }
                    else if (overtimeAmountList.Sum() > basicSalary * 0.5M)
                    {
                        decimal remSalary = overtimeAmountList.Sum() - basicSalary * 0.5M;
                        decimal iotTax = 0.05M * (overtimeAmountList.Sum() - basicSalary * 0.5M);
                        overtimeExcessTax = 0.1M * remSalary;
                        overtimeTax = iotTax + overtimeExcessTax;

                    }

                    var overtimeTaxDeduction = new MonthlyCumulativeDeduction
                    {
                        EmployeeId = employeeInfo.Id,
                        EmployeeName = employeeName,
                        EmployeeCategory = employeeInfo.CategoryName,
                        EmployeeDepartment = employeeInfo.DepartmentName,
                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                        Description = "Overtime Tax",
                        Amount = overtimeTax,
                        Month = currentPayMonth.Month,
                        Year = currentPayMonth.Year,
                    };

                    await _repositoryMonthlyCumulativeDeduction.InsertAsync(overtimeTaxDeduction);

                    decimal netSalary = (basicSalary + employeeNonTaxableAllowanceList.Sum() + employeeTaxableAllowanceList.Sum(a => a.AllowanceAmount) + employeebonusList.Sum() + (overtimeAmountList.Sum() - overtimeTax)) - (irsTax + loanAmountList.Sum() + ssnitDeduction + pfDeduction + secPFEmployeeDeduction + employeeDeductionsList.Sum(a => a.AllowanceAmount + overtimeTax) + totalOnetimeDeduction + salaryAdvanceAmountList.Sum());

                    var payCurrency = await _repositoryCurrency.FirstOrDefaultAsync(employeeSalaryInfo.CurrencyId);


                    var paymasterrecord = new Paymaster
                    {
                        EmployeeId = employeeInfo.Id,
                        EmployeeName = employeeName,
                        EmployeeCategory = employeeInfo.CategoryName,
                        EmployeeDepartment = employeeInfo.DepartmentName,
                        EmployeeIdentifier = employeeInfo.EmployeeIdentifier,
                        EmployeeSSFDeduction = ssnitDeduction,
                        EmployerSSFDeduction = employerSSFDeduction,
                        BasicSalary = basicSalary,
                        IRSTax = irsTax,
                        NetSalary = netSalary,
                        TaxableAllowance = employeeTaxableAllowanceList.Sum(a => a.AllowanceAmount),
                        NonTaxableAllowance = employeeNonTaxableAllowanceList.Sum(),
                        TaxableOverTime = overtimeAmountList.Sum(),
                        OverTimeTax = overtimeTax,
                        NonTaxableOverTime = 0,
                        VoluntaryDeduction = 0,
                        DaysWorked = employeeDaysWorked,
                        TaxRelief = taxReliefAmountList.Sum(),
                        BenefitsInKind = employeeBenefitInKindAmountList.Sum(),
                        Month = currentPayMonth.Month,
                        Year = currentPayMonth.Year,
                        EmployeeProvidentFundContribution = pfDeduction,
                        EmployerProvidentFundContribution = employerpfContribution,
                        EmployeeSecProvidentFundContribution = secPFEmployeeDeduction,
                        EmployerSecProvidentFundContribution = secPFEmployerDeduction,
                        TaxableSpecialEarning = 0,
                        NonTaxableSpecialEarning = 0,
                        OneTimeDeduction = totalOnetimeDeduction,
                        SpecialEarning = employeebonusList.Sum(),
                        BonuxTax = 0,
                        IsPaid = false,
                        CurrencyId = employeeSalaryInfo.CurrencyId,
                        CurrencyName = payCurrency != null ? payCurrency.CurrencyCode: "GHS",
                        BuyRate = payCurrency != null ? payCurrency.BuyRate : 1,
                        SellRate = payCurrency != null ? payCurrency.SellRate : 1,
                        HoursWorked = employeeHoursWorked,
                        HourlyRate = employeeSalaryInfo.CurrentHourlyRate,
                        WithHeld = false,
                        WithHeldRate = 0,
                        OvertimeExcesTax = overtimeExcessTax,
                        ContributionType = "",
                        LoanDeduction = loanAmountList.Sum(),
                        TaxableIncome = taxableIncome,
                        TotalSalaryAdvance = salaryAdvanceAmountList.Sum()
                    };

                    await _repositoryPaymaster.InsertAsync(paymasterrecord);

                }
                 

            }

            return new { code = 200, message = "Payroll processed successfully!" };

        }


        public async Task PostPayroll()
        {
            //obtain & move allowances to history...
            var allowances = await _repositoryMonthlyAllowance.GetAllListAsync();
            foreach (MonthlyAllowance monthlyAllowance in allowances)
            {
                var allowanceHist = new MonthlyAllowanceHist
                {
                    EmployeeId = monthlyAllowance.EmployeeId,
                    AllowanceTypeId = monthlyAllowance.AllowanceTypeId,
                    EmployeeIdentifier = monthlyAllowance.EmployeeIdentifier,
                    EmployeeCategory = monthlyAllowance.EmployeeCategory,
                    EmployeeDepartment = monthlyAllowance.EmployeeDepartment,
                    EmployeeName = monthlyAllowance.EmployeeName,
                    Amount = monthlyAllowance.Amount,
                    Taxable = monthlyAllowance.Taxable,
                    SSF = monthlyAllowance.SSF,
                    ProvidentFund = monthlyAllowance.ProvidentFund,
                    IsMonthly = monthlyAllowance.IsMonthly,
                    AllowanceDays = monthlyAllowance.AllowanceDays,
                    Month = monthlyAllowance.Month,
                    Year = monthlyAllowance.Year,
                    CurrencyId = monthlyAllowance.CurrencyId

                };

                await _repositoryMonthlyAllowanceHist.InsertAsync(allowanceHist);
            }

            //obtain & move deductions to history...
            var deductions = await _repositoryMonthlyDeduction.GetAllListAsync();
            foreach (MonthlyDeduction monthlyDeduction in deductions)
            {
                var deductionsHist = new MonthlyDeductionHist
                {
                    EmployeeId = monthlyDeduction.EmployeeId,
                    EmployeeIdentifier = monthlyDeduction.EmployeeIdentifier,
                    EmployeeCategory = monthlyDeduction.EmployeeCategory,
                    EmployeeDepartment = monthlyDeduction.EmployeeDepartment,
                    EmployeeName = monthlyDeduction.EmployeeName,
                    DeductionTypeId = monthlyDeduction.DeductionTypeId,
                    DeductionTypeName= monthlyDeduction.DeductionTypeName,
                    Amount = monthlyDeduction.Amount,
                    EmployerAmount = monthlyDeduction.EmployerAmount,
                    Month = monthlyDeduction.Month,
                    Year = monthlyDeduction.Year,

                };

                await _repositoryMonthlyDeductionHist.InsertAsync(deductionsHist);

            }

            //obtain & move benefits in kind...
            var biks = await _repositoryMonthlyBenefitsInKind.GetAllListAsync();
            foreach (MonthlyBenefitsInKind monthlyBenefitsInKind in biks)
            {
                var bikHist = new MonthlyBenefitsInKindHist
                {
                    EmployeeId = monthlyBenefitsInKind.EmployeeId,
                    EmployeeIdentifier = monthlyBenefitsInKind.EmployeeIdentifier,
                    EmployeeCategory = monthlyBenefitsInKind.EmployeeCategory,
                    EmployeeDepartment = monthlyBenefitsInKind.EmployeeDepartment,
                    EmployeeName = monthlyBenefitsInKind.EmployeeName,
                    BenefitInKindTypeId = monthlyBenefitsInKind.BenefitInKindTypeId,
                    BenefitInKindTypeName= monthlyBenefitsInKind.BenefitInKindTypeName,
                    Amount = monthlyBenefitsInKind.Amount,
                    Month = monthlyBenefitsInKind.Month,
                    Year = monthlyBenefitsInKind.Year,
                };
                await _repositoryMonthlyBenefitsInKindHist.InsertAsync(bikHist);

            }

            //obtain and move reliefs
            var reliefs = await _repositoryMonthlyRelief.GetAllListAsync();
            foreach (MonthlyRelief monthlyRelief in reliefs)
            {
                var reliefHist = new MonthlyReliefHist
                {
                    EmployeeId = monthlyRelief.EmployeeId,
                    EmployeeIdentifier = monthlyRelief.EmployeeIdentifier,
                    EmployeeCategory = monthlyRelief.EmployeeCategory,
                    EmployeeDepartment = monthlyRelief.EmployeeDepartment,
                    EmployeeName = monthlyRelief.EmployeeName,
                    ReliefTypeId = monthlyRelief.ReliefTypeId,
                    ReliefTypeName = monthlyRelief.ReliefTypeName,
                    Amount = monthlyRelief.Amount,
                    Month = monthlyRelief.Month,
                    Year = monthlyRelief.Year,

                };

                await _repositoryMonthlyReliefHist.InsertAsync(reliefHist);

            }

            //obtain and move irsTax
            var monthlyIRSTaxes = await _repositoryMonthlyIrsTax.GetAllListAsync();
            foreach (MonthlyIrsTax monthlyIRSTax in monthlyIRSTaxes)
            {
                var irsTaxHist = new MonthlyIrsTaxHist
                {
                    EmployeeId = monthlyIRSTax.EmployeeId,
                    EmployeeIdentifier = monthlyIRSTax.EmployeeIdentifier,
                    EmployeeCategory = monthlyIRSTax.EmployeeCategory,
                    EmployeeDepartment = monthlyIRSTax.EmployeeDepartment,
                    EmployeeName = monthlyIRSTax.EmployeeName,
                    Description = monthlyIRSTax.Description,
                    Amount = monthlyIRSTax.Amount,
                    Month = monthlyIRSTax.Month,
                    Year = monthlyIRSTax.Year

                };

                await _repositoryMonthlyIrsTaxHist.InsertAsync(irsTaxHist);
            }

            //obtain and move ssnit deductions...
            var monthlySnitDeductions = await _repositoryMonthlySsnitDeduction.GetAllListAsync();
            foreach (MonthlySsnitDeduction monthlySSNITDeduction in monthlySnitDeductions)
            {
                var snitDeductionHist = new MonthlySsnitDeductionHist
                {
                    EmployeeId = monthlySSNITDeduction.EmployeeId,
                    EmployeeIdentifier = monthlySSNITDeduction.EmployeeIdentifier,
                    EmployeeCategory = monthlySSNITDeduction.EmployeeCategory,
                    EmployeeDepartment = monthlySSNITDeduction.EmployeeDepartment,
                    EmployeeName = monthlySSNITDeduction.EmployeeName,
                    Description = monthlySSNITDeduction.Description,
                    Amount = monthlySSNITDeduction.Amount,
                    Month = monthlySSNITDeduction.Month,
                    Year = monthlySSNITDeduction.Year
                };

                await _repositoryMonthlySsnitDeductionHist.InsertAsync(snitDeductionHist);

            }

            //obtain anf move salary advances
            var monthSalaryAdvances = await _repositoryMonthlySalaryAdvance.GetAllListAsync();
            foreach (MonthlySalaryAdvance msadv in monthSalaryAdvances)
            {
                var salaryAdvanceHist = new MonthlySalaryAdvanceHist
                {
                    EmployeeId = msadv.EmployeeId,
                    EmployeeIdentifier = msadv.EmployeeIdentifier,
                    EmployeeCategory = msadv.EmployeeCategory,
                    EmployeeDepartment = msadv.EmployeeDepartment,
                    EmployeeName = msadv.EmployeeName,
                    Month = msadv.Month,
                    Year = msadv.Year,
                    Amount = msadv.Amount,
                    LoanDate = msadv.LoanDate,
                    LoanTypeId = msadv.LoanTypeId

                };
                await _repositoryMonthlySalaryAdvanceHist.InsertAsync(salaryAdvanceHist);
            }


            //obtain and move loan deductions...
            var monthlyLoanDeducions = await _repositoryMonthlyLoanDeduction.GetAllListAsync();
            foreach (MonthlyLoanDeduction monthlyLoanDeduction in monthlyLoanDeducions)
            {
                var monthlyLoanDeductionHist = new MonthlyLoanDeductionHist
                {
                    EmployeeId = monthlyLoanDeduction.EmployeeId,
                    EmployeeIdentifier = monthlyLoanDeduction.EmployeeIdentifier,
                    EmployeeCategory = monthlyLoanDeduction.EmployeeCategory,
                    EmployeeDepartment = monthlyLoanDeduction.EmployeeDepartment,
                    EmployeeName = monthlyLoanDeduction.EmployeeName,
                    Description = monthlyLoanDeduction.Description,
                    LoanTypeId = monthlyLoanDeduction.LoanTypeId,
                    LoanPeriod = monthlyLoanDeduction.LoanPeriod,
                    LoanAmount = monthlyLoanDeduction.LoanAmount,
                    RepayAmount = monthlyLoanDeduction.RepayAmount,
                    OpeningBalance = monthlyLoanDeduction.OpeningBalance,
                    ClosingBalance = monthlyLoanDeduction.ClosingBalance,
                    Month = monthlyLoanDeduction.Month,
                    Year = monthlyLoanDeduction.Year,
                    ScheduleDate = monthlyLoanDeduction.ScheduleDate,
                    MonthlyPayment = monthlyLoanDeduction.MonthlyPayment,
                    PrincipalPayment = monthlyLoanDeduction.PrincipalPayment,
                    InterestPayment = monthlyLoanDeduction.InterestPayment,
                    PrincipalBalance = monthlyLoanDeduction.PrincipalBalance,
                    InterestPlusPrincipalBalance = monthlyLoanDeduction.InterestPlusPrincipalBalance,
                    EmployeeLoanId = monthlyLoanDeduction.EmployeeLoanId

                };

                await _repositoryMonthlyLoanDeductionHist.InsertAsync(monthlyLoanDeductionHist);

                //obtain loan by loan id
                var loanObj = await _repositoryEmployeeLoan.FirstOrDefaultAsync(a => a.Id == monthlyLoanDeduction.EmployeeLoanId);

                if (loanObj.NextDeduction <= loanObj.Duration && loanObj.CurrentBalance > 0)
                {
                    var loanPayment = await _repositoryEmployeeLoanRepaymentSchedule.FirstOrDefaultAsync(a => a.EmployeeLoanId == loanObj.Id && a.Period == loanObj.NextDeduction);

                    loanObj.CurrentBalance = loanObj.CurrentBalance - loanPayment.MonthlyPayment;
                    //update employee loans..
                    loanObj.NextDeduction = loanObj.NextDeduction + 1;

                    await _repositoryEmployeeLoan.UpdateAsync(loanObj);
                }


            }

            //obtain and move monthly PF...
            var monthlyPfDedcutions = await _repositoryMonthlyPfDeduction.GetAllListAsync();

            foreach (MonthlyPfDeduction monthlyPfDeduction in monthlyPfDedcutions)
            {
                var monthlyPfDeductionHist = new MonthlyPfDeductionHist
                {
                    EmployeeId = monthlyPfDeduction.EmployeeId,
                    EmployeeIdentifier = monthlyPfDeduction.EmployeeIdentifier,
                    EmployeeCategory = monthlyPfDeduction.EmployeeCategory,
                    EmployeeDepartment = monthlyPfDeduction.EmployeeDepartment,
                    EmployeeName = monthlyPfDeduction.EmployeeName,
                    Description = monthlyPfDeduction.Description,
                    Amount = monthlyPfDeduction.Amount,
                    Month = monthlyPfDeduction.Month,
                    Year = monthlyPfDeduction.Year

                };

                await _repositoryMonthlyPfDeductionHist.InsertAsync(monthlyPfDeductionHist);

            }

            //obtain and move monthly second PF...
            var monthlySecPfDedcutions = await _repositoryMonthlySecPfDeduction.GetAllListAsync();

            foreach (MonthlySecPfDeduction monthlySecPfDeduction in monthlySecPfDedcutions)
            {
                var monthlySecPFDeductionHist = new MonthlySecPfDeductionHist
                {
                    EmployeeId = monthlySecPfDeduction.EmployeeId,
                    EmployeeIdentifier = monthlySecPfDeduction.EmployeeIdentifier,
                    EmployeeCategory = monthlySecPfDeduction.EmployeeCategory,
                    EmployeeDepartment = monthlySecPfDeduction.EmployeeDepartment,
                    EmployeeName = monthlySecPfDeduction.EmployeeName,
                    Description = monthlySecPfDeduction.Description,
                    Amount = monthlySecPfDeduction.Amount,
                    Month = monthlySecPfDeduction.Month,
                    Year = monthlySecPfDeduction.Year
                };

                await _repositoryMonthlySecPfDeductionHist.InsertAsync(monthlySecPFDeductionHist);
            }

            //obtain and move monthly one time deduction...
            var monthlyOTDs = await _repositoryMonthlyOnetimeDeduction.GetAllListAsync();
            foreach (MonthlyOnetimeDeduction monthlyOnetimeDeduction in monthlyOTDs)
            {
                var monthlyOTDHist = new MonthlyOnetimeDeductionHist
                {
                    EmployeeId = monthlyOnetimeDeduction.EmployeeId,
                    EmployeeIdentifier = monthlyOnetimeDeduction.EmployeeIdentifier,
                    EmployeeCategory = monthlyOnetimeDeduction.EmployeeCategory,
                    EmployeeDepartment = monthlyOnetimeDeduction.EmployeeDepartment,
                    EmployeeName = monthlyOnetimeDeduction.EmployeeName,
                    Description = monthlyOnetimeDeduction.Description,
                    Amount = monthlyOnetimeDeduction.Amount,
                    Month = monthlyOnetimeDeduction.Month,
                    Year = monthlyOnetimeDeduction.Year

                };

                await _repositoryMonthlyOnetimeDeductionHist.InsertAsync(monthlyOTDHist);
            }

            //obtain and move cumulative deduction history...
            var monthlyCumulativeDeductions = await _repositoryMonthlyCumulativeDeduction.GetAllListAsync();

            foreach (MonthlyCumulativeDeduction monthlyCumulativeDeduction in monthlyCumulativeDeductions)
            {
                var monthlycumDeductionHist = new MonthlyCumulativeDeductionHist
                {
                    EmployeeId = monthlyCumulativeDeduction.EmployeeId,
                    EmployeeIdentifier = monthlyCumulativeDeduction.EmployeeIdentifier,
                    EmployeeCategory = monthlyCumulativeDeduction.EmployeeCategory,
                    EmployeeDepartment = monthlyCumulativeDeduction.EmployeeDepartment,
                    EmployeeName = monthlyCumulativeDeduction.EmployeeName,
                    Description = monthlyCumulativeDeduction.Description,
                    Amount = monthlyCumulativeDeduction.Amount,
                    Month = monthlyCumulativeDeduction.Month,
                    Year = monthlyCumulativeDeduction.Year,
                    

                };

                await _repositoryMonthlyCumulativeDeductionHist.InsertAsync(monthlycumDeductionHist);
            }

            //obtain and move paymaster...
            var paymasters = await _repositoryPaymaster.GetAllListAsync();

            foreach (Paymaster paymaster in paymasters)
            {
                var paymasterHist = new PaymasterHist
                {
                    EmployeeId = paymaster.EmployeeId,
                    EmployeeIdentifier = paymaster.EmployeeIdentifier,
                    EmployeeName = paymaster.EmployeeName,
                    EmployeeDepartment = paymaster.EmployeeDepartment,
                    EmployeeCategory = paymaster.EmployeeCategory,
                    EmployeeSSFDeduction = paymaster.EmployeeSSFDeduction,
                    EmployerSSFDeduction = paymaster.EmployerSSFDeduction,
                    BasicSalary = paymaster.BasicSalary,
                    IRSTax = paymaster.IRSTax,
                    NetSalary = paymaster.NetSalary,
                    TaxableAllowance = paymaster.TaxableAllowance,
                    NonTaxableAllowance = paymaster.NonTaxableAllowance,
                    TaxableOverTime = paymaster.TaxableOverTime,
                    OverTimeTax = paymaster.OverTimeTax,
                    NonTaxableOverTime = paymaster.NonTaxableOverTime,
                    VoluntaryDeduction = paymaster.VoluntaryDeduction,
                    DaysWorked = paymaster.DaysWorked,
                    TaxRelief = paymaster.TaxRelief,
                    BenefitsInKind = paymaster.BenefitsInKind,
                    Month = paymaster.Month,
                    Year = paymaster.Year,
                    EmployeeProvidentFundContribution = paymaster.EmployeeProvidentFundContribution,
                    EmployerProvidentFundContribution = paymaster.EmployerProvidentFundContribution,
                    EmployeeSecProvidentFundContribution = paymaster.EmployeeSecProvidentFundContribution,
                    EmployerSecProvidentFundContribution = paymaster.EmployerSecProvidentFundContribution,
                    TaxableSpecialEarning = paymaster.TaxableSpecialEarning,
                    NonTaxableSpecialEarning = paymaster.NonTaxableSpecialEarning,
                    OneTimeDeduction = paymaster.OneTimeDeduction,
                    SpecialEarning = paymaster.SpecialEarning,
                    BonuxTax = paymaster.BonuxTax,
                    IsPaid = true,
                    CurrencyId = paymaster.CurrencyId,
                    BuyRate = paymaster.BuyRate,
                    SellRate = paymaster.SellRate,
                    HoursWorked = paymaster.HoursWorked,
                    HourlyRate = paymaster.HourlyRate,
                    WithHeld = paymaster.WithHeld,
                    WithHeldRate = paymaster.WithHeldRate,
                    OvertimeExcesTax = paymaster.OvertimeExcesTax,
                    ContributionType = paymaster.ContributionType,
                    LoanDeduction = paymaster.LoanDeduction,
                    TaxableIncome = paymaster.TaxableIncome
                };

                await _repositoryPaymasterHist.InsertAsync(paymasterHist);
            }


            var overtimes = await _repositoryMonthlyOvertime.GetAllListAsync();
            foreach (MonthlyOvertime monthlyOvertime in overtimes)
            {
                var mothist = new MonthlyOvertimeHistory
                {
                    EmployeeId = monthlyOvertime.EmployeeId,
                    EmployeeIdentifier = monthlyOvertime.EmployeeIdentifier,
                    EmployeeName = monthlyOvertime.EmployeeName,
                    OvertimeTypeId = monthlyOvertime.OvertimeTypeId,
                    OvertimeTypeName = monthlyOvertime.OvertimeTypeName,
                    Amount = monthlyOvertime.Amount,
                    Month = monthlyOvertime.Month,
                    Year = monthlyOvertime.Year,
                    OvertimeHours = monthlyOvertime.OvertimeHours
                };

                await _repositoryMonthlyOvertimeHist.InsertAsync(mothist);
            }

            //=>change nextdeduction for monthly loan if any...
            //var employeeLoans = _repositoryEmployeeLoan.GetAll().ToList();
            //foreach (EmployeeLoan employeeLoan in employeeLoans)
            //{
            //    if (employeeLoan.NextDeduction<= employeeLoan.Duration)
            //    {
            //        employeeLoan.NextDeduction = employeeLoan.NextDeduction + 1;
            //        var loanPayment = _repositoryEmployeeLoanRepaymentSchedule.GetAll().Where(a => a.EmployeeId == employeeLoan.EmployeeId && a.EmployeeLoanId == employeeLoan.Id && a.Period == employeeLoan.NextDeduction).FirstOrDefault();

            //        employeeLoan.CurrentBalance = employeeLoan.CurrentBalance - loanPayment.MonthlyPayment;
            //        //update employee loans..
            //        await _repositoryEmployeeLoan.UpdateAsync(employeeLoan);
            //    }

            //}

            //=>re initialize payrol month and year.
            var currentPayMonthYr = _repositoryInitializePayMonth.GetAll().FirstOrDefault();
            int currentMonth = 0; int currentYear = 0;

            if (currentPayMonthYr.Month == 12)
            {
                //means we are in december... reset to January=>1,
                currentMonth = 1; currentYear = currentPayMonthYr.Year + 1;
                currentPayMonthYr.Month = currentMonth;
                currentPayMonthYr.Year= currentYear;
            }
            else
            {
                //means we are not yet in december...
                currentPayMonthYr.Month = currentPayMonthYr.Month + 1;
                currentYear = currentPayMonthYr.Year;
            }

            await _repositoryInitializePayMonth.UpdateAsync(currentPayMonthYr);

        }


        public async Task<object> GetPayrollResults()
        {
            return await _repositoryPaymaster.GetAllListAsync();
        }

        public async Task<object> GetPayrollResults(Guid employeeId)
        {
            return await _repositoryPaymaster.GetAllListAsync(x=>x.EmployeeId == employeeId);
        }


        private async Task<decimal> CalculateIrsTax(decimal salary)
        {
            var taxTable = await _repositoryTaxTable.GetAllListAsync();
            decimal currentSalary = salary;
            //
            decimal taxAmount = 0;
            decimal salaryBalance = 0;
            decimal taxUpperLimit = 0;


            for (int i = 0; i < taxTable.Count(); i++)
            {

                if (currentSalary >= taxTable[i].UpperLimitOfAmount)
                {
                    //calculate tax
                    taxAmount += (taxTable[i].Rate / 100) * taxTable[i].UpperLimitOfAmount;
                    //get salary remainder
                    salaryBalance = currentSalary - taxTable[i].UpperLimitOfAmount;
                    //update current salary for next salary...
                    currentSalary = salaryBalance;

                }
                else if (currentSalary <= taxTable[i].UpperLimitOfAmount)
                {
                    //last stop
                    taxUpperLimit = salaryBalance;
                    taxAmount += (taxTable[i].Rate / 100) * salaryBalance;
                    break;

                }

            }

            return taxAmount;

        }
        private async Task InitPayroll()
        {
            var monthlyAllowances = await _repositoryMonthlyAllowance.GetAllListAsync();
            foreach (MonthlyAllowance employeeAllowance in monthlyAllowances)
            {
                await _repositoryMonthlyAllowance.DeleteAsync(employeeAllowance.Id);
            }
            var monthlyDeductions = await _repositoryMonthlyDeduction.GetAllListAsync();
            foreach (MonthlyDeduction monthlyDeduction in monthlyDeductions)
            {
                await _repositoryMonthlyDeduction.DeleteAsync(monthlyDeduction.Id);
            }

            var monthlybenefitsInKind = await _repositoryMonthlyBenefitsInKind.GetAllListAsync();
            foreach (MonthlyBenefitsInKind monthlyBenefitsInKind in monthlybenefitsInKind)
            {
                await _repositoryMonthlyBenefitsInKind.DeleteAsync(monthlyBenefitsInKind.Id);
            }

            var monthlyReliefs = await _repositoryMonthlyRelief.GetAllListAsync();
            foreach (MonthlyRelief monthlyRelief in monthlyReliefs)
            {
                await _repositoryMonthlyRelief.DeleteAsync(monthlyRelief.Id);
            }

            var paymasters = await _repositoryPaymaster.GetAllListAsync();
            foreach (Paymaster paymaster in paymasters)
            {
                await _repositoryPaymaster.DeleteAsync(paymaster.Id);
            }

            var monthlyonetimedeductions = await _repositoryMonthlyOnetimeDeduction.GetAllListAsync();
            foreach (MonthlyOnetimeDeduction monthlyOnetimeDeduction in monthlyonetimedeductions)
            {
                await _repositoryMonthlyOnetimeDeduction.DeleteAsync(monthlyOnetimeDeduction.Id);

            }

            var monthlyirstaxes = await _repositoryMonthlyIrsTax.GetAllListAsync();
            foreach (MonthlyIrsTax monthlyIRSTax in monthlyirstaxes)
            {
                await _repositoryMonthlyIrsTax.DeleteAsync(monthlyIRSTax.Id);
            }

            var monthlyCumulativeDeductions = await _repositoryMonthlyCumulativeDeduction.GetAllListAsync();
            foreach (MonthlyCumulativeDeduction monthlyCumulativeDeduction in monthlyCumulativeDeductions)
            {
                await _repositoryMonthlyCumulativeDeduction.DeleteAsync(monthlyCumulativeDeduction.Id);
            }

            var monthlyPFDeductions = await _repositoryMonthlyPfDeduction.GetAllListAsync();
            foreach (MonthlyPfDeduction monthlyPFDeduction in monthlyPFDeductions)
            {
                await _repositoryMonthlyPfDeduction.DeleteAsync(monthlyPFDeduction.Id);
            }

            var monthlyLoanDeductions = await _repositoryMonthlyLoanDeduction.GetAllListAsync();
            foreach (MonthlyLoanDeduction monthlyLoanDeduction in monthlyLoanDeductions)
            {
                await _repositoryMonthlyLoanDeduction.DeleteAsync(monthlyLoanDeduction.Id);
            }

            var monthlySsnitDeductions = await _repositoryMonthlySsnitDeduction.GetAllListAsync();
            foreach (MonthlySsnitDeduction monthlySsnitDeduction in monthlySsnitDeductions)
            {
                await _repositoryMonthlySsnitDeduction.DeleteAsync(monthlySsnitDeduction.Id);
            }

            var monthlySecPfDeductions = await _repositoryMonthlySecPfDeduction.GetAllListAsync();
            foreach (MonthlySecPfDeduction monthlySecPfDeduction in monthlySecPfDeductions)
            {
                await _repositoryMonthlySecPfDeduction.DeleteAsync(monthlySecPfDeduction.Id);
            }

            var monthlySalaryAdvanceList = await _repositoryMonthlySalaryAdvance.GetAllListAsync();
            foreach (MonthlySalaryAdvance monthlySalaryAdvance in monthlySalaryAdvanceList)
            {
                await _repositoryMonthlySalaryAdvance.DeleteAsync(monthlySalaryAdvance.Id);

            }

            var monthlyBonuses = await _repositoryMonthlyBonus.GetAllListAsync();
            foreach (MonthlyBonus monthlyBonus in monthlyBonuses)
            {
                await _repositoryMonthlyBonus.DeleteAsync(monthlyBonus.Id);

            }

            var monthlyOvertimeList = await _repositoryMonthlyOvertime.GetAllListAsync();
            foreach (MonthlyOvertime monthlyOvertime in monthlyOvertimeList)
            {
                await _repositoryMonthlyOvertime.DeleteAsync(monthlyOvertime.Id);
            }
        }


    }
}
