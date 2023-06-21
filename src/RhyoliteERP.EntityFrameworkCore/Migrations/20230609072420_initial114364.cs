using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial114364 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PropertyUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "ImageList",
                table: "PropertyUnits",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "PropertyUnits",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "PropertyUnits",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PropertyUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "YearBuilt",
                table: "PropertyUnits",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "ImageList",
                table: "Properties",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "Properties",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "YearBuilt",
                table: "Properties",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "LeaseTenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantIdentifier",
                table: "LeaseTenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OvertimePercentageOnBasicSalary",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OvertimePercentageOnDailyWage",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PaySecondProvidentOrOthers",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondProvidentEmployeeRate",
                table: "CompanyProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondProvidentEmployerRate",
                table: "CompanyProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "SecondProvidentTaxEmployerPortion",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TreatSecondProvidentAsSSF",
                table: "CompanyProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "ImageList",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "YearBuilt",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ImageList",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "YearBuilt",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "TenantIdentifier",
                table: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "OvertimePercentageOnBasicSalary",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "OvertimePercentageOnDailyWage",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "PaySecondProvidentOrOthers",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "SecondProvidentEmployeeRate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "SecondProvidentEmployerRate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "SecondProvidentTaxEmployerPortion",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "TreatSecondProvidentAsSSF",
                table: "CompanyProfiles");
        }
    }
}
