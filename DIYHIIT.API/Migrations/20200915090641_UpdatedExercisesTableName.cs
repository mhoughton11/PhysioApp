using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class UpdatedExercisesTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workout_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "DB_Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutId",
                table: "DB_Exercises",
                newName: "IX_DB_Exercises_WorkoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DB_Exercises",
                table: "DB_Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DB_Exercises_Workout_WorkoutId",
                table: "DB_Exercises",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DB_Exercises_Workout_WorkoutId",
                table: "DB_Exercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DB_Exercises",
                table: "DB_Exercises");

            migrationBuilder.RenameTable(
                name: "DB_Exercises",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_DB_Exercises_WorkoutId",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workout_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
