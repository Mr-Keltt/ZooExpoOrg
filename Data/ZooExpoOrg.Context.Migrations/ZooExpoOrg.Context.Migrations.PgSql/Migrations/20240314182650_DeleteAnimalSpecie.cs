using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAnimalSpecie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animals_animals_species_AnimalSpecieId",
                table: "animals");

            migrationBuilder.DropTable(
                name: "animals_species");

            migrationBuilder.DropIndex(
                name: "IX_animals_AnimalSpecieId",
                table: "animals");

            migrationBuilder.DropColumn(
                name: "AnimalSpecieId",
                table: "animals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalSpecieId",
                table: "animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_animals_AnimalSpecieId",
                table: "animals",
                column: "AnimalSpecieId");

            migrationBuilder.CreateIndex(
                name: "IX_animals_species_uid",
                table: "animals_species",
                column: "uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_animals_animals_species_AnimalSpecieId",
                table: "animals",
                column: "AnimalSpecieId",
                principalTable: "animals_species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
