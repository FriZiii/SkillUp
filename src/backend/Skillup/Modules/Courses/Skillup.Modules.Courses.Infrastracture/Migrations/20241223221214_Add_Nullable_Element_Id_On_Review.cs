using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Add_Nullable_Element_Id_On_Review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Elements_CourseElementId",
                schema: "courses",
                table: "ReviewComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseElementId",
                schema: "courses",
                table: "ReviewComments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "courses",
                table: "ReviewComments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_CourseId",
                schema: "courses",
                table: "ReviewComments",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Courses_CourseId",
                schema: "courses",
                table: "ReviewComments",
                column: "CourseId",
                principalSchema: "courses",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Elements_CourseElementId",
                schema: "courses",
                table: "ReviewComments",
                column: "CourseElementId",
                principalSchema: "courses",
                principalTable: "Elements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Courses_CourseId",
                schema: "courses",
                table: "ReviewComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Elements_CourseElementId",
                schema: "courses",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_CourseId",
                schema: "courses",
                table: "ReviewComments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "courses",
                table: "ReviewComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseElementId",
                schema: "courses",
                table: "ReviewComments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Elements_CourseElementId",
                schema: "courses",
                table: "ReviewComments",
                column: "CourseElementId",
                principalSchema: "courses",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
