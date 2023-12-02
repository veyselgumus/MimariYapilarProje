using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Yapilar",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "Yapilar",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Mimarlar",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "Mimarlar",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Yapilar");

            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "Yapilar");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Mimarlar");

            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "Mimarlar");
        }
    }
}
