using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class EditedTeamOfficial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamOfficials_CountyId",
                table: "TeamOfficials");

            migrationBuilder.CreateIndex(
                name: "IX_TeamOfficials_CountyId",
                table: "TeamOfficials",
                column: "CountyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamOfficials_CountyId",
                table: "TeamOfficials");

            migrationBuilder.CreateIndex(
                name: "IX_TeamOfficials_CountyId",
                table: "TeamOfficials",
                column: "CountyId",
                unique: true);
        }
    }
}
