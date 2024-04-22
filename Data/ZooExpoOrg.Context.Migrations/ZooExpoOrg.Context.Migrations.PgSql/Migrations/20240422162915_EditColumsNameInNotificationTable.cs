using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class EditColumsNameInNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notifications_expositions_SenderNotificationsId",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_recipients_clients_RecipientsNotificationId",
                table: "notifications_recipients");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_recipients_notifications_UnreadNotificationsId",
                table: "notifications_recipients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notifications_recipients",
                table: "notifications_recipients");

            migrationBuilder.RenameTable(
                name: "notifications_recipients",
                newName: "unreadNotifications_recipients");

            migrationBuilder.RenameColumn(
                name: "SenderNotificationsId",
                table: "notifications",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_notifications_SenderNotificationsId",
                table: "notifications",
                newName: "IX_notifications_SenderId");

            migrationBuilder.RenameColumn(
                name: "RecipientsNotificationId",
                table: "unreadNotifications_recipients",
                newName: "RecipientsId");

            migrationBuilder.RenameIndex(
                name: "IX_notifications_recipients_UnreadNotificationsId",
                table: "unreadNotifications_recipients",
                newName: "IX_unreadNotifications_recipients_UnreadNotificationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_unreadNotifications_recipients",
                table: "unreadNotifications_recipients",
                columns: new[] { "RecipientsId", "UnreadNotificationsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_expositions_SenderId",
                table: "notifications",
                column: "SenderId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_unreadNotifications_recipients_clients_RecipientsId",
                table: "unreadNotifications_recipients",
                column: "RecipientsId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_unreadNotifications_recipients_notifications_UnreadNotifica~",
                table: "unreadNotifications_recipients",
                column: "UnreadNotificationsId",
                principalTable: "notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notifications_expositions_SenderId",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_unreadNotifications_recipients_clients_RecipientsId",
                table: "unreadNotifications_recipients");

            migrationBuilder.DropForeignKey(
                name: "FK_unreadNotifications_recipients_notifications_UnreadNotifica~",
                table: "unreadNotifications_recipients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_unreadNotifications_recipients",
                table: "unreadNotifications_recipients");

            migrationBuilder.RenameTable(
                name: "unreadNotifications_recipients",
                newName: "notifications_recipients");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "notifications",
                newName: "SenderNotificationsId");

            migrationBuilder.RenameIndex(
                name: "IX_notifications_SenderId",
                table: "notifications",
                newName: "IX_notifications_SenderNotificationsId");

            migrationBuilder.RenameColumn(
                name: "RecipientsId",
                table: "notifications_recipients",
                newName: "RecipientsNotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_unreadNotifications_recipients_UnreadNotificationsId",
                table: "notifications_recipients",
                newName: "IX_notifications_recipients_UnreadNotificationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notifications_recipients",
                table: "notifications_recipients",
                columns: new[] { "RecipientsNotificationId", "UnreadNotificationsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_expositions_SenderNotificationsId",
                table: "notifications",
                column: "SenderNotificationsId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_recipients_clients_RecipientsNotificationId",
                table: "notifications_recipients",
                column: "RecipientsNotificationId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_recipients_notifications_UnreadNotificationsId",
                table: "notifications_recipients",
                column: "UnreadNotificationsId",
                principalTable: "notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
