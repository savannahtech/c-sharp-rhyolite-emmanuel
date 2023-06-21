using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial11451 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "SalaryIncrementHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "SalaryIncrementHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "Paymasters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "Paymasters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "PaymasterHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "PaymasterHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlySsnitDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlySsnitDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlySsnitDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlySsnitDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlySecPfDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlySecPfDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlySecPfDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlySecPfDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlySalaryAdvances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlySalaryAdvances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlySalaryAdvanceHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlySalaryAdvanceHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyReliefs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyReliefs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyReliefHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyReliefHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyPfDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyPfDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyPfDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyPfDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyOvertimes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyOvertimes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyOvertimeHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyOvertimeHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyOnetimeDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyOnetimeDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyOnetimeDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyOnetimeDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyLoanDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyLoanDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyLoanDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyLoanDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyIrsTaxHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyIrsTaxHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyIrsTaxes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyIrsTaxes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyCumulativeDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyCumulativeDeductions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyCumulativeDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyCumulativeDeductionHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyBonuses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyBonuses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyBenefitsInKinds",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyBenefitsInKinds",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyBenefitsInKindHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyBenefitsInKindHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyAllowanceHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyAllowanceHistory",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "SalaryIncrementHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "SalaryIncrementHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "Paymasters");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "Paymasters");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "PaymasterHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "PaymasterHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlySsnitDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlySsnitDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlySsnitDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlySsnitDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlySecPfDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlySecPfDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlySecPfDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlySecPfDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlySalaryAdvances");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlySalaryAdvances");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlySalaryAdvanceHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlySalaryAdvanceHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyReliefs");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyReliefs");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyReliefHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyReliefHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyPfDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyPfDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyPfDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyPfDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyOvertimes");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyOvertimes");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyOvertimeHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyOvertimeHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyOnetimeDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyOnetimeDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyOnetimeDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyOnetimeDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyLoanDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyLoanDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyLoanDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyLoanDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyIrsTaxHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyIrsTaxHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyIrsTaxes");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyIrsTaxes");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyCumulativeDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyCumulativeDeductions");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyCumulativeDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyCumulativeDeductionHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyBonuses");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyBonuses");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyBenefitsInKinds");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyBenefitsInKinds");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyBenefitsInKindHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyBenefitsInKindHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyAllowanceHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyAllowanceHistory");
        }
    }
}
