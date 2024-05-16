using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseLoan.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedinterest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Interest",
                table: "Amortizations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interest",
                table: "Amortizations");
        }
    }
}
