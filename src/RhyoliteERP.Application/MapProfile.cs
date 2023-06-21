using AutoMapper;
using RhyoliteERP.Models.Banking;
using RhyoliteERP.Models.Ledger;
using RhyoliteERP.Models.Payroll;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Models.School;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Ledger.BankAccounts.Dto;
using RhyoliteERP.Services.Ledger.CoaControls.Dto;
using RhyoliteERP.Services.Ledger.CoaDetails.Dto;
using RhyoliteERP.Services.Ledger.CoaHierachies.Dto;
using RhyoliteERP.Services.Ledger.ImprestCategories.Dto;
using RhyoliteERP.Services.Ledger.PettyCashRecipients.Dto;
using RhyoliteERP.Services.Payroll.AllowanceTypes.Dto;
using RhyoliteERP.Services.Payroll.BikTypes.Dto;
using RhyoliteERP.Services.Payroll.BonusAndOnetimeAllowances.Dto;
using RhyoliteERP.Services.Payroll.DeductionTypes.Dto;
using RhyoliteERP.Services.Payroll.EmployeeAllowances.Dto;
using RhyoliteERP.Services.Payroll.EmployeeBenefitInKinds.Dto;
using RhyoliteERP.Services.Payroll.EmployeeBioDatas.Dto;
using RhyoliteERP.Services.Payroll.EmployeeCategories.Dto;
using RhyoliteERP.Services.Payroll.EmployeeDaysWorkeds.Dto;
using RhyoliteERP.Services.Payroll.EmployeeDeductions.Dto;
using RhyoliteERP.Services.Payroll.EmployeeLoans.Dto;
using RhyoliteERP.Services.Payroll.EmployeeOnetimeDeductions.Dto;
using RhyoliteERP.Services.Payroll.EmployeeRanks.Dto;
using RhyoliteERP.Services.Payroll.EmployeeReliefs.Dto;
using RhyoliteERP.Services.Payroll.EmployeeSalaryAdvances.Dto;
using RhyoliteERP.Services.Payroll.EmployeeSalaryInfos.Dto;
using RhyoliteERP.Services.Payroll.EmployeeSnits.Dto;
using RhyoliteERP.Services.Payroll.Gratuities.Dto;
using RhyoliteERP.Services.Payroll.InitializePayMonths.Dto;
using RhyoliteERP.Services.Payroll.IrsSignatures.Dto;
using RhyoliteERP.Services.Payroll.LoanTypes.Dto;
using RhyoliteERP.Services.Payroll.OvertimeTimeSheets.Dto;
using RhyoliteERP.Services.Payroll.OvertimeTypes.Dto;
using RhyoliteERP.Services.Payroll.PayCalendars.Dto;
using RhyoliteERP.Services.Payroll.SalaryGrades.Dto;
using RhyoliteERP.Services.Payroll.TaxReliefs.Dto;
using RhyoliteERP.Services.Payroll.TaxTables.Dto;
using RhyoliteERP.Services.PropertyRental.BidOffers.Dto;
using RhyoliteERP.Services.PropertyRental.LeaseApplicants.Dto;
using RhyoliteERP.Services.PropertyRental.LeasePayments.Dto;
using RhyoliteERP.Services.PropertyRental.Leases.Dto;
using RhyoliteERP.Services.PropertyRental.MeterTypes.Dto;
using RhyoliteERP.Services.PropertyRental.Properties.Dto;
using RhyoliteERP.Services.PropertyRental.PropertyExpenseAllocations.Dto;
using RhyoliteERP.Services.PropertyRental.PropertyGroups.Dto;
using RhyoliteERP.Services.PropertyRental.PropertyTypes.Dto;
using RhyoliteERP.Services.PropertyRental.PropertyUnits.Dto;
using RhyoliteERP.Services.PropertyRental.RentalNotificationSettings.Dto;
using RhyoliteERP.Services.PropertyRental.ResidentAccounts.Dto;
using RhyoliteERP.Services.PropertyRental.ScheduledTours.Dto;
using RhyoliteERP.Services.PropertyRental.UnitReservations.Dto;
using RhyoliteERP.Services.PropertyRental.VendorCategories.Dto;
using RhyoliteERP.Services.PropertyRental.Vendors.Dto;
using RhyoliteERP.Services.PropertyRental.WorkOrders.Dto;
using RhyoliteERP.Services.School.AcademicYears.Dto;
using RhyoliteERP.Services.School.Attitudes.Dto;
using RhyoliteERP.Services.School.BillPayments.Dto;
using RhyoliteERP.Services.School.BillSetups.Dto;
using RhyoliteERP.Services.School.BillTypes.Dto;
using RhyoliteERP.Services.School.ClassStreams.Dto;
using RhyoliteERP.Services.School.Conducts.Dto;
using RhyoliteERP.Services.School.FeesDescriptions.Dto;
using RhyoliteERP.Services.School.Levels.Dto;
using RhyoliteERP.Services.School.Parents.Dto;
using RhyoliteERP.Services.School.ResultsUploads.Dto;
using RhyoliteERP.Services.School.ResultTypes.Dto;
using RhyoliteERP.Services.School.SchClasses.Dto;
using RhyoliteERP.Services.School.SchoolProfile.Dto;
using RhyoliteERP.Services.School.SpecialDuties.Dto;
using RhyoliteERP.Services.School.StaffDesignations.Dto;
using RhyoliteERP.Services.School.Staffs.Dto;
using RhyoliteERP.Services.School.StudentAttendances.Dto;
using RhyoliteERP.Services.School.StudentBills.Dto;
using RhyoliteERP.Services.School.StudentParents.Dto;
using RhyoliteERP.Services.School.Students.Dto;
using RhyoliteERP.Services.School.StudentStatuses.Dto;
using RhyoliteERP.Services.School.SubjectRemarks.Dto;
using RhyoliteERP.Services.School.Subjects.Dto;
using RhyoliteERP.Services.School.TeacherRemarks.Dto;
using RhyoliteERP.Services.Shared.Banks.Dto;
using RhyoliteERP.Services.Shared.BusinessProfile.Dto;
using RhyoliteERP.Services.Shared.CostCenters.Dto;
using RhyoliteERP.Services.Shared.Countires.Dto;
using RhyoliteERP.Services.Shared.CountryStates.Dto;
using RhyoliteERP.Services.Shared.Cties.Dto;
using RhyoliteERP.Services.Shared.Currencies.Dto;
using RhyoliteERP.Services.Shared.CustomerGroups.Dto;
using RhyoliteERP.Services.Shared.Customers.Dto;
using RhyoliteERP.Services.Shared.Departments.Dto;
using RhyoliteERP.Services.Shared.MessageTemplates.Dto;
using RhyoliteERP.Services.Shared.Relationships.Dto;
using RhyoliteERP.Services.Shared.Religions.Dto;
using RhyoliteERP.Services.Shared.ReportDownloads.Dto;
using RhyoliteERP.Services.Shared.SmsHistories.Dto;
using RhyoliteERP.Services.Shared.SupplierGroups.Dto;
using RhyoliteERP.Services.Shared.Suppliers.Dto;
using RhyoliteERP.Services.Shared.SystemNumbers.Dto;

