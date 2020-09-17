using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class RejiggedEntityConfigurations : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Workout",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "Exercise",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "DB_Exercises",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Workout",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "Exercise",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "DB_Exercises",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DB_Exercises_Workout_WorkoutId",
                table: "DB_Exercises",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Workout_WorkoutId",
                table: "Exercise",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_DB_Users_UserId",
                table: "Workout",
                column: "UserId",
                principalTable: "DB_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
