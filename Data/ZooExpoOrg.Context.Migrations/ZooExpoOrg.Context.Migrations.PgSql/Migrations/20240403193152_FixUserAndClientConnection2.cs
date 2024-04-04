using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class FixUserAndClientConnection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_clients_ClientId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_ClientId",
                table: "users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_clients_UserId",
                table: "clients");

            migrationBuilder.CreateIndex(
                name: "IX_clients_UserId",
                table: "clients",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_users_UserId",
                table: "clients",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_users_UserId",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "IX_clients_UserId",
                table: "clients");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_clients_UserId",
                table: "clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_ClientId",
                table: "users",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_clients_ClientId",
                table: "users",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
