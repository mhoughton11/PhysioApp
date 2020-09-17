using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class UpdatedSchemaForUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Workouts_DB_WorkoutId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Users_DB_UserId",
                table: "Workout");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workout_DB_UserId",
                table: "Workout");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_DB_WorkoutId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "DB_UserId",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "DB_WorkoutId",
                table: "Exercise");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Workout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workout_UserId",
                table: "Workout",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Users_UserId",
                table: "Workout",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Users_UserId",
                table: "Workout");

            migrationBuilder.DropIndex(
                name: "IX_Workout_UserId",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workout");

            migrationBuilder.AddColumn<int>(
                name: "DB_UserId",
                table: "Workout",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DB_WorkoutId",
                table: "Exercise",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveInterval = table.Column<double>(type: "float", nullable: true),
                    BodyFocus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUsed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<double>(type: "float", nullable: true),
                    Effort = table.Column<double>(type: "float", nullable: true),
                    ExerciseCount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestInterval = table.Column<double>(type: "float", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workout_DB_UserId",
                table: "Workout",
                column: "DB_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_DB_WorkoutId",
                table: "Exercise",
                column: "DB_WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Workouts_DB_WorkoutId",
                table: "Exercise",
                column: "DB_WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Users_DB_UserId",
                table: "Workout",
                column: "DB_UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
