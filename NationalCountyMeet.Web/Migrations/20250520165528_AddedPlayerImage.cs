using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlayerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerPhotoUrl",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerPhotoUrl",
                table: "Players");
        }
    }
}
