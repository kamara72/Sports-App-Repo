using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class RenameOfficalPhotoProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerPhotoUrl",
                table: "MatchOfficials",
                newName: "MatchOfficialPhotoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchOfficialPhotoUrl",
                table: "MatchOfficials",
                newName: "PlayerPhotoUrl");
        }
    }
}
