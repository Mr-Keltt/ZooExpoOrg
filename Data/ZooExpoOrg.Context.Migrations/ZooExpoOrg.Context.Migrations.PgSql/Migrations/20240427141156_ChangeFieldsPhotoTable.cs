using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldsPhotoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "photos");

            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "photos");

            migrationBuilder.AddColumn<string>(
                name: "StringImageData",
                table: "photos",
                type: "character varying(1000000)",
                maxLength: 1000000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StringImageData",
                table: "photos");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "photos",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "photos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
