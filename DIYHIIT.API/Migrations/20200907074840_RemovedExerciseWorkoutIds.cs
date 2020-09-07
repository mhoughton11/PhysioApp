using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class RemovedExerciseWorkoutIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseCatalog_WorkoutCatalog_WorkoutId",
                table: "ExerciseCatalog");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseCatalog_WorkoutId",
                table: "ExerciseCatalog");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "ExerciseCatalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "ExerciseCatalog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseCatalog_WorkoutId",
                table: "ExerciseCatalog",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseCatalog_WorkoutCatalog_WorkoutId",
                table: "ExerciseCatalog",
                column: "WorkoutId",
                principalTable: "WorkoutCatalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