namespace RhyoliteERP
{
   public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreateSchoolProfileInput, SchoolProfile>();

            CreateMap<CreateReportDownloadInput, ReportDownload>();

            CreateMap<BillSetup, BillSetupInfo>();

            CreateMap<CreateAcademicYearInput, AcademicYear>();
            CreateMap<UpdateAcademicYearInput, AcademicYear>();

            CreateMap<CreateBillTypeInput, BillType>();
            CreateMap<UpdateBillTypeInput, BillType>();

            CreateMap<CreateClassStreamInput, ClassStream>();
            CreateMap<UpdateClassStreamInput, ClassStream>();

            CreateMap<CreateFeesDescriptionInput, FeesDescription>();
            CreateMap<UpdateFeesDescriptionInput, FeesDescription>();

            CreateMap<CreateLevelInput, Level>();
            CreateMap<UpdateLevelInput, Level>();

            CreateMap<CreateStudentStatusInput, StudentStatus>();
            CreateMap<UpdateStudentStatusInput, StudentStatus>();

            CreateMap<CreateSubjectInput, Subject>();
            CreateMap<UpdateSubjectInput, Subject>();

            CreateMap<CreateSubjectRemarkInput, SubjectRemark>();
            CreateMap<UpdateSubjectRemarkInput, SubjectRemark>();

