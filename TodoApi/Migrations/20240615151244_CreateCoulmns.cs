using System;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateCoulmns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
              name: "isRead",
              table: "Suggestions",
              type: "bit",
              nullable: false,
              defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "isRead",
             table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "Customers");
        }
    }
}
