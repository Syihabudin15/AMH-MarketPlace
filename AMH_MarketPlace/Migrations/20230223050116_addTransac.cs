using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMH_MarketPlace.Migrations
{
    /// <inheritdoc />
    public partial class addTransac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_purchase_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_purchase_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "Varchar(50)", nullable: false),
                    payment_method = table.Column<string>(type: "Varchar(20)", nullable: true),
                    reference_pg = table.Column<string>(type: "Varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_transaction_m_user_user_id",
                        column: x => x.user_id,
                        principalTable: "m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_topup_wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    wallet_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_topup_wallet", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_topup_wallet_m_transaction_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "m_transaction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_topup_wallet_m_wallet_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "m_wallet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_transfer_wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    wallet_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    no_wallet_target = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_transfer_wallet", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_transfer_wallet_m_transaction_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "m_transaction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_transfer_wallet_m_wallet_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "m_wallet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_withdrawal_wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    wallet_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bank_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_withdrawal_wallet", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_withdrawal_wallet_m_transaction_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "m_transaction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_withdrawal_wallet_m_wallet_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "m_wallet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_topup_wallet_transaction_id",
                table: "m_topup_wallet",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_topup_wallet_wallet_id",
                table: "m_topup_wallet",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_transaction_user_id",
                table: "m_transaction",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_transfer_wallet_transaction_id",
                table: "m_transfer_wallet",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_transfer_wallet_wallet_id",
                table: "m_transfer_wallet",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_withdrawal_wallet_transaction_id",
                table: "m_withdrawal_wallet",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_withdrawal_wallet_wallet_id",
                table: "m_withdrawal_wallet",
                column: "wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_purchase_product");

            migrationBuilder.DropTable(
                name: "m_topup_wallet");

            migrationBuilder.DropTable(
                name: "m_transfer_wallet");

            migrationBuilder.DropTable(
                name: "m_withdrawal_wallet");

            migrationBuilder.DropTable(
                name: "m_transaction");
        }
    }
}
