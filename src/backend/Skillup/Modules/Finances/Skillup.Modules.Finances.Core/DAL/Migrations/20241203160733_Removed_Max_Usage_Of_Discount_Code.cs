using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Max_Usage_Of_Discount_Code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_DiscountCodes_DiscountCodeId",
                schema: "finances",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "HasUsageLimit",
                schema: "finances",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "MaxUsageLimit",
                schema: "finances",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "UsageCount",
                schema: "finances",
                table: "DiscountCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_DiscountCodes_DiscountCodeId",
                schema: "finances",
                table: "Carts",
                column: "DiscountCodeId",
                principalSchema: "finances",
                principalTable: "DiscountCodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_DiscountCodes_DiscountCodeId",
                schema: "finances",
                table: "Carts");

            migrationBuilder.AddColumn<bool>(
                name: "HasUsageLimit",
                schema: "finances",
                table: "DiscountCodes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxUsageLimit",
                schema: "finances",
                table: "DiscountCodes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsageCount",
                schema: "finances",
                table: "DiscountCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_DiscountCodes_DiscountCodeId",
                schema: "finances",
                table: "Carts",
                column: "DiscountCodeId",
                principalSchema: "finances",
                principalTable: "DiscountCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
