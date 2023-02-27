using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMH_MarketPlace.Migrations
{
    /// <inheritdoc />
    public partial class updateTableTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "m_transaction",
                type: "Varchar(15)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "m_transaction");
        }
    }
}
