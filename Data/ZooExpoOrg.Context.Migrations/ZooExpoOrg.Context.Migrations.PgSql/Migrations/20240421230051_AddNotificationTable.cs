using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MailingID = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderNotificationsId = table.Column<int>(type: "integer", nullable: false),
                    SenderNotificationId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_expositions_SenderNotificationId",
                        column: x => x.SenderNotificationId,
                        principalTable: "expositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications_recipients",
                columns: table => new
                {
                    RecipientsNotificationId = table.Column<int>(type: "integer", nullable: false),
                    UnreadNotificationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications_recipients", x => new { x.RecipientsNotificationId, x.UnreadNotificationsId });
                    table.ForeignKey(
                        name: "FK_notifications_recipients_clients_RecipientsNotificationId",
                        column: x => x.RecipientsNotificationId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notifications_recipients_notifications_UnreadNotificationsId",
                        column: x => x.UnreadNotificationsId,
                        principalTable: "notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notifications_SenderNotificationId",
                table: "notifications",
                column: "SenderNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_Uid",
                table: "notifications",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_recipients_UnreadNotificationsId",
                table: "notifications_recipients",
                column: "UnreadNotificationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifications_recipients");

            migrationBuilder.DropTable(
                name: "notifications");
        }
    }
}
