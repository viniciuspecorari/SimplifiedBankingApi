using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplifiedBankingApi.Migrations
{
    /// <inheritdoc />
    public partial class transactions_payee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_PayeeWalletId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "PayeeWalletId",
                table: "Transactions",
                newName: "payee");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PayeeWalletId",
                table: "Transactions",
                newName: "IX_Transactions_payee");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_payee",
                table: "Transactions",
                column: "payee",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_payee",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "payee",
                table: "Transactions",
                newName: "PayeeWalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_payee",
                table: "Transactions",
                newName: "IX_Transactions_PayeeWalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_PayeeWalletId",
                table: "Transactions",
                column: "PayeeWalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
