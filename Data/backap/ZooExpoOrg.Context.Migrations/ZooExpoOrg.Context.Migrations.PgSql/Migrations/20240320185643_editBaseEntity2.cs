using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class editBaseEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uid",
                table: "users_photos",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_users_photos_uid",
                table: "users_photos",
                newName: "IX_users_photos_Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "users",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_users_uid",
                table: "users",
                newName: "IX_users_Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "expositions_photos",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_expositions_photos_uid",
                table: "expositions_photos",
                newName: "IX_expositions_photos_Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "expositions",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_expositions_uid",
                table: "expositions",
                newName: "IX_expositions_Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "confirmations_achievements",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_confirmations_achievements_uid",
                table: "confirmations_achievements",
                newName: "IX_confirmations_achievements_Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "Comment",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_uid",
                table: "Comment",
                newName: "IX_Comment_Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "animals_photos",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_animals_photos_uid",
                table: "animals_photos",
                newName: "IX_animals_photos_Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "animals",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_animals_uid",
                table: "animals",
                newName: "IX_animals_Uid");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "achievements",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_achievements_uid",
                table: "achievements",
                newName: "IX_achievements_Uid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "users_photos",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_users_photos_Uid",
                table: "users_photos",
                newName: "IX_users_photos_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "users",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_users_Uid",
                table: "users",
                newName: "IX_users_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "expositions_photos",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_expositions_photos_Uid",
                table: "expositions_photos",
                newName: "IX_expositions_photos_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "expositions",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_expositions_Uid",
                table: "expositions",
                newName: "IX_expositions_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "confirmations_achievements",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_confirmations_achievements_Uid",
                table: "confirmations_achievements",
                newName: "IX_confirmations_achievements_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "Comment",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_Uid",
                table: "Comment",
                newName: "IX_Comment_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "animals_photos",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_animals_photos_Uid",
                table: "animals_photos",
                newName: "IX_animals_photos_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "animals",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_animals_Uid",
                table: "animals",
                newName: "IX_animals_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "achievements",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_achievements_Uid",
                table: "achievements",
                newName: "IX_achievements_uid");
        }
    }
}
