using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class CreateExpositionsParticipantsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animals_expositions_Id",
                table: "animals");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "animals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "expositions_participants",
                columns: table => new
                {
                    ExhibitionsId = table.Column<int>(type: "integer", nullable: false),
                    ParticipantsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expositions_participants", x => new { x.ExhibitionsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_expositions_participants_animals_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expositions_participants_expositions_ExhibitionsId",
                        column: x => x.ExhibitionsId,
                        principalTable: "expositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expositions_participants_ParticipantsId",
                table: "expositions_participants",
                column: "ParticipantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expositions_participants");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "animals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_animals_expositions_Id",
                table: "animals",
                column: "Id",
                principalTable: "expositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
