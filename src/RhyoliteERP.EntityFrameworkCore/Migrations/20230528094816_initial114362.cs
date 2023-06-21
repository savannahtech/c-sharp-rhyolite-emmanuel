using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using RhyoliteERP.Models.PropertyRental;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial114362 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amenities",
                table: "PropertyUnits");

            migrationBuilder.AddColumn<int>(
                name: "Baths",
                table: "PropertyUnits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "PropertyUnits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<UnitAmenity>>(
                name: "UnitAmenities",
                table: "PropertyUnits",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Baths",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "UnitAmenities",
                table: "PropertyUnits");

            migrationBuilder.AddColumn<string>(
                name: "Amenities",
                table: "PropertyUnits",
                type: "text",
                nullable: true);
        }
    }
}
