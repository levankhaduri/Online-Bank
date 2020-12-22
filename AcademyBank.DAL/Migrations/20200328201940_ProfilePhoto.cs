using Microsoft.EntityFrameworkCore.Migrations;

namespace AcademyBank.DAL.Migrations
{
    public partial class ProfilePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "UserInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "UserInfos");
        }
    }
}
