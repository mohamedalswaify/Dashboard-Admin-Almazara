using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateNotfiticationViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfSend",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "notificationViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationModelId = table.Column<int>(type: "int", nullable: false),
                    CsutomerId = table.Column<int>(type: "int", nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificationViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notificationViews_Customers_CustomerModelId",
                        column: x => x.CustomerModelId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notificationViews_Notifications_NotificationModelId",
                        column: x => x.NotificationModelId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdViews_UserId",
                table: "AdViews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_notificationViews_CustomerModelId",
                table: "notificationViews",
                column: "CustomerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_notificationViews_NotificationModelId",
                table: "notificationViews",
                column: "NotificationModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdViews_Customers_UserId",
                table: "AdViews",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdViews_Customers_UserId",
                table: "AdViews");

            migrationBuilder.DropTable(
                name: "notificationViews");

            migrationBuilder.DropIndex(
                name: "IX_AdViews_UserId",
                table: "AdViews");

            migrationBuilder.DropColumn(
                name: "NumberOfSend",
                table: "Notifications");
        }
    }
}
