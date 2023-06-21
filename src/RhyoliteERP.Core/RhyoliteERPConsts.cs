using RhyoliteERP.Debugging;

namespace RhyoliteERP
{
    public class RhyoliteERPConsts
    {
        public const string LocalizationSourceName = "RhyoliteERP";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "9eb20c2f05644af18947623902da8fc2";


        //Module Constants
        public const int GeneralLedger = 100;
        public const int Payroll = 200;
        public const int SchoolManager = 300;
        public const int StockManager = 400;
        public const int HRManager = 500;
        public const int AccountPayables = 600;
        public const int AccountReceivables = 700;
        public const int AssetManager = 800;
        public const int Banking = 900;
        public const int Audit = 9001;
        public const int BotApi = 1100;
        public const int AccountingApi = 1200;
        public const int PropertyRentals = 1300;

        //module descriptions
        public const string GeneralLedgerDescription = "Record, store and query accounting journal entries and generate financial statements.";
        public const string PayrollDescription = "Perform payroll calculations & maintain labour distribution.";
        public const string SchoolManagerDescription = "School Administration module for Primary, JHS and SHS schools.";
        public const string StockManagerDescription = "Multi-warehouse Inventory Control & analysis system for issuing and transfer of stock.";
        public const string HRManagerDescription = "";
        public const string AccountPayablesDescription = "Manage amount owed to suppliers, due rates and available discounts.";
        public const string AccountReceivablesDescription = "An enterprise grade module that enables efficient tracking of receivables.";
        public const string AuditDescription = "A solution for real time monitoring and auditing of organizational transactions.";
        public const string AssetManagerDescription = "Manage any asset owned by an organization such as vehicles, lands and bungalows.";
        public const string PropertyRentalModuleDescription = "Manage properties, tenants, automate leasing , renting & payments.";
         


        //report titles
        public const string StudentsByClass = "Students By Class";
        public const string StudentsByNationality = "Students By Nationality";
        public const string StudentsByReligion = "Students By Religion";
        public const string EnrollmentByGender = "Enrollment By Gender";
        public const string AttendanceSummary = "Attendance Summary";
        public const string StudentBalances = "Student Balances for";
        public const string PaidUpStudents = "Paidup Students for";
        public const string StudentDebtors = "Student Debtors for";
        public const string DailyPayments = "Payments made on";
        public const string CreditMemo = "Credit Memo Records for";
        public const string BillSetup = "Bill Setup for";
        public const string CancelledBills = "Cancelled Bills for";
        public const string CancelledPayments = "Cancelled Payments for";
        public const string Alumni = "Alumni for";
        public const string AlumniBalances = "Alumni Balances for";
        public const string TerminalSubjectResults = "Terminal Subject Results for";
        public const string SmsReports = "SMS reports for the period:";

        //pay types

    }



}
