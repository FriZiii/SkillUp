using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Removed_AssetID_Form_Element_Added_ElementId_To_Asset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Elements_AssetId",
                schema: "courses",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "AssetId",
                schema: "courses",
                table: "Elements");

            migrationBuilder.AddColumn<Guid>(
                name: "ElementId",
                schema: "courses",
                table: "VideoAssets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ElementId",
                schema: "courses",
                table: "AssignmentAssets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ElementId",
                schema: "courses",
                table: "ArticleAssets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VideoAssets_ElementId",
                schema: "courses",
                table: "VideoAssets",
                column: "ElementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAssets_ElementId",
                schema: "courses",
                table: "AssignmentAssets",
                column: "ElementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAssets_ElementId",
                schema: "courses",
                table: "ArticleAssets",
                column: "ElementId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleAssets_Elements_ElementId",
                schema: "courses",
                table: "ArticleAssets",
                column: "ElementId",
                principalSchema: "courses",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentAssets_Elements_ElementId",
                schema: "courses",
                table: "AssignmentAssets",
                column: "ElementId",
                principalSchema: "courses",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoAssets_Elements_ElementId",
                schema: "courses",
                table: "VideoAssets",
                column: "ElementId",
                principalSchema: "courses",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleAssets_Elements_ElementId",
                schema: "courses",
                table: "ArticleAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentAssets_Elements_ElementId",
                schema: "courses",
                table: "AssignmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoAssets_Elements_ElementId",
                schema: "courses",
                table: "VideoAssets");

            migrationBuilder.DropIndex(
                name: "IX_VideoAssets_ElementId",
                schema: "courses",
                table: "VideoAssets");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentAssets_ElementId",
                schema: "courses",
                table: "AssignmentAssets");

            migrationBuilder.DropIndex(
                name: "IX_ArticleAssets_ElementId",
                schema: "courses",
                table: "ArticleAssets");

            migrationBuilder.DropColumn(
                name: "ElementId",
                schema: "courses",
                table: "VideoAssets");

            migrationBuilder.DropColumn(
                name: "ElementId",
                schema: "courses",
                table: "AssignmentAssets");

            migrationBuilder.DropColumn(
                name: "ElementId",
                schema: "courses",
                table: "ArticleAssets");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                schema: "courses",
                table: "Elements",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_AssetId",
                schema: "courses",
                table: "Elements",
                column: "AssetId",
                unique: true);
        }
    }
}
