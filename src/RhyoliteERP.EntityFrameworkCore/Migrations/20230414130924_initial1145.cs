using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial1145 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModuleSource",
                table: "SmsHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "MonthlyAllowances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "MonthlyAllowances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployerSocialSecurityNo",
                table: "CompanyProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PayProvidentFund",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PaySocialSecurityFund",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ProvidentFundEmployeeRate",
                table: "CompanyProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProvidentFundEmployerRate",
                table: "CompanyProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ProvidentFundTaxEmployerPortion",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProvidentFundTreatAsSocialSecurityFund",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SocialSecurityFundEmployeeRate",
                table: "CompanyProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SocialSecurityFundEmployerRate",
                table: "CompanyProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "VATNo",
                table: "CompanyProfiles",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleSource",
                table: "SmsHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "MonthlyAllowances");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "MonthlyAllowances");

            migrationBuilder.DropColumn(
                name: "EmployerSocialSecurityNo",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "PayProvidentFund",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "PaySocialSecurityFund",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "ProvidentFundEmployeeRate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "ProvidentFundEmployerRate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "ProvidentFundTaxEmployerPortion",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "ProvidentFundTreatAsSocialSecurityFund",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "SocialSecurityFundEmployeeRate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "SocialSecurityFundEmployerRate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "VATNo",
                table: "CompanyProfiles");
        }
    }
}
