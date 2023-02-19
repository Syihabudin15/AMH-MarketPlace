using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMH_MarketPlace.Migrations
{
    /// <inheritdoc />
    public partial class initMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_category_notif",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_category_notif", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_notif_read",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_read = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_notif_read", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_notif",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "Varchar(255)", nullable: true),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    is_read_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_notification_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_notif", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_notif_m_category_notif_category_notification_id",
                        column: x => x.category_notification_id,
                        principalTable: "m_category_notif",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_notif_m_notif_read_is_read_id",
                        column: x => x.is_read_id,
                        principalTable: "m_notif_read",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_credential",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "Varchar(100)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_credential", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_credential_m_role_role_id",
                        column: x => x.role_id,
                        principalTable: "m_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "Varchar(50)", nullable: false),
                    last_name = table.Column<string>(type: "Varchar(50)", nullable: false),
                    phone_number = table.Column<string>(type: "Varchar(13)", nullable: false),
                    credential_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_user_m_credential_credential_id",
                        column: x => x.credential_id,
                        principalTable: "m_credential",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_address",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address1 = table.Column<string>(type: "Varchar(255)", nullable: true),
                    address2 = table.Column<string>(type: "Varchar(255)", nullable: true),
                    city = table.Column<string>(type: "Varchar(255)", nullable: true),
                    post_code = table.Column<string>(type: "Varchar(5)", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_address_m_user_user_id",
                        column: x => x.user_id,
                        principalTable: "m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_address_user_id",
                table: "m_address",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_credential_email",
                table: "m_credential",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_credential_role_id",
                table: "m_credential",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_notif_category_notification_id",
                table: "m_notif",
                column: "category_notification_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_notif_is_read_id",
                table: "m_notif",
                column: "is_read_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_user_credential_id",
                table: "m_user",
                column: "credential_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_user_phone_number",
                table: "m_user",
                column: "phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_address");

            migrationBuilder.DropTable(
                name: "m_notif");

            migrationBuilder.DropTable(
                name: "m_user");

            migrationBuilder.DropTable(
                name: "m_category_notif");

            migrationBuilder.DropTable(
                name: "m_notif_read");

            migrationBuilder.DropTable(
                name: "m_credential");

            migrationBuilder.DropTable(
                name: "m_role");
        }
    }
}
