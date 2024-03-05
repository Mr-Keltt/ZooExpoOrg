using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users_photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: false),
                    ImageMimeType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_users_photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "users_photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "expositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    OrganizersId = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    DateStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_expositions_users_OrganizersId",
                        column: x => x.OrganizersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "animals_species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animals_species", x => x.Id);
                    table.ForeignKey(
                        name: "FK_animals_species_expositions_Id",
                        column: x => x.Id,
                        principalTable: "expositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exposition_subscribers",
                columns: table => new
                {
                    SubscribersId = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exposition_subscribers", x => new { x.SubscribersId, x.SubscriptionsId });
                    table.ForeignKey(
                        name: "FK_exposition_subscribers_expositions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "expositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exposition_subscribers_users_SubscribersId",
                        column: x => x.SubscribersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expositions_photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpositionId = table.Column<int>(type: "integer", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: false),
                    ImageMimeType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expositions_photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_expositions_photos_expositions_ExpositionId",
                        column: x => x.ExpositionId,
                        principalTable: "expositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AnimalSpecieId = table.Column<int>(type: "integer", nullable: false),
                    Breed = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_animals_animals_species_AnimalSpecieId",
                        column: x => x.AnimalSpecieId,
                        principalTable: "animals_species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_animals_expositions_Id",
                        column: x => x.Id,
                        principalTable: "expositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_animals_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateAward = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AnimalId = table.Column<int>(type: "integer", nullable: false),
                    ConfirmationAchievementId = table.Column<int>(type: "integer", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_achievements_animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "animal_photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnimalId = table.Column<int>(type: "integer", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: false),
                    ImageMimeType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animal_photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_animal_photo_animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expositions_comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    DateWriting = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    AnimalId = table.Column<int>(type: "integer", nullable: true),
                    ExpositionId = table.Column<int>(type: "integer", nullable: true),
                    uid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expositions_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_expositions_comments_animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expositions_comments_expositions_ExpositionId",
                        column: x => x.ExpositionId,
                        principalTable: "expositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expositions_comments_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "confirmations_achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AchievementId = table.Column<int>(type: "integer", nullable: false),
                    uid = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileContent = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_confirmations_achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_confirmations_achievements_achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_achievements_AnimalId",
                table: "achievements",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_achievements_uid",
                table: "achievements",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_animal_photo_AnimalId",
                table: "animal_photo",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_animal_photo_uid",
                table: "animal_photo",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_animals_AnimalSpecieId",
                table: "animals",
                column: "AnimalSpecieId");

            migrationBuilder.CreateIndex(
                name: "IX_animals_uid",
                table: "animals",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_animals_UserId",
                table: "animals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_animals_species_uid",
                table: "animals_species",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_confirmations_achievements_AchievementId",
                table: "confirmations_achievements",
                column: "AchievementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_confirmations_achievements_uid",
                table: "confirmations_achievements",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_exposition_subscribers_SubscriptionsId",
                table: "exposition_subscribers",
                column: "SubscriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_OrganizersId",
                table: "expositions",
                column: "OrganizersId");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_uid",
                table: "expositions",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_expositions_comments_AnimalId",
                table: "expositions_comments",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_comments_AuthorId",
                table: "expositions_comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_comments_ExpositionId",
                table: "expositions_comments",
                column: "ExpositionId");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_comments_uid",
                table: "expositions_comments",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_expositions_photos_ExpositionId",
                table: "expositions_photos",
                column: "ExpositionId");

            migrationBuilder.CreateIndex(
                name: "IX_expositions_photos_uid",
                table: "expositions_photos",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_PhotoId",
                table: "users",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_uid",
                table: "users",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_photos_uid",
                table: "users_photos",
                column: "uid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animal_photo");

            migrationBuilder.DropTable(
                name: "confirmations_achievements");

            migrationBuilder.DropTable(
                name: "exposition_subscribers");

            migrationBuilder.DropTable(
                name: "expositions_comments");

            migrationBuilder.DropTable(
                name: "expositions_photos");

            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.DropTable(
                name: "animals");

            migrationBuilder.DropTable(
                name: "animals_species");

            migrationBuilder.DropTable(
                name: "expositions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "users_photos");
        }
    }
}
