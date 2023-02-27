using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMH_MarketPlace.Migrations
{
    /// <inheritdoc />
    public partial class fixTableTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "order_id",
                table: "m_transaction",
                type: "Varchar(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_id",
                table: "m_transaction");
        }
    }
}
