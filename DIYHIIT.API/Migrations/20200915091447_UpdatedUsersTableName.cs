using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class UpdatedUsersTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Users_UserId",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "DB_Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DB_Users",
                table: "DB_Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_DB_Users_UserId",
                table: "Workout",
                column: "UserId",
                principalTable: "DB_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_DB_Users_UserId",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DB_Users",
                table: "DB_Users");

            migrationBuilder.RenameTable(
                name: "DB_Users",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Users_UserId",
                table: "Workout",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
