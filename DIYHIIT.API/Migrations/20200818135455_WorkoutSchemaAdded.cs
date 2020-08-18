using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class WorkoutSchemaAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColour",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutID",
                table: "Exercises",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ExercisesString = table.Column<string>(nullable: true),
                    RestInterval = table.Column<double>(nullable: false),
                    ActiveInterval = table.Column<double>(nullable: false),
                    Effort = table.Column<double>(nullable: false),
                    BodyFocus = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateUsed = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutID",
                table: "Exercises",
                column: "WorkoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutID",
                table: "Exercises",
                column: "WorkoutID",
                principalTable: "Workouts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutID",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_WorkoutID",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "WorkoutID",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColour",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
