using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateIsDeleteEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "BackpDatabases");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Employees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHide",
                table: "Employees",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsHide",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BackpDatabases",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
