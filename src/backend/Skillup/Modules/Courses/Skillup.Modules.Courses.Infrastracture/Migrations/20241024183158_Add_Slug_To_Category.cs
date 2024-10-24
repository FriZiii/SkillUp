using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Add_Slug_To_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "courses",
                table: "Subcategories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "courses",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "courses",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "courses",
                table: "Categories");
        }
    }
}
