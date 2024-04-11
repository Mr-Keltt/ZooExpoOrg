using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumInAnimalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expositions_participants_expositions_ExhibitionsId",
                table: "expositions_participants");

            migrationBuilder.RenameColumn(
                name: "ExhibitionsId",
                table: "expositions_participants",
                newName: "ExpositionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_expositions_participants_expositions_ExpositionsId",
                table: "expositions_participants",
                column: "ExpositionsId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expositions_participants_expositions_ExpositionsId",
                table: "expositions_participants");

            migrationBuilder.RenameColumn(
                name: "ExpositionsId",
                table: "expositions_participants",
                newName: "ExhibitionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_expositions_participants_expositions_ExhibitionsId",
                table: "expositions_participants",
                column: "ExhibitionsId",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
