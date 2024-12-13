using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Add_Course_User_Progress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseUserProgess",
                schema: "courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ElementId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUserProgess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseUserProgess_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "courses",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUserProgess_Elements_ElementId",
                        column: x => x.ElementId,
                        principalSchema: "courses",
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUserProgess_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "courses",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseUserProgess_CourseId",
                schema: "courses",
                table: "CourseUserProgess",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUserProgess_ElementId",
                schema: "courses",
                table: "CourseUserProgess",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUserProgess_UserId",
                schema: "courses",
                table: "CourseUserProgess",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseUserProgess",
                schema: "courses");
        }
    }
}
