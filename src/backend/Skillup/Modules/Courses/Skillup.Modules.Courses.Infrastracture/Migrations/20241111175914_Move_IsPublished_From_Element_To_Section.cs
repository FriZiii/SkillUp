using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Move_IsPublished_From_Element_To_Section : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "courses",
                table: "Elements");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "courses",
                table: "Sections",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "courses",
                table: "Sections");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "courses",
                table: "Elements",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
