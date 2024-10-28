using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Add_ProfilePictureKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                schema: "courses",
                table: "Users",
                newName: "ProfilePictureKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureKey",
                schema: "courses",
                table: "Users",
                newName: "ProfilePicture");
        }
    }
}
