using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial114363 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeconadaryPhoneNo",
                table: "LeaseTenants",
                newName: "SecondaryPhoneNo");

            migrationBuilder.RenameColumn(
                name: "SeconadaryEmail",
                table: "LeaseTenants",
                newName: "SecondaryEmail");

            migrationBuilder.AddColumn<Guid>(
                name: "LeasedPropertyId",
                table: "LeaseTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LeasedPropertyName",
                table: "LeaseTenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LeasedPropertyUnitId",
                table: "LeaseTenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LeasedPropertyUnitNo",
                table: "LeaseTenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyUnitId",
                table: "Leases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PropertyUnitName",
                table: "Leases",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeasedPropertyId",
                table: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "LeasedPropertyName",
                table: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "LeasedPropertyUnitId",
                table: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "LeasedPropertyUnitNo",
                table: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "PropertyUnitId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "PropertyUnitName",
                table: "Leases");

            migrationBuilder.RenameColumn(
                name: "SecondaryPhoneNo",
                table: "LeaseTenants",
                newName: "SeconadaryPhoneNo");

            migrationBuilder.RenameColumn(
                name: "SecondaryEmail",
                table: "LeaseTenants",
                newName: "SeconadaryEmail");
        }
    }
}
