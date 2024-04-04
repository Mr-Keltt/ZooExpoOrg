using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class FixUserAndClientConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_clients_ClientId",
                table: "users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_clients_Uid",
                table: "clients");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_clients_UserId",
                table: "clients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_clients_ClientId",
                table: "users",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_clients_ClientId",
                table: "users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_clients_UserId",
                table: "clients");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_clients_Uid",
                table: "clients",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_users_clients_ClientId",
                table: "users",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
