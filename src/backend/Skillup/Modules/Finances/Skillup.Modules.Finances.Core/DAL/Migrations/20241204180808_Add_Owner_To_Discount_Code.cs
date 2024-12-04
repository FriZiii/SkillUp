using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Owner_To_Discount_Code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                schema: "finances",
                table: "DiscountCodes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_OwnerId",
                schema: "finances",
                table: "DiscountCodes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodes_Users_OwnerId",
                schema: "finances",
                table: "DiscountCodes",
                column: "OwnerId",
                principalSchema: "finances",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodes_Users_OwnerId",
                schema: "finances",
                table: "DiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCodes_OwnerId",
                schema: "finances",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "finances",
                table: "DiscountCodes");
        }
    }
}
