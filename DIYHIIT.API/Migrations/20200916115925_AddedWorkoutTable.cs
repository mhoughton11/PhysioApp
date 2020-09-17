using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class AddedWorkoutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DB_Exercises_Workout_WorkoutId",
                table: "DB_Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Workout_WorkoutId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_DB_Users_UserId",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workout",
                table: "Workout");

            migrationBuilder.RenameTable(
                name: "Workout",
                newName: "DB_Workouts");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_UserId",
                table: "DB_Workouts",
                newName: "IX_DB_Workouts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DB_Workouts",
                table: "DB_Workouts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DB_Exercises_DB_Workouts_WorkoutId",
                table: "DB_Exercises",
                column: "WorkoutId",
                principalTable: "DB_Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DB_Workouts_DB_Users_UserId",
                table: "DB_Workouts",
                column: "UserId",
                principalTable: "DB_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_DB_Workouts_WorkoutId",
                table: "Exercise",
                column: "WorkoutId",
                principalTable: "DB_Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DB_Exercises_DB_Workouts_WorkoutId",
                table: "DB_Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_DB_Workouts_DB_Users_UserId",
                table: "DB_Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_DB_Workouts_WorkoutId",
                table: "Exercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DB_Workouts",
                table: "DB_Workouts");

            migrationBuilder.RenameTable(
                name: "DB_Workouts",
                newName: "Workout");

            migrationBuilder.RenameIndex(
                name: "IX_DB_Workouts_UserId",
                table: "Workout",
                newName: "IX_Workout_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workout",
                table: "Workout",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DB_Exercises_Workout_WorkoutId",
                table: "DB_Exercises",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Workout_WorkoutId",
                table: "Exercise",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_DB_Users_UserId",
                table: "Workout",
                column: "UserId",
                principalTable: "DB_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
