using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class DeleteExtraColumnInNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notifications_expositions_SenderNotificationId",
                table: "notifications");

            migrationBuilder.DropIndex(
                name: "IX_notifications_SenderNotificationId",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "SenderNotificationId",
                table: "notifications");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_SenderNotificationsId",
                table: "notifications",
                column: "SenderNotificationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_expositions_SenderNotificationsId",
                table: "notifications",
                column: "SenderNotificationsId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notifications_expositions_SenderNotificationsId",
                table: "notifications");

            migrationBuilder.DropIndex(
                name: "IX_notifications_SenderNotificationsId",
                table: "notifications");

            migrationBuilder.AddColumn<int>(
                name: "SenderNotificationId",
                table: "notifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_SenderNotificationId",
                table: "notifications",
                column: "SenderNotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_expositions_SenderNotificationId",
                table: "notifications",
                column: "SenderNotificationId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
