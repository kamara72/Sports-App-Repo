using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalCountyMeet.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedVenuePhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VenuePhotoUrl",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenuePhotoUrl",
                table: "Venues");
        }
    }
}
