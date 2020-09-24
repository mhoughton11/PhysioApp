using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DIYHIIT.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Height = table.Column<double>(nullable: true),
                    Weight = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserKey);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    WorkoutKey = table.Column<int>(nullable: false)
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
                    UserKey = table.Column<int>(nullable: false),
                    BackgroundImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.WorkoutKey);
                    table.ForeignKey(
                        name: "FK_Workouts_Users_UserKey",
                        column: x => x.UserKey,
                        principalTable: "Users",
                        principalColumn: "UserKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(nullable: true),
                    UserKey = table.Column<int>(nullable: false),
                    DOE = table.Column<DateTime>(nullable: false),
                    AuditWorkoutWorkoutKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTrails_Workouts_AuditWorkoutWorkoutKey",
                        column: x => x.AuditWorkoutWorkoutKey,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditTrails_Users_UserKey",
                        column: x => x.UserKey,
                        principalTable: "Users",
                        principalColumn: "UserKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    Duration = table.Column<double>(nullable: true),
                    WorkoutKey = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseKey);
                    table.ForeignKey(
                        name: "FK_Exercises_Workouts_WorkoutKey",
                        column: x => x.WorkoutKey,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedItems",
                columns: table => new
                {
                    FeedItemKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true),
                    FeedType = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    WorkoutKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItems", x => x.FeedItemKey);
                    table.ForeignKey(
                        name: "FK_FeedItems_Workouts_WorkoutKey",
                        column: x => x.WorkoutKey,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrails_AuditWorkoutWorkoutKey",
                table: "AuditTrails",
                column: "AuditWorkoutWorkoutKey");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrails_UserKey",
                table: "AuditTrails",
                column: "UserKey");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutKey",
                table: "Exercises",
                column: "WorkoutKey");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItems_WorkoutKey",
                table: "FeedItems",
                column: "WorkoutKey");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserKey",
                table: "Workouts",
                column: "UserKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "FeedItems");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
