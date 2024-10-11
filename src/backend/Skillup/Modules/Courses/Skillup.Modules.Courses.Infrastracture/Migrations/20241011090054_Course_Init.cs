using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Course_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "courses");

            migrationBuilder.CreateTable(
                name: "ArticleAssets",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HTMLContent = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentAssets",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Instruction = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoAssets",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswerExercise",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerExercise_AssignmentAssets_AssignmentId",
                        column: x => x.AssignmentId,
                        principalSchema: "courses",
                        principalTable: "AssignmentAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionExercise",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestionExercise_AssignmentAssets_AssignmentId",
                        column: x => x.AssignmentId,
                        principalSchema: "courses",
                        principalTable: "AssignmentAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "courses",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAnswer",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    isCorrectAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAnswer_QuizQuestionExercise_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "courses",
                        principalTable: "QuizQuestionExercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubcategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Details_Subtitle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "text", nullable: true),
                    Difficulty = table.Column<string>(type: "text", nullable: false),
                    ObjectivesSummary = table.Column<string>(type: "text", nullable: false),
                    MustKnowBefore = table.Column<string>(type: "text", nullable: false),
                    IntendedFor = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "courses",
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalSchema: "courses",
                        principalTable: "Subcategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "courses",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    IsFree = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elements_Sections_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "courses",
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                schema: "courses",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubcategoryId",
                schema: "courses",
                table: "Courses",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_AssetId",
                schema: "courses",
                table: "Elements",
                column: "AssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_SectionId",
                schema: "courses",
                table: "Elements",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerExercise_AssignmentId",
                schema: "courses",
                table: "QuestionAnswerExercise",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswer_QuestionId",
                schema: "courses",
                table: "QuizAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionExercise_AssignmentId",
                schema: "courses",
                table: "QuizQuestionExercise",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseId",
                schema: "courses",
                table: "Sections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                schema: "courses",
                table: "Subcategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleAssets",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "Elements",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "QuestionAnswerExercise",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "QuizAnswer",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "VideoAssets",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "Sections",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "QuizQuestionExercise",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "AssignmentAssets",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "Subcategories",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "courses");
        }
    }
}