            CreateMap<CreateSpecialDutyInput, SpecialDuty>();
            CreateMap<UpdateSpecialDutyInput, SpecialDuty>();

            CreateMap<CreateStaffDesignationInput, StaffDesignation>();
            CreateMap<UpdateStaffDesignationInput, StaffDesignation>();

            CreateMap<CreateResultTypeInput, ResultType>();
            CreateMap<UpdateResultTypeInput, ResultType>();

            CreateMap<CreateClassInput, SchClass>();
            CreateMap<UpdateClassInput, SchClass>();

            CreateMap<CreateBillSetupInput, BillSetup>();

            CreateMap<CreateStudentInput, Student>();
            CreateMap<UpdateStudentInput, Student>();

            CreateMap<CreateParentInput, Parent>();
            CreateMap<UpdateParentInput, Parent>();

            CreateMap<CreateStudentParentInput, StudentParent>();
            CreateMap<UpdateStudentParentInput, StudentParent>();

            CreateMap<CreateStudentAttendanceInput, StudentAttendance>();

            CreateMap<CreateResultsUploadInput, ResultsUpload>();
            CreateMap<UpdateResultsUploadInput, ResultsUpload>();

            CreateMap<CreateAttitudeInput, Attitude>();
            CreateMap<UpdateAttitudeInput, Attitude>();

            CreateMap<CreateConductInput, Conduct>();
            CreateMap<UpdateConductInput, Conduct>();

            CreateMap<CreateTeacherRemarkInput, TeacherRemark>();
            CreateMap<UpdateTeacherRemarkInput, TeacherRemark>();

            CreateMap<CreateStaffInput, Staff>();
            CreateMap<UpdateStaffInput, Staff>();

            CreateMap<CreateStudentBillInput, StudentBill>();
            CreateMap<CancelStudentBillInput, StudentBill>();

            CreateMap<CreateBillPaymentInput, BillPayment>();
            CreateMap<CancelBillPaymentInput, BillPayment>();
             
            CreateMap<CreateMessageTemplateInput, MessageTemplate>();
            CreateMap<UpdateMessageTemplateInput, MessageTemplate>();

            CreateMap<CreateSupplierGroupInput, SupplierGroup>();
            CreateMap<UpdateSupplierGroupInput, SupplierGroup>();

            CreateMap<CreateCustomerGroupInput, CustomerGroup>();
            CreateMap<UpdateCustomerGroupInput, CustomerGroup>();

            CreateMap<CreateCustomerInput, Customer>();
            CreateMap<UpdateCustomerInput, Customer>();

            CreateMap<CreateSupplierInput, Supplier>();
            CreateMap<UpdateSupplierInput, Supplier>();


            //shared
            CreateMap<SmsHistoryInput, SmsHistory>();

            CreateMap<CreateCurrencyInput, Currency>();
            CreateMap<UpdateCurrencyInput, Currency>();

            CreateMap<CreateRelationshipInput, Relationship>();
            CreateMap<UpdateRelationshipInput, Relationship>();

            CreateMap<CreateReligionInput, Religion>();
            CreateMap<UpdateReligionInput, Religion>();

            CreateMap<CreateSystemNumberInput, SystemNumber>();
            CreateMap<UpdateSystemNumberInput, SystemNumber>();

            CreateMap<CreateCostCenterInput, CostCenter>();
            CreateMap<UpdateCostCenterInput, CostCenter>();

