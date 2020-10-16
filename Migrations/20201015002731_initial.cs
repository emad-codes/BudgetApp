using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level0 = table.Column<string>(maxLength: 50, nullable: false),
                    Level1 = table.Column<string>(maxLength: 50, nullable: false),
                    Level2 = table.Column<string>(maxLength: 50, nullable: false),
                    Level3 = table.Column<string>(maxLength: 50, nullable: false),
                    Level4 = table.Column<string>(maxLength: 50, nullable: false),
                    Level5 = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<string>(maxLength: 4, nullable: false),
                    Status = table.Column<string>(maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    ManagerId = table.Column<int>(nullable: false),
                    ApproverId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetLevels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Alias" },
                values: new object[] { 100, "EKhan6" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Alias" },
                values: new object[] { 101, "DDavies" });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLevels_UserId",
                table: "BudgetLevels",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetLevels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
