using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AnimalSpecie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animals_animals_species_AnimalSpecieId",
                table: "animals");

            migrationBuilder.DropIndex(
                name: "IX_animals_AnimalSpecieId",
                table: "animals");

            migrationBuilder.AddForeignKey(
                name: "FK_animals_species_animals_Id",
                table: "animals_species",
                column: "Id",
                principalTable: "animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animals_species_animals_Id",
                table: "animals_species");

            migrationBuilder.CreateIndex(
                name: "IX_animals_AnimalSpecieId",
                table: "animals",
                column: "AnimalSpecieId");

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
