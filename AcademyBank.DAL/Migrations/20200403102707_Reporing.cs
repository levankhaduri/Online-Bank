using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcademyBank.DAL.Migrations
{
    public partial class Reporing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountersReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    DepositPage = table.Column<int>(nullable: false),
                    LoansPage = table.Column<int>(nullable: false),
                    CardsPage = table.Column<int>(nullable: false),
                    AccountPage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountersReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountersReports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiltersReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    DepositAmount = table.Column<decimal>(nullable: false),
                    DepositCurrency = table.Column<string>(nullable: true),
                    LoanAmount = table.Column<decimal>(nullable: false),
                    LoanPurpose = table.Column<string>(nullable: true),
                    LoanTerm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltersReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiltersReports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    CounterLogsIn = table.Column<int>(nullable: false),
                    FirstLogin = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    AvgPerday = table.Column<decimal>(nullable: false),
                    PerMonth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginReports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransfersReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Spent = table.Column<decimal>(nullable: false),
                    Received = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransfersReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransfersReports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountersReports_UserId",
                table: "CountersReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FiltersReports_UserId",
                table: "FiltersReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginReports_UserId",
                table: "LoginReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransfersReports_UserId",
                table: "TransfersReports",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountersReports");

            migrationBuilder.DropTable(
                name: "FiltersReports");

            migrationBuilder.DropTable(
                name: "LoginReports");

            migrationBuilder.DropTable(
                name: "TransfersReports");
        }
    }
}
