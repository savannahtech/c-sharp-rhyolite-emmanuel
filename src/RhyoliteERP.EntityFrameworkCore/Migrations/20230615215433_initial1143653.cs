using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial1143653 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ScheduledTours",
                newName: "PropertyUnitNo");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "ScheduledTours",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "ScheduledTours",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "ScheduledTours",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyUnitId",
                table: "ScheduledTours",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "BidOffers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "BidOffers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyUnitId",
                table: "BidOffers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PropertyUnitNo",
                table: "BidOffers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "ScheduledTours");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "ScheduledTours");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "ScheduledTours");

            migrationBuilder.DropColumn(
                name: "PropertyUnitId",
                table: "ScheduledTours");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "BidOffers");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "BidOffers");

            migrationBuilder.DropColumn(
                name: "PropertyUnitId",
                table: "BidOffers");

            migrationBuilder.DropColumn(
                name: "PropertyUnitNo",
                table: "BidOffers");

            migrationBuilder.RenameColumn(
                name: "PropertyUnitNo",
                table: "ScheduledTours",
                newName: "Name");
        }
    }
}
