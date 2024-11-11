using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class App_Properties_To_Section_And_Element : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                schema: "courses",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "courses",
                table: "Elements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                schema: "courses",
                table: "Elements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "courses",
                table: "Elements",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                schema: "courses",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "courses",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "Index",
                schema: "courses",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "courses",
                table: "Elements");
        }
    }
}
