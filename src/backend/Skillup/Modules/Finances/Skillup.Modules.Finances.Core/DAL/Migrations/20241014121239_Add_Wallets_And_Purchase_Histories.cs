using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Wallets_And_Purchase_Histories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wallets",
                schema: "finances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseHistories",
                schema: "finances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserWalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseHistories_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "finances",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseHistories_Wallets_UserWalletId",
                        column: x => x.UserWalletId,
                        principalSchema: "finances",
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_ItemId",
                schema: "finances",
                table: "PurchaseHistories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_UserWalletId",
                schema: "finances",
                table: "PurchaseHistories",
                column: "UserWalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseHistories",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Wallets",
                schema: "finances");
        }
    }
}
