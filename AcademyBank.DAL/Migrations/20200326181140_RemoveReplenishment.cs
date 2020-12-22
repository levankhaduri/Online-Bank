using Microsoft.EntityFrameworkCore.Migrations;

namespace AcademyBank.DAL.Migrations
{
    public partial class RemoveReplenishment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Replanishment",
                table: "Deposits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Replanishment",
                table: "Deposits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
