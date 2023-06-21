using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using RhyoliteERP.Authorization.Roles;
using RhyoliteERP.Authorization.Users;
using RhyoliteERP.MultiTenancy;
using System;
using RhyoliteERP.Models.Ap;
using RhyoliteERP.Models.Ar;
using RhyoliteERP.Models.Banking;
using RhyoliteERP.Models.Ledger;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Models.Stock;
using RhyoliteERP.Models.PropertyRental;

namespace RhyoliteERP.EntityFrameworkCore
{
    public class RhyoliteERPDbContext : AbpZeroDbContext<Tenant, Role, User, RhyoliteERPDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public RhyoliteERPDbContext(DbContextOptions<RhyoliteERPDbContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        //school manager
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<AlumniHistory> AlumniHistories { get; set; }
        public DbSet<AssignedDesignation> AssignedDesignations { get; set; }
        public DbSet<AssignedClass> AssignedClasses { get; set; }
        public DbSet<AssignSpecialDuty> AssignSpecialDuties { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<Attitude> Attitudes { get; set; }
        public DbSet<StudentBillReceipt> StudentBillReceipts { get; set; }
        public DbSet<StudentBill> StudentBills { get; set; }
        public DbSet<BillPayment> BillPayments { get; set; }
        public DbSet<BillSetup> BillSetups { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<CancelledBill> CancelledBills { get; set; }
        public DbSet<CancelledPayment> CancelledPayments { get; set; }
        public DbSet<ClassStream> ClassStreams { get; set; }
        public DbSet<Conduct> Conducts { get; set; }
        public DbSet<FeesDescription> FeesDescriptions { get; set; }
        public DbSet<ResultProportion> ResultProportions { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<MessageTemplate> MessageTemplates { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<PastQuestion> PastQuestions { get; set; }
        public DbSet<PromotionHistory> PromotionHistories { get; set; }
        public DbSet<ResultsUpload> ResultsUploads { get; set; }
        public DbSet<ResultType> ResultTypes { get; set; }
        public DbSet<SchClass> SchClasses { get; set; }
        public DbSet<SchoolProfile> SchoolProfiles { get; set; }
        public DbSet<Sibling> Siblings { get; set; }
        public DbSet<SpecialDuty> SpecialDuties { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffDesignation> StaffDesignations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<StudentStatement> StudentStatements { get; set; }
        public DbSet<StudentStatus> StudentStatuses { get; set; }
        public DbSet<TeacherRemark> TeacherRemarks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectRemark> SubjectRemarks { get; set; }
        public DbSet<TerminalReport> TerminalReports { get; set; }


        //payroll
        public DbSet<EmployeeCategory> EmployeeCategories { get; set; }
        public DbSet<SalaryAdvanceApplication> SalaryAdvanceApplications { get; set; }
        public DbSet<AllowanceType> AllowanceTypes { get; set; }
        public DbSet<DeductionType> DeductionTypes { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<TaxRelief> TaxReliefs { get; set; }
        public DbSet<TaxTable> TaxTables { get; set; }
        public DbSet<EmployeeRank> EmployeeRanks { get; set; }
        public DbSet<Gratuity> Gratuities { get; set; }
        public DbSet<BikType> BikTypes { get; set; }
        public DbSet<PayCalendar> PayCalendars { get; set; }
        public DbSet<SalaryGrade> SalaryGrades { get; set; }
        public DbSet<OvertimeType> OvertimeTypes { get; set; }
        public DbSet<EmployeeDaysWorked> EmployeeDaysWorkeds { get; set; }
        public DbSet<BonusAndOnetimeAllowance> BonusAndOnetimeAllowances { get; set; }
        public DbSet<EmployeeBioData> EmployeeBioDatas { get; set; }
        public DbSet<EmployeeSalaryInfo> EmployeeSalaryInfos { get; set; }
        public DbSet<EmployeeSnit> EmployeeSnits { get; set; }
        public DbSet<EmployeeSalaryAdvance> EmployeeSalaryAdvances { get; set; }
        public DbSet<EmployeeOnetimeDeduction> EmployeeOnetimeDeductions { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<EmployeeAllowance> EmployeeAllowances { get; set; }
        public DbSet<EmployeeDeduction> EmployeeDeductions { get; set; }
        public DbSet<EmployeeBenefitInKind> EmployeeBenefitInKinds { get; set; }
        public DbSet<EmployeeRelief> EmployeeReliefs { get; set; }
        public DbSet<OvertimeTimeSheet> OvertimeTimeSheets { get; set; }
        public DbSet<EmployeeLoan> EmployeeLoans { get; set; }
        public DbSet<InitializePayMonth> InitializePayMonths { get; set; }
        public DbSet<EmployeeLoanRepaymentSchedule> EmployeeLoanRepaymentSchedules { get; set; }
        public DbSet<MonthlyAllowance> MonthlyAllowances { get; set; }
        public DbSet<MonthlyDeduction> MonthlyDeductions { get; set; }
        public DbSet<MonthlyBenefitsInKind> MonthlyBenefitsInKinds { get; set; }
        public DbSet<MonthlyRelief> MonthlyReliefs { get; set; }
        public DbSet<Paymaster> Paymasters { get; set; }
        public DbSet<MonthlyAllowanceHist> MonthlyAllowanceHistory { get; set; }
        public DbSet<MonthlyDeductionHist> MonthlyDeductionHistory { get; set; }
        public DbSet<MonthlyBenefitsInKindHist> MonthlyBenefitsInKindHistory { get; set; }
        public DbSet<MonthlyReliefHist> MonthlyReliefHistory { get; set; }
        public DbSet<PaymasterHist> PaymasterHistory { get; set; }
        public DbSet<MonthlyCumulativeDeductionHist> MonthlyCumulativeDeductionHistory { get; set; }
        public DbSet<MonthlyCumulativeDeduction> MonthlyCumulativeDeductions { get; set; }
        public DbSet<MonthlySsnitDeductionHist> MonthlySsnitDeductionHistory { get; set; }
        public DbSet<MonthlySsnitDeduction> MonthlySsnitDeductions { get; set; }
        public DbSet<MonthlyOnetimeDeductionHist> MonthlyOnetimeDeductionHistory { get; set; }
        public DbSet<MonthlyOnetimeDeduction> MonthlyOnetimeDeductions { get; set; }
        public DbSet<SalaryIncrementHistory> SalaryIncrementHistory { get; set; }
        public DbSet<MonthlyPfDeductionHist> MonthlyPfDeductionHistory { get; set; }
        public DbSet<MonthlyPfDeduction> MonthlyPfDeductions { get; set; }
        public DbSet<MonthlyLoanDeductionHist> MonthlyLoanDeductionHistory { get; set; }
        public DbSet<MonthlyLoanDeduction> MonthlyLoanDeductions { get; set; }
        public DbSet<MonthlyIrsTaxHist> MonthlyIrsTaxHistory { get; set; }
        public DbSet<MonthlyIrsTax> MonthlyIrsTaxes { get; set; }
        public DbSet<MonthlySalaryAdvance> MonthlySalaryAdvances { get; set; }
        public DbSet<MonthlySecPfDeduction> MonthlySecPfDeductions { get; set; }
        public DbSet<MonthlySalaryAdvanceHist> MonthlySalaryAdvanceHistory { get; set; }
        public DbSet<MonthlySecPfDeductionHist> MonthlySecPfDeductionHistory { get; set; }
        public DbSet<MonthlyOvertimeHistory> MonthlyOvertimeHistory { get; set; }
        public DbSet<MonthlyOvertime> MonthlyOvertimes { get; set; }
        public DbSet<MonthlyBonus> MonthlyBonuses { get; set; }
        public DbSet<IrsSignature> IrsSignatures { get; set; }

        //shared
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<SystemNumber> SystemNumbers { get; set; }
        public DbSet<SupplierGroup> SupplierGroups { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryState> CountryStates { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Biometric> Biometrics { get; set; }
        public DbSet<ReportDownload> ReportDownloads { get; set; }
        

        //AR Services...
        public DbSet<ArInvoice> ArInvoices { get; set; }
        public DbSet<ArPayment> ArPayments { get; set; }
        public DbSet<Quotation> Quotations { get; set; }

        //AP Services...
        public DbSet<OpeningBalance> OpeningBalances { get; set; }
        public DbSet<ApInvoice> ApInvoices { get; set; }
        public DbSet<ApInvoiceHist> ApInvoiceHists { get; set; }
        public DbSet<ApVoucher> ApVouchers { get; set; }
        public DbSet<ApPayment> ApPayments { get; set; }
        public DbSet<PaymentApproval> PaymentApprovals { get; set; }
        public DbSet<ArOpeningBalance> ArOpeningBalances { get; set; }

        //GL
        public DbSet<CoaHierachy> CoaHierachies { get; set; }
        public DbSet<PettyCashRecipient> PettyCashRecipients { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<CoaControl> CoaControls { get; set; }
        public DbSet<CoaDetail> CoaDetails { get; set; }
        public DbSet<PvRetension> PvRetensions { get; set; }
        public DbSet<ImprestCategory> ImprestCategories { get; set; }
        public DbSet<Imprest> Imprests { get; set; }
        public DbSet<AccountingPeriod> AccountingPeriods { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<AccountBalance> AccountBalances { get; set; }
        public DbSet<JournalHistory> JournalHistories { get; set; }

        //Stock and Inventory Services
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<ModeOfShipment> ModeOfShipments { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<StockReceipt> StockReceipts { get; set; }
        public DbSet<PurchaseOrderStatus> PurchaseOrderStatuses { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<StockIssue> StockIssues { get; set; }
        public DbSet<ReturnIssuedStock> ReturnIssuedStocks { get; set; }
        public DbSet<ReturnIssuedStockDetail> ReturnIssuedStockDetails { get; set; }
        public DbSet<StockTransfer> StockTransfers { get; set; }

        //banking
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Teller> Tellers { get; set; }
        public DbSet<CashierTransaction> CashierTransactions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<SavingsAccountTransaction> SavingsAccountTransactions { get; set; }

        // sms & money
        public DbSet<SmsHistory> SmsHistory { get; set; }

        //property & rental...
        public DbSet<MeterType> MeterTypes { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyGroup> PropertyGroups { get; set; }
        public DbSet<PropertyTask> PropertyTasks { get; set; }
        public DbSet<PropertyUnit> PropertyUnits { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<LeaseTenant> LeaseTenants { get; set; }
        public DbSet<LeaseApplicant> LeaseApplicants { get; set; }
        public DbSet<LeasePayment> LeasePayments { get; set; }
        public DbSet<RentalOwner> RentalOwners { get; set; }
        public DbSet<VendorCategory> VendorCategories { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<RentalNotificationSetting> RentalNotificationSettings { get; set; }
        public DbSet<BidOffer> BidOffers { get; set; }
        public DbSet<ScheduledTour> ScheduledTours { get; set; }
        public DbSet<ResidentAccount> ResidentAccounts { get; set; }
        public DbSet<UnitReservation> UnitReservations { get; set; }
        public DbSet<PropertyExpenseAllocation> PropertyExpenseAllocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // max char length value in sqlserver
                    if (property.GetMaxLength() == 67108864)
                        // max char length value in postgresql
                        property.SetMaxLength(10485760);
                }
            }
        }

    }
}
