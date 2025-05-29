using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountyFKtoMatchOfficalModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MatchOfficials_CountyId",
                table: "MatchOfficials",
                column: "CountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchOfficials_Counties_CountyId",
                table: "MatchOfficials",
                column: "CountyId",
                principalTable: "Counties",
                principalColumn: "CountyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchOfficials_Counties_CountyId",
                table: "MatchOfficials");

            migrationBuilder.DropIndex(
                name: "IX_MatchOfficials_CountyId",
                table: "MatchOfficials");
        }
    }
}
