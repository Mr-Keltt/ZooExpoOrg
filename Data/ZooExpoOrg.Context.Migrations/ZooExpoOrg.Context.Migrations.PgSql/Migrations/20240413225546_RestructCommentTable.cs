using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class RestructCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_animals_AnimalEntityId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_expositions_LocationId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_AnimalEntityId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "AnimalEntityId",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "comments",
                newName: "ExpositionId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_LocationId",
                table: "comments",
                newName: "IX_comments_ExpositionId");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_comments_AnimalId",
                table: "comments",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_animals_AnimalId",
                table: "comments",
                column: "AnimalId",
                principalTable: "animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_expositions_ExpositionId",
                table: "comments",
                column: "ExpositionId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_animals_AnimalId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_expositions_ExpositionId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_AnimalId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "ExpositionId",
                table: "comments",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_ExpositionId",
                table: "comments",
                newName: "IX_comments_LocationId");

            migrationBuilder.AddColumn<int>(
                name: "AnimalEntityId",
                table: "comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_AnimalEntityId",
                table: "comments",
                column: "AnimalEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_animals_AnimalEntityId",
                table: "comments",
                column: "AnimalEntityId",
                principalTable: "animals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_expositions_LocationId",
                table: "comments",
                column: "LocationId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
