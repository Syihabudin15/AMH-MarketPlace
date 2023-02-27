using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMH_MarketPlace.Migrations
{
    /// <inheritdoc />
    public partial class addProductAndWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "m_store",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "body",
                table: "m_notif",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "m_category_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "Varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_category_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "Varchar(150)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    weight_kg = table.Column<float>(type: "real", nullable: true),
                    weight_g = table.Column<float>(type: "real", nullable: true),
                    size_inch = table.Column<float>(type: "real", nullable: true),
                    size_cm = table.Column<float>(type: "real", nullable: true),
                    stock = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    category_product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_product_m_store_store_id",
                        column: x => x.store_id,
                        principalTable: "m_store",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_product_image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conten_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product_image", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    no_wallet = table.Column<string>(type: "Varchar(17)", nullable: false),
                    balance = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_wallet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_user_wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "Varchar(150)", nullable: false),
                    nik = table.Column<string>(type: "Varchar(16)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "Varchar(100)", nullable: false),
                    national = table.Column<string>(type: "Varchar(100)", nullable: true),
                    is_verified = table.Column<bool>(type: "bit", nullable: false),
                    wallet_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_user_wallet", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_user_wallet_m_wallet_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "m_wallet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_product_store_id",
                table: "m_product",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_user_wallet_nik",
                table: "m_user_wallet",
                column: "nik",
                unique: true,
                filter: "[nik] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_m_user_wallet_wallet_id",
                table: "m_user_wallet",
                column: "wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_category_product");

            migrationBuilder.DropTable(
                name: "m_product");

            migrationBuilder.DropTable(
                name: "m_product_image");

            migrationBuilder.DropTable(
                name: "m_user_wallet");

            migrationBuilder.DropTable(
                name: "m_wallet");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "m_store",
                type: "Varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "body",
                table: "m_notif",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
