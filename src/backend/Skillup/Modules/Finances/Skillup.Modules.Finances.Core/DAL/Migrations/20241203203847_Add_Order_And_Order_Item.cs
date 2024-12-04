using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Order_And_Order_Item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_UserId",
                schema: "finances",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_UserId",
                schema: "finances",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "finances",
                table: "Wallets",
                newName: "OwnerId");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "finances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    OrdererId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_OrdererId",
                        column: x => x.OrdererId,
                        principalSchema: "finances",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "finances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "finances",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "finances",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_OwnerId",
                schema: "finances",
                table: "Wallets",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                schema: "finances",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "finances",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrdererId",
                schema: "finances",
                table: "Orders",
                column: "OrdererId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_OwnerId",
                schema: "finances",
                table: "Wallets",
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
                name: "FK_Wallets_Users_OwnerId",
                schema: "finances",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "finances");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_OwnerId",
                schema: "finances",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                schema: "finances",
                table: "Wallets",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                schema: "finances",
                table: "Wallets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_UserId",
                schema: "finances",
                table: "Wallets",
                column: "UserId",
                principalSchema: "finances",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
