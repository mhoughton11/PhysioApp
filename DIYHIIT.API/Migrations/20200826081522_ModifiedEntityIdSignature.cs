using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class ModifiedEntityIdSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutID",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Workouts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "WorkoutID",
                table: "Exercises",
                newName: "WorkoutId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Exercises",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutID",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workouts",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "WorkoutId",
                table: "Exercises",
                newName: "WorkoutID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Exercises",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutID",
                table: "Exercises",
                column: "WorkoutID",
                principalTable: "Workouts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
