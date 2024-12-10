using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_DiscountCode_To_Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DiscountCodeId",
                schema: "finances",
                table: "Carts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_DiscountCodeId",
                schema: "finances",
                table: "Carts",
                column: "DiscountCodeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_DiscountCodes_DiscountCodeId",
                schema: "finances",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_DiscountCodeId",
                schema: "finances",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DiscountCodeId",
                schema: "finances",
                table: "Carts");
        }
    }
}
