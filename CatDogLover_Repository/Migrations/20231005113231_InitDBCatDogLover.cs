using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatDogLoverRepository.Migrations
{
    /// <inheritdoc />
    public partial class InitDBCatDogLover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceImage",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "SourceImage",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
