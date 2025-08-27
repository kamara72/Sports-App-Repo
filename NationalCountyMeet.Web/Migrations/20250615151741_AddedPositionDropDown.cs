using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedPositionDropDown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentOfficials_Positions_PositionId",
                table: "TournamentOfficials");

            migrationBuilder.DropIndex(
                name: "IX_TournamentOfficials_PositionId",
                table: "TournamentOfficials");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "TournamentOfficials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "TournamentOfficials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentOfficials_PositionId",
                table: "TournamentOfficials",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentOfficials_Positions_PositionId",
                table: "TournamentOfficials",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "PositionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
