using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ExternalClickColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExternalClick",
                table: "UserBookmarkClicks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalClick",
                table: "UserBookmarkClicks");
        }
    }
}
