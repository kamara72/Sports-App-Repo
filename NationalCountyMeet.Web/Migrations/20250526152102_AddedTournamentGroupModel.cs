using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedTournamentGroupModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupAlias",
                table: "TournamentGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupAlias",
                table: "TournamentGroups");
        }
    }
}
