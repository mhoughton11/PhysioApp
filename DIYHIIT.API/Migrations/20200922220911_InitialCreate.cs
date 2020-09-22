using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DB_Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    BodyFocus = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    Duration = table.Column<double>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DB_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DB_Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DB_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DB_Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    BodyFocus = table.Column<string>(nullable: true),
                    ExerciseCount = table.Column<string>(nullable: true),
                    RestInterval = table.Column<double>(nullable: true),
                    ActiveInterval = table.Column<double>(nullable: true),
                    Effort = table.Column<double>(nullable: true),
                    Duration = table.Column<double>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    DateUsed = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ExerciseIds = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    BackgroundImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DB_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DB_Workouts_DB_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "DB_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true),
                    FeedType = table.Column<int>(nullable: false),
                    BackgroundColour = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    WorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItems_DB_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "DB_Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DB_Workouts_UserId",
                table: "DB_Workouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItems_WorkoutId",
                table: "FeedItems",
                column: "WorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DB_Exercises");

            migrationBuilder.DropTable(
                name: "FeedItems");

            migrationBuilder.DropTable(
                name: "DB_Workouts");

            migrationBuilder.DropTable(
                name: "DB_Users");
        }
    }
}
