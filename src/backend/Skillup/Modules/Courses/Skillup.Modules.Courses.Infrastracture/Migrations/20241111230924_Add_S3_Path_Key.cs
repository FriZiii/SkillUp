using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Add_S3_Path_Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                schema: "courses",
                table: "VideoAssets");

            migrationBuilder.DropColumn(
                name: "HTMLContent",
                schema: "courses",
                table: "ArticleAssets");

            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                schema: "courses",
                table: "VideoAssets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                schema: "courses",
                table: "ArticleAssets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                schema: "courses",
                table: "VideoAssets");

            migrationBuilder.DropColumn(
                name: "Key",
                schema: "courses",
                table: "ArticleAssets");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "courses",
                table: "VideoAssets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HTMLContent",
                schema: "courses",
                table: "ArticleAssets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
