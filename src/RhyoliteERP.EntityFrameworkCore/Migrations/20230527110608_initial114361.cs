using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial114361 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Baths",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "PropertyUnits");

            migrationBuilder.AddColumn<string>(
                name: "LedgerAccountName",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LeaseTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PrimaryPhoneNo = table.Column<string>(type: "text", nullable: true),
                    SeconadaryPhoneNo = table.Column<string>(type: "text", nullable: true),
                    PrimaryEmail = table.Column<string>(type: "text", nullable: true),
                    SeconadaryEmail = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TaxIdentificationNo = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactName = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactPhoneNo = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactEmail = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactRelationshipToTenant = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: true),
                    RegionOrState = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseTenants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "LedgerAccountName",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "Baths",
                table: "PropertyUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rooms",
                table: "PropertyUnits",
                type: "text",
                nullable: true);
        }
    }
}
