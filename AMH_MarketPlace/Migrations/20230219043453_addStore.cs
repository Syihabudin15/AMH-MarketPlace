using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMH_MarketPlace.Migrations
{
    /// <inheritdoc />
    public partial class addStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "m_address",
                type: "Varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Varchar(255)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "m_rate_store",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rate1 = table.Column<int>(type: "int", nullable: true),
                    rate2 = table.Column<int>(type: "int", nullable: true),
                    rate3 = table.Column<int>(type: "int", nullable: true),
                    rate4 = table.Column<int>(type: "int", nullable: true),
                    rate5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_rate_store", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_store_image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conten_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_store_image", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_store",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "Varchar(150)", nullable: false),
                    description = table.Column<string>(type: "Varchar(500)", nullable: false),
                    store_image_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rate_store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_store", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_store_m_rate_store_rate_store_id",
                        column: x => x.rate_store_id,
                        principalTable: "m_rate_store",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_store_m_store_image_store_image_id",
                        column: x => x.store_image_id,
                        principalTable: "m_store_image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_store_rate_store_id",
                table: "m_store",
                column: "rate_store_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_store_store_image_id",
                table: "m_store",
                column: "store_image_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_store");

            migrationBuilder.DropTable(
                name: "m_rate_store");

            migrationBuilder.DropTable(
                name: "m_store_image");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "m_address",
                type: "Varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Varchar(100)",
                oldNullable: true);
        }
    }
}
