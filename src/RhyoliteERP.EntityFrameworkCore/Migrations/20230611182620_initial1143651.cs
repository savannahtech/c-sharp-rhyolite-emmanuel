using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using RhyoliteERP.Models.PropertyRental;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial1143651 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendSmsLowBalanceLimitAlert",
                table: "RentalNotificationSettings");

            migrationBuilder.AddColumn<List<string>>(
                name: "Elevations",
                table: "Properties",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<List<UnitAmenity>>(
                name: "InteriorDetails",
                table: "Properties",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<List<UnitAmenity>>(
                name: "OutdoorDetails",
                table: "Properties",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "ThreeDimensionPlans",
                table: "Properties",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "TwoDimensionPlans",
                table: "Properties",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<List<UnitAmenity>>(
                name: "UnitAmenities",
                table: "Properties",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<List<UnitAmenity>>(
                name: "Utilities",
                table: "Properties",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elevations",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "InteriorDetails",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OutdoorDetails",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ThreeDimensionPlans",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "TwoDimensionPlans",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UnitAmenities",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Utilities",
                table: "Properties");

            migrationBuilder.AddColumn<decimal>(
                name: "SendSmsLowBalanceLimitAlert",
                table: "RentalNotificationSettings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