            CreateMap<CreateDepartmentInput, Department>();
            CreateMap<UpdateDepartmentInput, Department>();

            CreateMap<CreateBankInput, Bank>();
            CreateMap<UpdateBankInput, Bank>();

            CreateMap<CreateCountryStateInput, CountryState>();
            CreateMap<UpdateCountryStateInput, CountryState>();

            CreateMap<CreateCityInput, City>();
            CreateMap<UpdateCityInput, City>();

            CreateMap<CreateBankAccountInput, BankAccount>();
            CreateMap<UpdateBankAccountInput, BankAccount>();


            CreateMap<CreateCountryInput, Country>();
            CreateMap<UpdateCountryInput, Country>();

            CreateMap<CreateBusinessProfileInput, CompanyProfile>();
            CreateMap<UpdateBusinessProfileInput, CompanyProfile>();


            //ledger

            CreateMap<CreateCoaControlInput, CoaControl>();
            CreateMap<UpdateCoaControlInput, CoaControl>();

            CreateMap<CreateCoaHierachyInput, CoaHierachy>();
            CreateMap<UpdateCoaHierachyInput, CoaHierachy>();

            CreateMap<CreateCoaDetailInput, CoaDetail>();
            CreateMap<UpdateCoaDetailInput, CoaDetail>();

            CreateMap<CreateImprestCategoryInput, ImprestCategory>();
            CreateMap<UpdateImprestCategoryInput, ImprestCategory>();

            CreateMap<CreatePettyCashRecipientInput, PettyCashRecipient>();
            CreateMap<UpdatePettyCashRecipientInput, PettyCashRecipient>();

            //payroll

            CreateMap<CreateAllowanceTypeInput, AllowanceType>();
            CreateMap<UpdateAllowanceTypeInput, AllowanceType>();

            CreateMap<CreateEmployeeCategoryInput, EmployeeCategory>();
            CreateMap<UpdateEmployeeCategoryInput, EmployeeCategory>();

            CreateMap<CreateBikTypeInput, BikType>();
            CreateMap<UpdateBikTypeInput, BikType>();

            CreateMap<CreateDeductionTypeInput, DeductionType>();
            CreateMap<UpdateDeductionTypeInput, DeductionType>();

            CreateMap<CreateOvertimeTypeInput, OvertimeType>();
            CreateMap<UpdateOvertimeTypeInput, OvertimeType>();

            CreateMap<CreateEmployeeRankInput, EmployeeRank>();
            CreateMap<UpdateEmployeeRankInput, EmployeeRank>();

            CreateMap<CreateTaxTableInput, TaxTable>();
            CreateMap<UpdateTaxTableInput, TaxTable>();

            CreateMap<CreateGratuityInput, Gratuity>();
            CreateMap<UpdateGratuityInput, Gratuity>();

            CreateMap<CreateLoanTypeInput, LoanType>();
            CreateMap<UpdateLoanTypeInput, LoanType>();

            CreateMap<CreatePayCalendarInput, PayCalendar>();
            CreateMap<UpdatePayCalendarInput, PayCalendar>();

            CreateMap<CreateSalaryGradeInput, SalaryGrade>();
            CreateMap<UpdateSalaryGradeInput, SalaryGrade>();

            CreateMap<CreateTaxReliefInput, TaxRelief>();
            CreateMap<UpdateTaxReliefInput, TaxRelief>();

            CreateMap<CreateInitializePayMonthInput, InitializePayMonth>();

            CreateMap<CreateEmployeeBioDataInput, EmployeeBioData>();
            CreateMap<UpdateEmployeeBioDataInput, EmployeeBioData>();

            CreateMap<CreateEmployeeSalaryInfoInput, EmployeeSalaryInfo>();
            CreateMap<UpdateEmployeeSalaryInfoInput, EmployeeSalaryInfo>();

            CreateMap<CreateEmployeeDaysWorkedInput, EmployeeDaysWorked>();
            CreateMap<UpdateEmployeeDaysWorkedInput, EmployeeDaysWorked>();

