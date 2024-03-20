using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class ConfirmationAchievementChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_confirmations_achievements_achievements_AchievementId",
                table: "confirmations_achievements");

            migrationBuilder.DropIndex(
                name: "IX_confirmations_achievements_AchievementId",
                table: "confirmations_achievements");

            migrationBuilder.AlterColumn<int>(
                name: "AchievementId",
                table: "confirmations_achievements",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_achievements_ConfirmationAchievementId",
                table: "achievements",
                column: "ConfirmationAchievementId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_achievements_confirmations_achievements_ConfirmationAchieve~",
                table: "achievements",
                column: "ConfirmationAchievementId",
                principalTable: "confirmations_achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_achievements_confirmations_achievements_ConfirmationAchieve~",
                table: "achievements");

            migrationBuilder.DropIndex(
                name: "IX_achievements_ConfirmationAchievementId",
                table: "achievements");

            migrationBuilder.AlterColumn<int>(
                name: "AchievementId",
                table: "confirmations_achievements",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_confirmations_achievements_AchievementId",
                table: "confirmations_achievements",
                column: "AchievementId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_confirmations_achievements_achievements_AchievementId",
                table: "confirmations_achievements",
                column: "AchievementId",
                principalTable: "achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
