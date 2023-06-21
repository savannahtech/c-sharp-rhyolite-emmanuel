using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial1143656 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LeasedPropertyId",
                table: "ResidentAccounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LeasedPropertyName",
                table: "ResidentAccounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LeasedPropertyUnitId",
                table: "ResidentAccounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LeasedPropertyUnitNo",
                table: "ResidentAccounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResidentAccountName",
                table: "LeasePayments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RevenueAccountId",
                table: "LeasePayments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeasedPropertyId",
                table: "ResidentAccounts");

            migrationBuilder.DropColumn(
                name: "LeasedPropertyName",
                table: "ResidentAccounts");

            migrationBuilder.DropColumn(
                name: "LeasedPropertyUnitId",
                table: "ResidentAccounts");

            migrationBuilder.DropColumn(
                name: "LeasedPropertyUnitNo",
                table: "ResidentAccounts");

            migrationBuilder.DropColumn(
                name: "ResidentAccountName",
                table: "LeasePayments");

            migrationBuilder.DropColumn(
                name: "RevenueAccountId",
                table: "LeasePayments");
        }
    }
}
