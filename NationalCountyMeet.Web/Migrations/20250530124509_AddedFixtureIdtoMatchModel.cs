using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedFixtureIdtoMatchModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamTwoScore",
                table: "Matches",
                newName: "HomeTeamScore");

            migrationBuilder.RenameColumn(
                name: "TeamOneScore",
                table: "Matches",
                newName: "FixtureId");

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamScore",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FixtureId",
                table: "Matches",
                column: "FixtureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Fixtures_FixtureId",
                table: "Matches",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "FixtureId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Fixtures_FixtureId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_FixtureId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "AwayTeamScore",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "HomeTeamScore",
                table: "Matches",
                newName: "TeamTwoScore");

            migrationBuilder.RenameColumn(
                name: "FixtureId",
                table: "Matches",
                newName: "TeamOneScore");
        }
    }
}
