using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCountIdFromPlayerStatsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Counties_CountyId",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_CountyId",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "PlayerStatistics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                table: "PlayerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_CountyId",
                table: "PlayerStatistics",
                column: "CountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Counties_CountyId",
                table: "PlayerStatistics",
                column: "CountyId",
                principalTable: "Counties",
                principalColumn: "CountyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
