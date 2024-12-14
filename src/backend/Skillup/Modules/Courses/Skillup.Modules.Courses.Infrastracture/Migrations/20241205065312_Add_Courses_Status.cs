using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Add_Courses_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "courses",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "courses",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "courses",
                table: "Courses");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "courses",
                table: "Courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
