using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class RenameAndRestruct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animal_photo_animals_AnimalId",
                table: "animal_photo");

            migrationBuilder.DropForeignKey(
                name: "FK_exposition_subscribers_expositions_SubscriptionsId",
                table: "exposition_subscribers");

            migrationBuilder.DropForeignKey(
                name: "FK_exposition_subscribers_users_SubscribersId",
                table: "exposition_subscribers");

            migrationBuilder.DropForeignKey(
                name: "FK_expositions_comments_animals_AnimalId",
                table: "expositions_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_expositions_comments_users_AuthorId",
                table: "expositions_comments");

            migrationBuilder.DropIndex(
                name: "IX_expositions_comments_AnimalId",
                table: "expositions_comments");

            migrationBuilder.DropIndex(
                name: "IX_expositions_comments_AuthorId",
                table: "expositions_comments");

            migrationBuilder.DropIndex(
                name: "IX_expositions_comments_uid",
                table: "expositions_comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_exposition_subscribers",
                table: "exposition_subscribers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_animal_photo",
                table: "animal_photo");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "expositions_comments");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "expositions_comments");

            migrationBuilder.DropColumn(
                name: "DateWriting",
                table: "expositions_comments");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "expositions_comments");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "expositions_comments");

            migrationBuilder.DropColumn(
                name: "uid",
                table: "expositions_comments");

            migrationBuilder.RenameTable(
                name: "exposition_subscribers",
                newName: "expositions_subscribers");

            migrationBuilder.RenameTable(
                name: "animal_photo",
                newName: "animals_photos");

            migrationBuilder.RenameIndex(
                name: "IX_exposition_subscribers_SubscriptionsId",
                table: "expositions_subscribers",
                newName: "IX_expositions_subscribers_SubscriptionsId");

            migrationBuilder.RenameIndex(
                name: "IX_animal_photo_uid",
                table: "animals_photos",
                newName: "IX_animals_photos_uid");

            migrationBuilder.RenameIndex(
                name: "IX_animal_photo_AnimalId",
                table: "animals_photos",
                newName: "IX_animals_photos_AnimalId");

            migrationBuilder.AlterColumn<int>(
                name: "ExpositionId",
                table: "expositions_comments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "expositions_comments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_expositions_subscribers",
                table: "expositions_subscribers",
                columns: new[] { "SubscribersId", "SubscriptionsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_animals_photos",
                table: "animals_photos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    DateWriting = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "animals_comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    AnimalId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animals_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_animals_comments_Comment_Id",
                        column: x => x.Id,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_animals_comments_animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_animals_comments_AnimalId",
                table: "animals_comments",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_uid",
                table: "Comment",
                column: "uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_animals_photos_animals_AnimalId",
                table: "animals_photos",
                column: "AnimalId",
                principalTable: "animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expositions_comments_Comment_Id",
                table: "expositions_comments",
                column: "Id",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expositions_subscribers_expositions_SubscriptionsId",
                table: "expositions_subscribers",
                column: "SubscriptionsId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expositions_subscribers_users_SubscribersId",
                table: "expositions_subscribers",
                column: "SubscribersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animals_photos_animals_AnimalId",
                table: "animals_photos");

            migrationBuilder.DropForeignKey(
                name: "FK_expositions_comments_Comment_Id",
                table: "expositions_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_expositions_subscribers_expositions_SubscriptionsId",
                table: "expositions_subscribers");

            migrationBuilder.DropForeignKey(
                name: "FK_expositions_subscribers_users_SubscribersId",
                table: "expositions_subscribers");

            migrationBuilder.DropTable(
                name: "animals_comments");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_expositions_subscribers",
                table: "expositions_subscribers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_animals_photos",
                table: "animals_photos");

            migrationBuilder.RenameTable(
                name: "expositions_subscribers",
                newName: "exposition_subscribers");

            migrationBuilder.RenameTable(
                name: "animals_photos",
                newName: "animal_photo");

            migrationBuilder.RenameIndex(
                name: "IX_expositions_subscribers_SubscriptionsId",
                table: "exposition_subscribers",
                newName: "IX_exposition_subscribers_SubscriptionsId");

            migrationBuilder.RenameIndex(
                name: "IX_animals_photos_uid",
                table: "animal_photo",
                newName: "IX_animal_photo_uid");

            migrationBuilder.RenameIndex(
                name: "IX_animals_photos_AnimalId",
                table: "animal_photo",
                newName: "IX_animal_photo_AnimalId");

            migrationBuilder.AlterColumn<int>(
                name: "ExpositionId",
                table: "expositions_comments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "expositions_comments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "expositions_comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "expositions_comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateWriting",
                table: "expositions_comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "expositions_comments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "expositions_comments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "uid",
                table: "expositions_comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_exposition_subscribers",
                table: "exposition_subscribers",
                columns: new[] { "SubscribersId", "SubscriptionsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_animal_photo",
                table: "animal_photo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_comments_AnimalId",
                table: "expositions_comments",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_comments_AuthorId",
                table: "expositions_comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_comments_uid",
                table: "expositions_comments",
                column: "uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_animal_photo_animals_AnimalId",
                table: "animal_photo",
                column: "AnimalId",
                principalTable: "animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_exposition_subscribers_expositions_SubscriptionsId",
                table: "exposition_subscribers",
                column: "SubscriptionsId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_exposition_subscribers_users_SubscribersId",
                table: "exposition_subscribers",
                column: "SubscribersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expositions_comments_animals_AnimalId",
                table: "expositions_comments",
                column: "AnimalId",
                principalTable: "animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expositions_comments_users_AuthorId",
                table: "expositions_comments",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
