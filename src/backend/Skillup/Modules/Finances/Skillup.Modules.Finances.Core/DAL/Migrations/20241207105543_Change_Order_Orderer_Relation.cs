using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Change_Order_Orderer_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrdererId",
                schema: "finances",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrdererId",
                schema: "finances",
                table: "Orders",
                column: "OrdererId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrdererId",
                schema: "finances",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrdererId",
                schema: "finances",
                table: "Orders",
                column: "OrdererId",
                unique: true);
        }
    }
}
