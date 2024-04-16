using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Breed",
                table: "animals",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "ParticipantsType",
                table: "expositions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantsType",
                table: "expositions");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "animals",
                newName: "Breed");
        }
    }
}
