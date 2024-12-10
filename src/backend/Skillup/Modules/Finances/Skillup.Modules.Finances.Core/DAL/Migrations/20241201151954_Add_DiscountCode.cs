using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_DiscountCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "finances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountCodes",
                schema: "finances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AppliesToEntireCart = table.Column<bool>(type: "boolean", nullable: false),
                    HasUsageLimit = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    MaxUsageLimit = table.Column<int>(type: "integer", nullable: true),
                    UsageCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "finances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CartId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "finances",
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "finances",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountedItems",
                schema: "finances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountCodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountedItems_DiscountCodes_DiscountCodeId",
                        column: x => x.DiscountCodeId,
                        principalSchema: "finances",
                        principalTable: "DiscountCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountedItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "finances",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                schema: "finances",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ItemId",
                schema: "finances",
                table: "CartItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_Code",
                schema: "finances",
                table: "DiscountCodes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_IsActive",
                schema: "finances",
                table: "DiscountCodes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_IsPublic",
                schema: "finances",
                table: "DiscountCodes",
                column: "IsPublic");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountedItems_DiscountCodeId",
                schema: "finances",
                table: "DiscountedItems",
                column: "DiscountCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountedItems_ItemId",
                schema: "finances",
                table: "DiscountedItems",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "DiscountedItems",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "DiscountCodes",
                schema: "finances");
        }
    }
}
