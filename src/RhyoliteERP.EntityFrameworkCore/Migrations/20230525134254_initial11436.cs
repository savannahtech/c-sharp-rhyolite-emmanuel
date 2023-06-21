using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using RhyoliteERP.Models.PropertyRental;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial11436 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Leases");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Properties",
                newName: "PropertyManagerEmail");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Leases",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Frequency",
                table: "Leases",
                newName: "ChargeMemo");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Leases",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "ChargeDueDate",
                table: "Leases",
                newName: "ChargeNextDueDate");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Leases",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<List<MeterReading>>(
                name: "MeterReadings",
                table: "PropertyUnits",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChargeFrequency",
                table: "Leases",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<RentCharge>>(
                name: "RentCharges",
                table: "Leases",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeterReadings",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "ChargeFrequency",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "RentCharges",
                table: "Leases");

            migrationBuilder.RenameColumn(
                name: "PropertyManagerEmail",
                table: "Properties",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Leases",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Leases",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Leases",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "ChargeNextDueDate",
                table: "Leases",
                newName: "ChargeDueDate");

            migrationBuilder.RenameColumn(
                name: "ChargeMemo",
                table: "Leases",
                newName: "Frequency");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Leases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
