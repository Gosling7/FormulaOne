using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaOne.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EfCoreModelCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverStandings_Teams_TeamId",
                table: "DriverStandings");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceResults_Teams_TeamId",
                table: "RaceResults");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamStandings_Teams_TeamId",
                table: "TeamStandings");

            migrationBuilder.DropIndex(
                name: "IX_TeamStandings_TeamId",
                table: "TeamStandings");

            migrationBuilder.CreateTable(
                name: "TeamDao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamDao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamStandingDao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Points = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStandingDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStandingDao_TeamDao_TeamId",
                        column: x => x.TeamId,
                        principalTable: "TeamDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStandingDao_TeamId",
                table: "TeamStandingDao",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverStandings_TeamDao_TeamId",
                table: "DriverStandings",
                column: "TeamId",
                principalTable: "TeamDao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceResults_TeamDao_TeamId",
                table: "RaceResults",
                column: "TeamId",
                principalTable: "TeamDao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverStandings_TeamDao_TeamId",
                table: "DriverStandings");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceResults_TeamDao_TeamId",
                table: "RaceResults");

            migrationBuilder.DropTable(
                name: "TeamStandingDao");

            migrationBuilder.DropTable(
                name: "TeamDao");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStandings_TeamId",
                table: "TeamStandings",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverStandings_Teams_TeamId",
                table: "DriverStandings",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceResults_Teams_TeamId",
                table: "RaceResults",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamStandings_Teams_TeamId",
                table: "TeamStandings",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
