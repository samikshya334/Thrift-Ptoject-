﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thrift_Us.Migrations
{
    public partial class goodpratimagirlssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "RentalOrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "RentalOrderDetails");
        }
    }
}