            CreateMap<CreateBonusAndOnetimeAllowanceInput, BonusAndOnetimeAllowance>();
            CreateMap<UpdateBonusAndOnetimeAllowanceInput, BonusAndOnetimeAllowance>();

            CreateMap<CreateEmployeeSnitInput, EmployeeSnit>();
            CreateMap<UpdateEmployeeSnitInput, EmployeeSnit>();

            CreateMap<CreateEmployeeDeductionInput, EmployeeDeduction>();
            CreateMap<UpdateEmployeeDeductionInput, EmployeeDeduction>();

            CreateMap<CreateEmployeeAllowanceInput, EmployeeAllowance>();
            CreateMap<UpdateEmployeeAllowanceInput, EmployeeAllowance>();

            CreateMap<CreateEmployeeBenefitInKindInput, EmployeeBenefitInKind>();
            CreateMap<UpdateEmployeeBenefitInKindInput, EmployeeBenefitInKind>();

            CreateMap<CreateEmployeeLoanInput, EmployeeLoan>();

            CreateMap<CreateEmployeeSalaryAdvanceInput, EmployeeSalaryAdvance>();

            CreateMap<CreateEmployeeOnetimeDeductionInput, EmployeeOnetimeDeduction>();
            CreateMap<UpdateEmployeeOnetimeDeductionInput, EmployeeOnetimeDeduction>();

            CreateMap<CreateEmployeeReliefInput, EmployeeRelief>();
            CreateMap<UpdateEmployeeReliefInput, EmployeeRelief>();

            CreateMap<CreateOvertimeTimeSheetInput, OvertimeTimeSheet>();
            CreateMap<UpdateOvertimeTimeSheetInput, OvertimeTimeSheet>();

            CreateMap<CreateIrsSignatureInput, IrsSignature>();

            //property rental...
            CreateMap<CreateLeasePaymentInput, LeasePayment>();

            CreateMap<CreateLeaseInput, Lease>();
            CreateMap<UpdateLeaseInput, Lease>();

            CreateMap<CreateMeterTypeInput, MeterType>();
            CreateMap<UpdateMeterTypeInput, MeterType>();

            CreateMap<CreatePropertyExpenseAllocationInput, PropertyExpenseAllocation>();
            CreateMap<UpdatePropertyExpenseAllocationInput, PropertyExpenseAllocation>();

            CreateMap<CreatePropertyUnitInput, PropertyUnit>();
            CreateMap<UpdatePropertyUnitInput, PropertyUnit>();

            CreateMap<CreatePropertyTypeInput, PropertyType>();
            CreateMap<UpdatePropertyTypeInput, PropertyType>();

            CreateMap<CreatePropertyGroupInput, PropertyGroup>();
            CreateMap<UpdatePropertyGroupInput, PropertyGroup>();

            CreateMap<CreatePropertyInput, Property>();
            CreateMap<UpdatePropertyInput, Property>();
             

            CreateMap<CreateLeaseApplicantInput, LeaseApplicant>();

            CreateMap<CreateVendorCategoryInput, VendorCategory>();
            CreateMap<UpdateVendorCategoryInput, VendorCategory>();

            CreateMap<CreateVendorInput, Vendor>();
            CreateMap<UpdateVendorInput, Vendor>();

            CreateMap<CreateWorkOrderInput, WorkOrder>();
            CreateMap<UpdateWorkOrderInput, WorkOrder>();

            CreateMap<CreateRentalNotificationSettingInput, RentalNotificationSetting>();
            CreateMap<UpdateRentalNotificationSettingInput, RentalNotificationSetting>();

            CreateMap<CreateBidOfferInput, BidOffer>();

            CreateMap<CreateScheduledTourInput, ScheduledTour>();

            CreateMap<CreateResidentAccountInput, ResidentAccount>();

            CreateMap<CreateUnitReservationInput, UnitReservation>();

            //banking

            //CreateMap<CreateAccountInput, SavingsAccount>();
            //CreateMap<UpdateAccountInput, SavingsAccount>();





        }
    }
}
