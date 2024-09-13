using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class createModelRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdModelId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AdModelId",
                table: "Notifications",
                column: "AdModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Ads_AdModelId",
                table: "Notifications",
                column: "AdModelId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Ads_AdModelId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AdModelId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AdModelId",
                table: "Notifications");
        }
    }
}
