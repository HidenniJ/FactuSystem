using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactuSystem.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertyInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypePayment",
                table: "Facturas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypePayment",
                table: "Facturas");
        }
    }
}
