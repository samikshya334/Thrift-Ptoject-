using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thrift_Us.Migrations
{
    public partial class refundamount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "RentalViewModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RefundAmount",
                table: "RentalCarts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "RentalViewModel");

            migrationBuilder.DropColumn(
                name: "RefundAmount",
                table: "RentalCarts");
        }
    }
}
