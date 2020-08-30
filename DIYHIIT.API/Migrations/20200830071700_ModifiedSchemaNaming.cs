using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class ModifiedSchemaNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "Workouts",
                newName: "WorkoutCatalog");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "ExerciseCatalog");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutId",
                table: "ExerciseCatalog",
                newName: "IX_ExerciseCatalog_WorkoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutCatalog",
                table: "WorkoutCatalog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseCatalog",
                table: "ExerciseCatalog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseCatalog_WorkoutCatalog_WorkoutId",
                table: "ExerciseCatalog",
                column: "WorkoutId",
                principalTable: "WorkoutCatalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseCatalog_WorkoutCatalog_WorkoutId",
                table: "ExerciseCatalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutCatalog",
                table: "WorkoutCatalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseCatalog",
                table: "ExerciseCatalog");

            migrationBuilder.RenameTable(
                name: "WorkoutCatalog",
                newName: "Workouts");

            migrationBuilder.RenameTable(
                name: "ExerciseCatalog",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseCatalog_WorkoutId",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
