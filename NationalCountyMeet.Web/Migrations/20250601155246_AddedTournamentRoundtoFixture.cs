using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedTournamentRoundtoFixture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TournamentRounds_TournamentRoundId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TournamentRoundId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TournamentRoundId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "TournamentRoundId",
                table: "Fixtures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_TournamentRoundId",
                table: "Fixtures",
                column: "TournamentRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_TournamentRounds_TournamentRoundId",
                table: "Fixtures",
                column: "TournamentRoundId",
                principalTable: "TournamentRounds",
                principalColumn: "TournamentRoundId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_TournamentRounds_TournamentRoundId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_TournamentRoundId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "TournamentRoundId",
                table: "Fixtures");

            migrationBuilder.AddColumn<int>(
                name: "TournamentRoundId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentRoundId",
                table: "Matches",
                column: "TournamentRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TournamentRounds_TournamentRoundId",
                table: "Matches",
                column: "TournamentRoundId",
                principalTable: "TournamentRounds",
                principalColumn: "TournamentRoundId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
