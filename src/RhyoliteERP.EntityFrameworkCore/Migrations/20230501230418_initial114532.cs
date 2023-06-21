using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial114532 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                table: "EmployeeDaysWorkeds",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeDepartment",
                table: "EmployeeDaysWorkeds",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "EmployeeDaysWorkeds");

            migrationBuilder.DropColumn(
                name: "EmployeeDepartment",
                table: "EmployeeDaysWorkeds");
        }
    }
}
