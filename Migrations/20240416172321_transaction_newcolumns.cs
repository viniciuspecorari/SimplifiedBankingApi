using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplifiedBankingApi.Migrations
{
    /// <inheritdoc />
    public partial class transaction_newcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Transactions",
                newName: "PayerWalletId");

            migrationBuilder.AddColumn<Guid>(
                name: "PayeeWalletId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PayeeWalletId",
                table: "Transactions",
                column: "PayeeWalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_PayeeWalletId",
                table: "Transactions",
                column: "PayeeWalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_PayeeWalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PayeeWalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PayeeWalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "PayerWalletId",
                table: "Transactions",
                newName: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
