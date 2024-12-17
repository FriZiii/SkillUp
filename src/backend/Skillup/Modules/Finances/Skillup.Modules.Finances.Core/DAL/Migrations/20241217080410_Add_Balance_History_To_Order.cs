using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Balance_History_To_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChangeType",
                schema: "finances",
                table: "BalanceHistories",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ChangeDate",
                schema: "finances",
                table: "BalanceHistories",
                newName: "Date");

            migrationBuilder.AddColumn<Guid>(
                name: "BalanceHistoryId",
                schema: "finances",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BalanceHistoryId",
                schema: "finances",
                table: "Orders",
                column: "BalanceHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_BalanceHistories_BalanceHistoryId",
                schema: "finances",
                table: "Orders",
                column: "BalanceHistoryId",
                principalSchema: "finances",
                principalTable: "BalanceHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_BalanceHistories_BalanceHistoryId",
                schema: "finances",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BalanceHistoryId",
                schema: "finances",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BalanceHistoryId",
                schema: "finances",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "finances",
                table: "BalanceHistories",
                newName: "ChangeType");

            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "finances",
                table: "BalanceHistories",
                newName: "ChangeDate");
        }
    }
}
