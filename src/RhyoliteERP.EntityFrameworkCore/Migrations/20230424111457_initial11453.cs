using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial11453 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeContribution",
                table: "Paymasters");

            migrationBuilder.DropColumn(
                name: "EmployerContribution",
                table: "Paymasters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EmployeeContribution",
                table: "Paymasters",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EmployerContribution",
                table: "Paymasters",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
