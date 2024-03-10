using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thrift_Us.Migrations
{
    public partial class hellosamiregmi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "RentalViewModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "RentalViewModel");
        }
    }
}
