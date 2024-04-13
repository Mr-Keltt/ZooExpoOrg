using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConfirmationsAchievementsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_achievements_confirmations_achievements_ConfirmationAchieve~",
                table: "achievements");

            migrationBuilder.DropTable(
                name: "confirmations_achievements");

            migrationBuilder.DropIndex(
                name: "IX_achievements_ConfirmationAchievementId",
                table: "achievements");

            migrationBuilder.DropColumn(
                name: "ConfirmationAchievementId",
                table: "achievements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfirmationAchievementId",
                table: "achievements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "confirmations_achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AchievementId = table.Column<int>(type: "integer", nullable: true),
                    FileContent = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_confirmations_achievements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_achievements_ConfirmationAchievementId",
                table: "achievements",
                column: "ConfirmationAchievementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_confirmations_achievements_Uid",
                table: "confirmations_achievements",
                column: "Uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_achievements_confirmations_achievements_ConfirmationAchieve~",
                table: "achievements",
                column: "ConfirmationAchievementId",
                principalTable: "confirmations_achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
