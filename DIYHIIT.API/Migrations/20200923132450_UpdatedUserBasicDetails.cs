using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class UpdatedUserBasicDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "DB_Users");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "DB_Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "DB_Users",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "DB_Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "DB_Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "DB_Users");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DB_Users");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "DB_Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
