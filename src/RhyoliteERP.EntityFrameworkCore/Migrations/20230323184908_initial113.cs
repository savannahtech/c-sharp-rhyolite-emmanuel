using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhyoliteERP.Migrations
{
    public partial class initial113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taxable",
                table: "DeductionTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Taxable",
                table: "DeductionTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
